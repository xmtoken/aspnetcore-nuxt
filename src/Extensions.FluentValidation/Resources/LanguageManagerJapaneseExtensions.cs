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
            languageManager.AddTranslation(Culture, "EmailValidator", "{PropertyName}は有効なメールアドレスではありません。");
            languageManager.AddTranslation(Culture, nameof(EnumValidator), "{PropertyName}は有効な値ではありません。");
            languageManager.AddTranslation(Culture, nameof(GreaterThanOrEqualValidator), "{PropertyName}は{ComparisonValue}以上の値を入力してください。");
            languageManager.AddTranslation(Culture, nameof(InclusiveBetweenValidator), "{PropertyName}は{From}から{To}までの値を入力してください。");
            languageManager.AddTranslation(Culture, nameof(LengthValidator), "{PropertyName}は{MaxLength}文字で入力してください。");
            languageManager.AddTranslation(Culture, nameof(LessThanOrEqualValidator), "{PropertyName}は{ComparisonValue}以下の値を入力してください。");
            languageManager.AddTranslation(Culture, nameof(MaximumLengthValidator), "{PropertyName}は{MaxLength}文字以下で入力してください。");
            languageManager.AddTranslation(Culture, nameof(MinimumLengthValidator), "{PropertyName}は{MinLength}文字以上で入力してください。");
            languageManager.AddTranslation(Culture, nameof(NotNullValidator), "{PropertyName}を入力してください。");
        }
    }
}
