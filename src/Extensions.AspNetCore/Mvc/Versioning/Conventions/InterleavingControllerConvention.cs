using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.Versioning.Conventions
{
    /// <summary>
    /// すべてのアクションで最新の API バージョンまでを自動でサポートします。
    /// </summary>
    /// <remarks>
    /// デフォルトの API バージョンを 1.0、
    /// Action-A に API バージョンの指定なし、
    /// Action-B に API バージョン 1.1、
    /// Action-C に API バージョン 2.0 を指定した場合、
    /// Action-A は 1.0, 1.1, 2.0、
    /// Action-B は 1.1, 2.0、
    /// Action-C は 2.0 の API バージョンをそれぞれ自動でサポートします。
    /// </remarks>
    public class InterleavingControllerConvention : IControllerConvention
    {
        /// <summary>
        /// 定義されている API バージョンのコレクションを表します。
        /// </summary>
        private readonly IEnumerable<ApiVersion> Versions;

        /// <summary>
        /// <see cref="InterleavingControllerConvention"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="defaultApiVersion">デフォルトの API バージョン。</param>
        /// <param name="assemblies">API のコントローラーをスキャンするアセンブリのコレクション。</param>
        public InterleavingControllerConvention(ApiVersion defaultApiVersion, params Assembly[] assemblies)
        {
            var versions = new List<ApiVersion>() { defaultApiVersion };
            var controllerTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(type => type.IsClass && type.IsConcrete() && typeof(ControllerBase).IsAssignableFrom(type));
            foreach (var controllerType in controllerTypes)
            {
                versions.AddRange(controllerType.GetCustomAttributes<ApiVersionAttribute>().SelectMany(x => x.Versions));
                foreach (var actionMethod in controllerType.GetMethods().Where(x => x.GetCustomAttribute<NonActionAttribute>() is null))
                {
                    versions.AddRange(actionMethod.GetCustomAttributes<ApiVersionAttribute>().SelectMany(x => x.Versions));
                }
            }
            Versions = versions
                .OrderBy(version => version)
                .Select(x => x.ToString())
                .Distinct()
                .Select(version => ApiVersion.Parse(version))
                .ToArray();
        }

        /// <inheritdoc/>
        public bool Apply(IControllerConventionBuilder controller, ControllerModel controllerModel)
        {
            foreach (var action in controllerModel.Actions)
            {
                if (action.Attributes.OfType<ApiVersionNeutralAttribute>().Any())
                {
                    continue;
                }
                var version = action.Attributes.OfType<ApiVersionAttribute>().SelectMany(x => x.Versions).OrderBy(x => x).LastOrDefault();
                if (version is null)
                {
                    controller.Action(action.ActionMethod).HasApiVersions(Versions);
                }
                else
                {
                    controller.Action(action.ActionMethod).HasApiVersions(Versions.Where(x => x > version));
                }
            }
            return true;
        }
    }
}
