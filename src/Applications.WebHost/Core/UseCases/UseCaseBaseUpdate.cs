using AspNetCoreNuxt.Applications.WebHost.Core.Authorizations;
using AspNetCoreNuxt.Extensions;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Polly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// 指定された主キーに一致するデータを非同期に更新します。
        /// </summary>
        /// <param name="keyValues">更新するデータの主キー値のコレクション。</param>
        /// <param name="model">更新するデータを表す <typeparamref name="T"/> オブジェクト。</param>
        /// <returns>更新が成功したかどうかを表す値と、更新されたエンティティのタプルオブジェクト。</returns>
        public virtual Task<(bool Succeeded, T Entity)> UpdateAsync(object[] keyValues, T model)
            => UpdateCoreAsync(keyValues, model, propertyNames: new[] { "*" });

        /// <summary>
        /// 指定された主キーに一致するデータを非同期に更新します。
        /// </summary>
        /// <param name="keyValues">更新するデータの主キー値のコレクション。</param>
        /// <param name="model">更新するデータを表す <typeparamref name="T"/> オブジェクト。</param>
        /// <param name="propertyNames">更新するプロパティ名のコレクション。</param>
        /// <returns>更新が成功したかどうかを表す値と、更新されたエンティティのタプルオブジェクト。</returns>
        protected async Task<(bool Succeeded, T Entity)> UpdateCoreAsync(object[] keyValues, T model, IEnumerable<string> propertyNames)
        {
            var source = await ExpandoObjectFactory.CreateAsync(
                typeof(T),
                model,
                propertyNames,
                Operations.Update,
                includeUnauthorizedProperty: false,
                includeNullObject: false);

            var linq = Context.Set<T>().AsTracking();
            foreach (var navigationPropertyPath in GetNavigationPropertyPaths(Context.Model, typeof(T), source))
            {
                linq = linq.Include(navigationPropertyPath);
            }

            var entity = await linq.Where(Context.Model, keyValues).SingleOrDefaultAsync();
            if (entity is null)
            {
                return (Succeeded: false, Entity: null);
            }

            MapMergeCollection(Context.Model, Mapper, entity, source);
            Mapper.Map(source, entity);

            var succeeded = true;
            if (Context.ChangeTracker.HasChanges())
            {
                succeeded = await Policy<bool>
                    .Handle<DbUpdateConcurrencyException>()
                    .FallbackAsync(false)
                    .ExecuteAsync(async () => await Context.SaveChangesAsync() != 0);
            }

            return (Succeeded: succeeded, Entity: succeeded ? entity : null);

            static IEnumerable<string> GetNavigationPropertyPaths(IModel model, Type type, object values)
                => GetNavigationPropertyPathsCore(model, type, values as IDictionary<string, object>).OrderBy(x => x).Distinct();

            static IEnumerable<string> GetNavigationPropertyPathsCore(IModel model, Type type, IDictionary<string, object> dictionary)
            {
                foreach (var key in dictionary.Keys)
                {
                    var value = dictionary[key];
                    if (value is IDictionary<string, object>)
                    {
                        var navigation = model.FindEntityType(type).GetNavigations().SingleOrDefault(x => x.Name == key);
                        if (navigation is not null)
                        {
                            yield return key;
                            var navigationType = navigation.ClrType;
                            foreach (var path in GetNavigationPropertyPaths(model, navigationType, value))
                            {
                                yield return $"{key}.{path}";
                            }
                        }
                    }
                    else if (value is IEnumerable enumerable && enumerable.GetType() != typeof(string))
                    {
                        var navigation = model.FindEntityType(type).GetNavigations().SingleOrDefault(x => x.Name == key);
                        if (navigation is not null)
                        {
                            yield return key;
                            foreach (var item in enumerable)
                            {
                                if (item is IDictionary<string, object>)
                                {
                                    var navigationType = navigation.TargetEntityType.ClrType;
                                    foreach (var path in GetNavigationPropertyPaths(model, navigationType, item))
                                    {
                                        yield return $"{key}.{path}";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            static void MapMergeCollection(IModel model, IMapper mapper, object entity, object values)
                => MapMergeCollectionCore(model, mapper, entity, values as IDictionary<string, object>);

            static void MapMergeCollectionCore(IModel model, IMapper mapper, object entity, IDictionary<string, object> dictionary)
            {
                foreach (var navigation in model.FindEntityType(typeof(T)).GetNavigations())
                {
                    if (navigation.IsCollection && dictionary.ContainsKey(navigation.Name))
                    {
                        var type = navigation.TargetEntityType.ClrType;
                        var sourceCollection = dictionary[navigation.Name];
                        var destinationCollection = typeof(T).GetProperty(navigation.Name).GetValue(entity);
                        var collectionMapperType = typeof(CollectionMapper<>).MakeGenericType(type);
                        var collectionMapperConstructor = ActivatorHelper.GetConstructor(
                            collectionMapperType,
                            typeof(IModel),
                            typeof(IMapper),
                            typeof(IEnumerable<object>),
                            typeof(ICollection<>).MakeGenericType(type));
                        var collectionMapperParameters = new object[] { model, mapper, sourceCollection, destinationCollection };
                        var collectionMapper = (ICollectionMapper)collectionMapperConstructor.Invoke(collectionMapperParameters);
                        collectionMapper.Map();
                        dictionary.Remove(navigation.Name);
                    }
                }
            }
        }

        private interface ICollectionMapper
        {
            void Map();
        }

        private class CollectionMapper<Txxx> : ICollectionMapper
        {
            private readonly IModel Model;
            private readonly IMapper Mapper;
            private readonly IEnumerable<object> Sources;
            private readonly ICollection<Txxx> Destinations;

            public CollectionMapper(IModel model, IMapper mapper, IEnumerable<object> sources, ICollection<Txxx> destinations)
            {
                Model = model;
                Mapper = mapper;
                Sources = sources;
                Destinations = destinations;
            }

            public void Map()
            {
                var primaryKeyProperties = Model.FindEntityType(typeof(Txxx)).FindPrimaryKey().Properties;
                foreach (var source in Sources)
                {
                    var destination = Destinations.SingleOrDefault(destination => primaryKeyProperties.All(primaryKeyProperty =>
                    {
                        var sourceValue = (source as IDictionary<string, object>)[primaryKeyProperty.Name];
                        var destinationValue = typeof(Txxx).GetProperty(primaryKeyProperty.Name).GetValue(destination);
                        return sourceValue?.ToString() == destinationValue?.ToString();
                    }));
                    if (destination is null)
                    {
                        var item = Mapper.Map<Txxx>(source);
                        Destinations.Add(item);
                    }
                    else
                    {
                        Mapper.Map(source, destination);
                    }
                }

                var removes = new List<Txxx>();
                foreach (var destination in Destinations)
                {
                    var source = Sources.SingleOrDefault(source => primaryKeyProperties.All(primaryKeyProperty =>
                    {
                        var sourceValue = (source as IDictionary<string, object>)[primaryKeyProperty.Name];
                        var destinationValue = typeof(Txxx).GetProperty(primaryKeyProperty.Name).GetValue(destination);
                        return sourceValue?.ToString() == destinationValue?.ToString();
                    }));
                    if (source is null)
                    {
                        removes.Add(destination);
                    }
                }
                foreach (var remove in removes)
                {
                    Destinations.Remove(remove);
                }
            }
        }
    }
}
