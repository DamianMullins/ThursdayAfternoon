using System.Collections;
using System.Reflection;
using Nancy.Validation;
using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Utilities.Core.Text;
using AntiXSS = Microsoft.Security.Application;

namespace ThursdayAfternoon.Nancy.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString CheckBox<T>(this HtmlHelpers<T> helper, string name, bool value)
        {
            var checkBoxBuilder = new StringBuilder();

            checkBoxBuilder.Append(@"<input data-name=""");
            checkBoxBuilder.Append(AntiXSS.Encoder.HtmlAttributeEncode(name));
            checkBoxBuilder.Append(@""" id=""");
            checkBoxBuilder.Append(AntiXSS.Encoder.HtmlAttributeEncode(name));
            checkBoxBuilder.Append(@""" type=""checkbox""");
            checkBoxBuilder.Append(value ? @" checked=""checked"" />" : " />");

            //checkBoxBuilder.Append(@"<input name=""");
            //checkBoxBuilder.Append(AntiXSS.Encoder.HtmlAttributeEncode(name));
            //checkBoxBuilder.Append(@""" type=""hidden"" value=""");
            //checkBoxBuilder.Append(value.ToString().ToLowerInvariant());
            //checkBoxBuilder.Append(@""" />");

            return new NonEncodedHtmlString(checkBoxBuilder.ToString());
        }

        public static IHtmlString ValidationSummary<TModel>(this HtmlHelpers<TModel> htmlHelper)
        {
            var validationResult = htmlHelper.RenderContext.Context.ModelValidationResult;
            if (validationResult.IsValid)
            {
                return new NonEncodedHtmlString(string.Empty);
            }

            var summaryBuilder = new StringBuilder();
            summaryBuilder.Append(@"<div class=""alert alert-danger""><ul class=""validation-summary-errors"">");
            foreach (var modelValidationError in validationResult.Errors)
            {
                foreach (var memberName in modelValidationError.MemberNames)
                {
                    summaryBuilder.AppendFormat("<li>{0}</li>", modelValidationError.GetMessage(memberName));
                }
            }
            summaryBuilder.Append(@"</ul></div>");

            return new NonEncodedHtmlString(summaryBuilder.ToString());
        }

        public static IHtmlString TextBox<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName)
        {
            return TextBox(htmlHelper, propertyName, string.Empty);
        }

        public static IHtmlString TextBox<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className)
        {
            return TextBox(htmlHelper, propertyName, className, null);
        }

        public static IHtmlString TextBox<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className, string placeholder)
        {
            return InputHelper(htmlHelper, "text", propertyName, htmlHelper.GetValueForProperty(propertyName), className, placeholder);
        }

        public static IHtmlString Password<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName)
        {
            return Password(htmlHelper, propertyName, string.Empty);
        }

        public static IHtmlString Password<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className)
        {
            return Password(htmlHelper, propertyName, className, null);
        }

        public static IHtmlString Password<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className, string placeholder)
        {
            return InputHelper(htmlHelper, "password", propertyName, null, className, placeholder);
        }

        public static IHtmlString Hidden<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName)
        {
            const string hiddenInputTemplate = @"<input type=""hidden"" id=""{0}"" name=""{1}"" value=""{2}"" />";

            return new NonEncodedHtmlString(hiddenInputTemplate.With(propertyName, propertyName, htmlHelper.GetValueForProperty(propertyName)));
        }

        public static IHtmlString DisplayNoneIf<TModel>(this HtmlHelpers<TModel> htmlHelper, Expression<Func<TModel, bool>> expression)
        {
            if (expression.Compile()(htmlHelper.Model))
                return new NonEncodedHtmlString(@" style=""display:none;"" ");

            return NonEncodedHtmlString.Empty;
        }

        private const string InputTemplate = @"<input type=""{0}"" id=""{1}"" name=""{2}"" value=""{3}"" class=""{4}"" placeholder=""{5}"" />";
        private static IHtmlString InputHelper<TModel>(HtmlHelpers<TModel> htmlHelper, string inputType, string propertyName, string value, string className, string placeholder)
        {
            bool hasError = htmlHelper.GetErrorsForProperty(propertyName).Any();

            return new NonEncodedHtmlString(InputTemplate.With(inputType, propertyName, propertyName, value, hasError ? "{0} {1}".With(className, "error").Trim() : className, placeholder));
        }

        internal static string GetValueForProperty<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName)
        {
            PropertyInfo propInfo = typeof(TModel).GetProperties().FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
            string value = null;

            if (propInfo != null && htmlHelper.Model != null)
            {
                object val = propInfo.GetValue(htmlHelper.Model);
                if (val != null)
                {
                    // Convert arrays to comma-seperated string
                    if (propInfo.PropertyType.IsArray)
                    {
                        var arrValues = ((IEnumerable)val).Cast<object>()
                                 .Select(x => x.ToString())
                                 .ToArray();

                        value = String.Join(",", arrValues);
                    }
                    else
                    {
                        value = val.ToString();
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = htmlHelper.RenderContext.Context.Request.Form[propertyName];
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = htmlHelper.RenderContext.Context.Request.Query[propertyName];
            }

            return value;
        }

        internal static IEnumerable<ModelValidationError> GetErrorsForProperty<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName)
        {
            var validationResult = htmlHelper.RenderContext.Context.ModelValidationResult;
            if (validationResult.IsValid)
            {
                return Enumerable.Empty<ModelValidationError>();
            }

            var errorsForField = validationResult.Errors.Where(x => x.MemberNames.Any(y => y.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)));
            return errorsForField;
        }
    }
}
