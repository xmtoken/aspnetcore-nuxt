using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// <see cref="ModelStateDictionary"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class ModelStateDictionaryExtensions
    {
        /// <inheritdoc cref="ModelStateDictionary.AddModelError(string, string)"/>
        public static void AddModelError(this ModelStateDictionary modelState, string errorMessage)
            => modelState.AddModelError(key: string.Empty, errorMessage);
    }
}
