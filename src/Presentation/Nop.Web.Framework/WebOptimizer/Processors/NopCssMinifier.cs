using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Infrastructure;
using Nop.Services.Logging;
using NUglify;
using NUglify.Css;
using WebOptimizer;

namespace Nop.Web.Framework.WebOptimizer.Processors
{
    public class NopCssMinifier : Processor
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
                var result = Uglify.Css(input, new CssSettings());

                var minified = result.Code;

                if (result.HasErrors)
                {
                    await EngineContext.Current.Resolve<ILogger>()
                        .WarningAsync($"Stylesheet minification: {key}", new(string.Join("\r\n", result.Errors)));
                }

                content[key] = minified.AsByteArray();
            }

            context.Content = content;

            return;
        }

        #endregion

    }
}