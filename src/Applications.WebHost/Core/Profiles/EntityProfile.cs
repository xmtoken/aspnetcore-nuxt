using AspNetCoreNuxt.Domains.Data;
using AutoMapper;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Profiles
{
    /// <summary>
    /// <see cref="IEntity{T}"/> インタフェースを実装したクラスのマッピングを提供します。
    /// </summary>
    public class EntityProfile : Profile
    {
        /// <summary>
        /// <see cref="EntityProfile"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public EntityProfile(AppDbContext context)
        {
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                CreateMap(entityType.ClrType, entityType.ClrType)
                    .ForAllMembers(x => x.ExplicitExpansion());
            }

            //Expression<Func<ExpandoObject, ManyEntity, bool>> xxxx
            //    = (source, destination) => true;

            //CreateMap<ExpandoObject, ManyEntity>()
            //    .EqualityComparison(xxxx);
        }

        // https://entityframeworkcore.com/knowledge-base/52218340/automapper-projectto-adds-tolist-into-child-properties
        public class GenericEnumerableExpressionBinder : IExpressionBinder
        {
            public bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result) =>
                propertyMap.DestinationType.IsGenericType &&
                propertyMap.DestinationType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

            public MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, IDictionary<ExpressionRequest, int> typePairCount, LetPropertyMaps letPropertyMaps)
                => BindEnumerableExpression(configuration, propertyMap, request, result, typePairCount, letPropertyMaps);

            private static MemberAssignment BindEnumerableExpression(IConfigurationProvider configuration, PropertyMap propertyMap, ExpressionRequest request, ExpressionResolutionResult result, IDictionary<ExpressionRequest, int> typePairCount, LetPropertyMaps letPropertyMaps)
            {
                var expression = result.ResolutionExpression;

                //if (propertyMap.DestinationType != expression.Type)
                //{
                //    var destinationListType = ElementTypeHelper.GetElementType(propertyMap.DestinationType);
                //    var sourceListType = ElementTypeHelper.GetElementType(propertyMap.SourceType);
                //    var listTypePair = new ExpressionRequest(sourceListType, destinationListType, request.MembersToExpand, request);
                //    var transformedExpressions = configuration.ExpressionBuilder.CreateMapExpression(listTypePair, typePairCount, letPropertyMaps.New());
                //    if (transformedExpressions == null) return null;
                //    expression = transformedExpressions.Aggregate(expression, (source, lambda) => Select(source, lambda));
                //}

                var destinationListType = ElementTypeHelper.GetElementType(propertyMap.DestinationType);
                var sourceListType = ElementTypeHelper.GetElementType(propertyMap.SourceType);
                var listTypePair = new ExpressionRequest(sourceListType, destinationListType, request.MembersToExpand, request);
                var transformedExpressions = configuration.ExpressionBuilder.CreateMapExpression(listTypePair, typePairCount, letPropertyMaps.New());
                if (transformedExpressions == null) return null;
                expression = transformedExpressions.Aggregate(expression, (source, lambda) => Select(source, lambda));

                return Expression.Bind(propertyMap.DestinationMember, expression);
            }

            private static Expression Select(Expression source, LambdaExpression lambda)
            {
                return Expression.Call(typeof(Enumerable), "Select", new[] { lambda.Parameters[0].Type, lambda.ReturnType }, source, lambda);
            }

            public static void InsertTo(List<IExpressionBinder> binders) =>
                binders.Insert(0, new GenericEnumerableExpressionBinder());
        }
    }
}
