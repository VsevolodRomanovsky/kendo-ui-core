﻿using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.Rendering.Expressions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Kendo.Mvc.Rendering
{
    public class KendoHtmlGenerator : IKendoHtmlGenerator
    {
        private readonly IActionBindingContextProvider _actionBindingContextProvider;
        private readonly IModelMetadataProvider _metadataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="KendoHtmlGenerator"/> class.
        /// </summary>
        public KendoHtmlGenerator(
            IActionBindingContextProvider actionBindingContextProvider,
            IModelMetadataProvider metadataProvider)

        {
            _actionBindingContextProvider = actionBindingContextProvider;
            _metadataProvider = metadataProvider;
        }

        public virtual TagBuilder GenerateDateInput(
            ViewContext viewContext,
            ModelMetadata metadata,
            string id,
            string name,
            object value,
            string format,
            IDictionary<string, object> htmlAttributes)
        {
            var tagBuilder = GenerateTag("input", viewContext, id, name, htmlAttributes);
            tagBuilder.MergeAttribute("type", "date");

            var fullName = tagBuilder.Attributes["name"];
            var valueParameter = FormatValue(value, format);
            var useViewData = metadata == null && value == null;
            var attributeValue = (string) GetModelStateValue(viewContext, fullName, typeof(string));
            if (attributeValue == null)
            {
                attributeValue = useViewData ? EvalString(viewContext, fullName, format) : valueParameter;
            }

            tagBuilder.MergeAttribute("value", attributeValue, true);

            // If there are any errors for a named field, we add the CSS attribute.
            ModelState modelState;
            if (viewContext.ViewData.ModelState.TryGetValue(fullName, out modelState) && modelState.Errors.Count > 0)
            {
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }

            tagBuilder.MergeAttributes(GetValidationAttributes(viewContext, metadata, name));

            return tagBuilder;
        }

        public virtual TagBuilder GenerateDateTimeInput(
            ViewContext viewContext,
            ModelMetadata metadata,
            string id,
            string name,
            object value,
            string format,
            IDictionary<string, object> htmlAttributes)
        {
            var tagBuilder = GenerateDateInput(viewContext, metadata, id, name, value, format, htmlAttributes);
            tagBuilder.MergeAttribute("type", "datetime", replaceExisting: true);

            return tagBuilder;
        }

        public virtual TagBuilder GenerateTag(
            string tagName,
            ViewContext viewContext,
            string id,
            string name,
            IDictionary<string, object> htmlAttributes)
        {
            var fullName = GetFullHtmlFieldName(viewContext, name);
            if (string.IsNullOrEmpty(fullName))
            {
                throw new InvalidOperationException(Resources.Exceptions.NameCannotBeBlank);
            }

            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.MergeAttribute("id", id);
            tagBuilder.MergeAttribute("name", fullName);
            tagBuilder.MergeAttributes(htmlAttributes, replaceExisting: true);

            return tagBuilder;
        }

        // Only render attributes if client-side validation is enabled, and then only if we've
        // never rendered validation for a field with this name in this form.
        protected virtual IDictionary<string, object> GetValidationAttributes(
            ViewContext viewContext,
            ModelMetadata metadata,
            string name)
        {
            var formContext = viewContext.ClientValidationEnabled ? viewContext.FormContext : null;
            if (formContext == null)
            {
                return null;
            }

            var fullName = GetFullHtmlFieldName(viewContext, name);
            if (formContext.RenderedField(fullName))
            {
                return null;
            }

            formContext.RenderedField(fullName, true);
            var clientRules = GetClientValidationRules(viewContext, metadata, name);

            return UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientRules);
        }

        protected virtual IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ViewContext viewContext,
            ModelMetadata metadata,
            string name)
        {
            var actionBindingContext = _actionBindingContextProvider.GetActionBindingContextAsync(viewContext).Result;
            metadata = metadata ??
                ExpressionMetadataProvider.FromStringExpression(name, viewContext.ViewData, _metadataProvider);

            return actionBindingContext
                .ValidatorProvider
                .GetValidators(metadata)
                .OfType<IClientModelValidator>()
                .SelectMany(v => v.GetClientValidationRules(
                    new ClientModelValidationContext(metadata, _metadataProvider)));
        }

        private static string GetFullHtmlFieldName(ViewContext viewContext, string name)
        {
            var fullName = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            return fullName;
        }

        private static object GetModelStateValue(ViewContext viewContext, string key, Type destinationType)
        {
            ModelState modelState;
            if (viewContext.ViewData.ModelState.TryGetValue(key, out modelState) && modelState.Value != null)
            {
                return modelState.Value.ConvertTo(destinationType, culture: null);
            }

            return null;
        }

        private static string EvalString(ViewContext viewContext, string key)
        {
            return Convert.ToString(viewContext.ViewData.Eval(key), CultureInfo.CurrentCulture);
        }

        private static string EvalString(ViewContext viewContext, string key, string format)
        {
            return Convert.ToString(viewContext.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }

        private static string FormatValue(object value, string format)
        {
            return ViewDataDictionary.FormatValue(value, format);
        }
    }
}
