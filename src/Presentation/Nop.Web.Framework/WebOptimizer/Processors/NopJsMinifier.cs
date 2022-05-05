using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Infrastructure;
using Nop.Services.Logging;
using NUglify;
using NUglify.JavaScript;
using WebOptimizer;

namespace Nop.Web.Framework.WebOptimizer.Processors
{
    public class NopJsMinifier : Processor
    {
        #region Methods

        public override async Task ExecuteAsync(IAssetContext context)
        {
            var content = new Dictionary<string, byte[]>();

            foreach (var key in context.Content.Keys)
            {
                if (key.EndsWith(".min"))
                {
                    content[key] = context.Content[key];
                    continue;
                }

                var input = context.Content[key].AsString();
                var result = Uglify.Js(input, new CodeSettings());

                var minified = result.Code;

                if (result.HasErrors)
                {
                    await EngineContext.Current.Resolve<ILogger>()
                        .WarningAsync($"JavaScript minification: {key}", new(string.Join("\r\n", result.Errors)));
                }

                content[key] = minified.AsByteArray();
            }

            context.Content = content;

            return;
        }

        #endregion

    }
}