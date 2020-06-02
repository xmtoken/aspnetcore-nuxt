using FluentValidation;
using FluentValidation.Resources;

namespace AspNetCoreNuxt.Extensions.FluentValidation.Resources
{
    /// <summary>
    /// <see cref="ILanguageManager"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class LanguageManagerExtensions
    {
        /// <summary>
        /// 指定されたカルチャーのバリデーターに既定のエラーメッセージを追加します。
        /// </summary>
        /// <typeparam name="T">既定のエラーメッセージを追加するバリデーターを表す <see cref="IValidator"/> インターフェイスを実装するクラスの型。</typeparam>
        /// <param name="languageManager"><see cref="LanguageManager"/> オブジェクト。</param>
        /// <param name="language">カルチャー。</param>
        /// <param name="message">エラーメッセージ。</param>
        /// <returns><see cref="LanguageManager"/> オブジェクト。</returns>
        public static LanguageManager AddTranslation<T>(this LanguageManager languageManager, string language, string message)
        {
            languageManager.AddTranslation(language, typeof(T).Name, message);
            return languageManager;
        }
    }
}
