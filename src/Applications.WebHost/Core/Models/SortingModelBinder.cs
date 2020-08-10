using AspNetCoreNuxt.Extensions.AspNetCore;
using AspNetCoreNuxt.Extensions.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Models
{
    /// <summary>
    /// <see cref="Sorting"/> クラスのモデルバインダーを表します。
    /// </summary>
    public class SortingModelBinder : IModelBinder
    {
        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var sortQuery = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            if (sortQuery == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var sortParameters = sortQuery.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var property = sortParameters.Length >= 1 ? sortParameters[0].Trim() : null;
            var direction = sortParameters.Length >= 2 ? sortParameters[1].Trim() : null;
            if (direction != null)
            {
                direction
                    = direction.Equals("asc", StringComparison.OrdinalIgnoreCase) ? nameof(SortDirection.Ascending)
                    : direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? nameof(SortDirection.Descending)
                    : direction;
            }

            var sorting = new Sorting()
            {
                SortPropertyName = property,
                SortDirection = QueryStringConverter.ConvertToEnum<SortDirection>(direction) ?? default,
            };

            bindingContext.Result = ModelBindingResult.Success(sorting);
            return Task.CompletedTask;
        }
    }
}
