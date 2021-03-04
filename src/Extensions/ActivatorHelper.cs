using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// インスタンスの作成に関する機能を提供します。
    /// </summary>
    public static class ActivatorHelper
    {
        /// <summary>
        /// パラメーターを 1 つ持つコンストラクターのキャッシュを表します。
        /// </summary>
        private static readonly ConcurrentDictionary<(Type InstanceType, Type Arg1Type), Func<object[], object>> Cache1 = new();

        /// <summary>
        /// パラメーターを 2 つ持つコンストラクターのキャッシュを表します。
        /// </summary>
        private static readonly ConcurrentDictionary<(Type InstanceType, Type Arg1Type, Type Arg2Type), Func<object[], object>> Cache2 = new();

        /// <summary>
        /// パラメーターを 3 つ持つコンストラクターのキャッシュを表します。
        /// </summary>
        private static readonly ConcurrentDictionary<(Type InstanceType, Type Arg1Type, Type Arg2Type, Type Arg3Type), Func<object[], object>> Cache3 = new();

        /// <summary>
        /// パラメーターを 4 つ持つコンストラクターのキャッシュを表します。
        /// </summary>
        private static readonly ConcurrentDictionary<(Type InstanceType, Type Arg1Type, Type Arg2Type, Type Arg3Type, Type Arg4Type), Func<object[], object>> Cache4 = new();

        /// <summary>
        /// パラメーターを 5 つ持つコンストラクターのキャッシュを表します。
        /// </summary>
        private static readonly ConcurrentDictionary<(Type InstanceType, Type Arg1Type, Type Arg2Type, Type Arg3Type, Type Arg4Type, Type Arg5Type), Func<object[], object>> Cache5 = new();

        /// <summary>
        /// 指定された型のコンストラクターを取得します。
        /// </summary>
        /// <param name="instanceType">コンストラクターを取得する型。</param>
        /// <param name="arg1Type">コンストラクターのパラメーターの型。</param>
        /// <returns>コンストラクター。</returns>
        public static Func<object[], object> GetConstructor(Type instanceType, Type arg1Type)
            => Cache1.GetOrAdd((instanceType, arg1Type),
                x => GetConstructorCore(x.InstanceType, new[] { x.Arg1Type }));

        /// <summary>
        /// 指定された型のコンストラクターを取得します。
        /// </summary>
        /// <param name="instanceType">コンストラクターを取得する型。</param>
        /// <param name="arg1Type">コンストラクターの第 1 パラメーターの型。</param>
        /// <param name="arg2Type">コンストラクターの第 2 パラメーターの型。</param>
        /// <returns>コンストラクター。</returns>
        public static Func<object[], object> GetConstructor(Type instanceType, Type arg1Type, Type arg2Type)
            => Cache2.GetOrAdd((instanceType, arg1Type, arg2Type),
                x => GetConstructorCore(x.InstanceType, new[] { x.Arg1Type, x.Arg2Type }));

        /// <summary>
        /// 指定された型のコンストラクターを取得します。
        /// </summary>
        /// <param name="instanceType">コンストラクターを取得する型。</param>
        /// <param name="arg1Type">コンストラクターの第 1 パラメーターの型。</param>
        /// <param name="arg2Type">コンストラクターの第 2 パラメーターの型。</param>
        /// <param name="arg3Type">コンストラクターの第 3 パラメーターの型。</param>
        /// <returns>コンストラクター。</returns>
        public static Func<object[], object> GetConstructor(Type instanceType, Type arg1Type, Type arg2Type, Type arg3Type)
            => Cache3.GetOrAdd((instanceType, arg1Type, arg2Type, arg3Type),
                x => GetConstructorCore(x.InstanceType, new[] { x.Arg1Type, x.Arg2Type, x.Arg3Type }));

        /// <summary>
        /// 指定された型のコンストラクターを取得します。
        /// </summary>
        /// <param name="instanceType">コンストラクターを取得する型。</param>
        /// <param name="arg1Type">コンストラクターの第 1 パラメーターの型。</param>
        /// <param name="arg2Type">コンストラクターの第 2 パラメーターの型。</param>
        /// <param name="arg3Type">コンストラクターの第 3 パラメーターの型。</param>
        /// <param name="arg4Type">コンストラクターの第 4 パラメーターの型。</param>
        /// <returns>コンストラクター。</returns>
        public static Func<object[], object> GetConstructor(Type instanceType, Type arg1Type, Type arg2Type, Type arg3Type, Type arg4Type)
            => Cache4.GetOrAdd((instanceType, arg1Type, arg2Type, arg3Type, arg4Type),
                x => GetConstructorCore(x.InstanceType, new[] { x.Arg1Type, x.Arg2Type, x.Arg3Type, x.Arg4Type }));

        /// <summary>
        /// 指定された型のコンストラクターを取得します。
        /// </summary>
        /// <param name="instanceType">コンストラクターを取得する型。</param>
        /// <param name="arg1Type">コンストラクターの第 1 パラメーターの型。</param>
        /// <param name="arg2Type">コンストラクターの第 2 パラメーターの型。</param>
        /// <param name="arg3Type">コンストラクターの第 3 パラメーターの型。</param>
        /// <param name="arg4Type">コンストラクターの第 4 パラメーターの型。</param>
        /// <param name="arg5Type">コンストラクターの第 5 パラメーターの型。</param>
        /// <returns>コンストラクター。</returns>
        public static Func<object[], object> GetConstructor(Type instanceType, Type arg1Type, Type arg2Type, Type arg3Type, Type arg4Type, Type arg5Type)
            => Cache5.GetOrAdd((instanceType, arg1Type, arg2Type, arg3Type, arg4Type, arg5Type),
                x => GetConstructorCore(x.InstanceType, new[] { x.Arg1Type, x.Arg2Type, x.Arg3Type, x.Arg4Type, x.Arg5Type }));

        /// <summary>
        /// 指定された型のコンストラクターを取得します。
        /// </summary>
        /// <param name="instanceType">コンストラクターを取得する型。</param>
        /// <param name="parameterTypes">コンストラクターのパラメーターの型のコレクション。</param>
        /// <returns>コンストラクター。</returns>
        private static Func<object[], object> GetConstructorCore(Type instanceType, Type[] parameterTypes)
        {
            var parameterExpression = Expression.Parameter(typeof(object[]), "args");
            var parameterExpressions = parameterTypes
                .Select((parameterType, index) =>
                {
                    var indexExpression = Expression.ArrayIndex(parameterExpression, Expression.Constant(index));
                    return Expression.Convert(indexExpression, parameterType);
                })
                .ToArray();
            var constructorBindingAttrs = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var constructor = instanceType.GetConstructor(constructorBindingAttrs, Type.DefaultBinder, parameterTypes, modifiers: null);
            var newExpression = Expression.New(constructor, parameterExpressions);
            return Expression.Lambda<Func<object[], object>>(Expression.Convert(newExpression, typeof(object)), parameterExpression).Compile();
        }
    }
}
