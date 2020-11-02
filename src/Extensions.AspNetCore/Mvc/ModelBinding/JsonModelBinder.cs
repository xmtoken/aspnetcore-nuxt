using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// 文字列を JSON オブジェクトとしてバインドするモデルバインダーを表します。
    /// </summary>
    public class JsonModelBinder : IModelBinder
    {
        /// <summary>
        /// <see cref="JsonSerializerSettings"/> オブジェクトを表します。
        /// </summary>
        private readonly JsonSerializerSettings Settings;

        /// <summary>
        /// <see cref="JsonModelBinder"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="options"><see cref="JsonSerializerSettings"/> オブジェクト。</param>
        public JsonModelBinder(IOptions<MvcNewtonsoftJsonOptions> options)
        {
            Settings = options.Value.SerializerSettings;
        }

        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelName = string.Empty;
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var value = valueProviderResult.FirstValue;
                var model = JsonConvert.DeserializeObject(value, bindingContext.ModelType, Settings);
                if (model != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(model);
                }
            }
            return Task.CompletedTask;
        }
    }
}
