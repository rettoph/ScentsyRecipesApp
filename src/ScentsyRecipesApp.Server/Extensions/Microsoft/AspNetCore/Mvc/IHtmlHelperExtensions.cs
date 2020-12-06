using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScentsyRecipesApp.Server.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Extensions.Microsoft.AspNetCore.Mvc
{
    public static class IHtmlHelperExtensions
    {
        /// <summary>
        /// Automatic help to include the <see cref="InputComponent"/>
        /// content.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="modelStateEntry"><see cref="InputComponent.ModelStateEntry"/></param>
        /// <param name="name"><see cref="InputComponent.Name"/></param>
        /// <param name="value"><see cref="InputComponent.Value"/></param>
        /// <param name="placeholder"><see cref="InputComponent.Placeholder"/></param>
        /// <param name="helpText"><see cref="InputComponent.HelpText"/></param>
        /// <param name="method"><see cref="InputComponent.Method"/></param>
        /// <returns></returns>
        public static IHtmlContent RenderInputComponent(
            this IHtmlHelper html,
            ModelStateEntry modelStateEntry,
            String name,
            String value,
            String placeholder,
            String helpText,
            String method,
            String type = "text")
        {
            return html.RenderComponentAsync<InputComponent>(
                RenderMode.ServerPrerendered,
                new
                {
                    ModelStateEntry = modelStateEntry,
                    Name = name,
                    Value = value ?? "",
                    Placeholder = placeholder,
                    HelpText = helpText,
                    Method = method,
                    Type = type
                }).GetAwaiter().GetResult();
        }
    }
}
