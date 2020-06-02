using CsvHelper.Configuration;

namespace AspNetCoreNuxt.Extensions.CsvHelper.DependencyInjection
{
    /// <summary>
    /// <see cref="ClassMap"/> オブジェクトを作成する機能を提供します。
    /// </summary>
    public interface ICsvClassMapFactory
    {
        /// <summary>
        /// 指定された <typeparamref name="TClassMap"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <typeparam name="TClassMap"><see cref="ClassMap"/> クラスの型。</typeparam>
        /// <returns><typeparamref name="TClassMap"/> オブジェクト。</returns>
        TClassMap CreateClassMap<TClassMap>()
            where TClassMap : ClassMap;
    }
}
