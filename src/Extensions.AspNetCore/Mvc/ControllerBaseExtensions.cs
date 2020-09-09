using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc
{
    /// <summary>
    /// <see cref="ControllerBase"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class ControllerBaseExtensions
    {
        /// <summary>
        /// 指定されたエラーメッセージの検証エラーを含んだ <see cref="StatusCodes.Status400BadRequest"/> を作成する <see cref="IActionResult"/> オブジェクトを返します。
        /// </summary>
        /// <param name="controller"><see cref="ControllerBase"/> オブジェクト。</param>
        /// <param name="errorMessage">検証エラーに含めるエラーメッセージ。</param>
        /// <returns><see cref="IActionResult"/> オブジェクト。</returns>
        public static IActionResult ValidationProblemWith(this ControllerBase controller, string errorMessage)
        {
            controller.ModelState.AddModelError(errorMessage);
            return controller.ValidationProblem(controller.ModelState);
        }
    }
}
