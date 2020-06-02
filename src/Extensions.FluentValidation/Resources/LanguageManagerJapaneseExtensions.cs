using FluentValidation.Resources;
using FluentValidation.Validators;

namespace AspNetCoreNuxt.Extensions.FluentValidation.Resources
{
    /// <summary>
    /// <see cref="ILanguageManager"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class LanguageManagerJapaneseExtensions
    {
        /// <summary>
        /// カルチャーを表します。
        /// </summary>
        private const string Culture = "ja";

        /// <summary>
        /// 日本語の既定のエラーメッセージを追加します。
        /// </summary>
        /// <param name="source"><see cref="ILanguageManager"/> オブジェクト。</param>
        public static void AddJapaneseTranslations(this ILanguageManager source)
        {
            var languageManager = (LanguageManager)source;
            languageManager
                .AddTranslation<EmailValidator>(Culture, "{PropertyName}は有効なメールアドレスではありません。")
                .AddTranslation<GreaterThanOrEqualValidator>(Culture, "{PropertyName}は{ComparisonValue}以上の値を入力してください。")
                .AddTranslation<InclusiveBetweenValidator>(Culture, "{PropertyName}は{From}から{To}までの値を入力してください。")
                .AddTranslation<LengthValidator>(Culture, "{PropertyName}は{MaxLength}文字で入力してください。")
                .AddTranslation<LessThanOrEqualValidator>(Culture, "{PropertyName}は{ComparisonValue}以下の値を入力してください。")
                .AddTranslation<MaximumLengthValidator>(Culture, "{PropertyName}は{MaxLength}文字以下で入力してください。")
                .AddTranslation<MinimumLengthValidator>(Culture, "{PropertyName}は{MinLength}文字以上で入力してください。")
                .AddTranslation<NotNullValidator>(Culture, "{PropertyName}を入力してください。");
        }
    }
}
