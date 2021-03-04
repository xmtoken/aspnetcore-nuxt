using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace AspNetCoreNuxt.Extensions.Linq.Emit
{
    /// <summary>
    /// ランタイム時の動的な型ビルダーを表します。
    /// </summary>
    public static class RuntimeTypeBuilder
    {
        /// <summary>
        /// <see cref="AssemblyName"/> オブジェクトを表します。
        /// </summary>
        private static readonly AssemblyName DynamicAssemblyName = new AssemblyName(typeof(RuntimeTypeBuilder).FullName);

        /// <summary>
        /// <see cref="ModuleBuilder"/> オブジェクトを表します。
        /// </summary>
        private static readonly ModuleBuilder DynamicModuleBuilder = AssemblyBuilder
                .DefineDynamicAssembly(DynamicAssemblyName, AssemblyBuilderAccess.Run)
                .DefineDynamicModule(DynamicAssemblyName.Name);

        /// <summary>
        /// 動的に作成した型のキャッシュを表します。
        /// </summary>
        private static readonly ConcurrentDictionary<string, Type> TypeCache = new();

        /// <summary>
        /// 指定されたプロパティを含むクラスの型を動的に作成します。
        /// </summary>
        /// <param name="properties">クラスに含めるプロパティの名前と型のコレクション。</param>
        /// <returns>動的に作成したクラスの型。</returns>
        public static Type CreateDynamicType(IDictionary<string, Type> properties)
        {
            if (properties.Any(x => x.Key.StartsWith("#", StringComparison.Ordinal)) ||
                properties.Select(x => x.Key.ToUpperInvariant()).Distinct().Count() != properties.Count)
            {
                throw new InvalidOperationException();
            }

            var keys = properties.Select(x => $"{x.Key}:{x.Value.Name}");
            var key = $"`{string.Join("/", keys)}`";
            return TypeCache.GetOrAdd(key, _ =>
            {
                var typeAttributes = TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Serializable;
                var typeBuilder = DynamicModuleBuilder.DefineType(key, typeAttributes);
                foreach (var property in properties)
                {
                    var fieldBuilder = typeBuilder.DefineField($"#_{property.Key}", property.Value, FieldAttributes.NotSerialized | FieldAttributes.Private);
                    var propertyBuilder = typeBuilder.DefineProperty(property.Key, PropertyAttributes.None, property.Value, parameterTypes: null);
                    var propertyMethodAttributes = MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName;

                    var propertyGetterBuilder = typeBuilder.DefineMethod($"#get_{property.Key}", propertyMethodAttributes, returnType: property.Value, parameterTypes: null);
                    var propertyGetterIL = propertyGetterBuilder.GetILGenerator();
                    propertyGetterIL.Emit(OpCodes.Ldarg_0);
                    propertyGetterIL.Emit(OpCodes.Ldfld, fieldBuilder);
                    propertyGetterIL.Emit(OpCodes.Ret);
                    propertyBuilder.SetGetMethod(propertyGetterBuilder);

                    var propertySetterBuilder = typeBuilder.DefineMethod($"#set_{property.Key}", propertyMethodAttributes, returnType: null, parameterTypes: new[] { property.Value });
                    var propertySetterIL = propertySetterBuilder.GetILGenerator();
                    propertySetterIL.Emit(OpCodes.Ldarg_0);
                    propertySetterIL.Emit(OpCodes.Ldarg_1);
                    propertySetterIL.Emit(OpCodes.Stfld, fieldBuilder);
                    propertySetterIL.Emit(OpCodes.Ret);
                    propertyBuilder.SetSetMethod(propertySetterBuilder);
                }
                return typeBuilder.CreateType();
            });
        }
    }
}
