using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stubble.Core.Builders;

namespace Study2019.WebUI.Utils
{
    public static class TemplateHelper
    {
        public static string Render(string template, object obj)
        {
            var stubble = new StubbleBuilder().Build();
            return stubble.Render(template, obj);
        }

    }
}