using Nancy.Validation;
using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ThursdayAfternoon.Infrastructure;
using Utilities.Core.Text;
using AntiXSS = Microsoft.Security.Application;

namespace ThursdayAfternoon.Nancy.Extensions
{
    public static class HtmlHelperExtensions
    {
        private const string InputTemplate = @"<input type=""{0}"" id=""{1}"" name=""{2}"" value=""{3}"" class=""{4}"" placeholder=""{5}"" tabindex=""{6}"" />";
        private const string TextAreaTemplate = @"<textarea id=""{0}"" name=""{1}"" class=""{3}"" cols=""{4}"" rows=""{5}"" tabindex=""{6}"">{2}</textarea>";

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
            return TextBox(htmlHelper, propertyName, className, placeholder, null);
        }
        
        public static IHtmlString TextBox<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className, string placeholder, int? tabIndex)
        {
            return InputHelper(htmlHelper, "text", propertyName, htmlHelper.GetValueForProperty(propertyName), className, placeholder, tabIndex);
        }
        
        public static IHtmlString TextArea<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName)
        {
            return TextArea(htmlHelper, propertyName, string.Empty, 10, 5, null);
        }

        public static IHtmlString TextArea<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className)
        {
            return TextArea(htmlHelper, propertyName, className, 10, 5, null);
        }

        public static IHtmlString TextArea<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className, int columns, int rows)
        {
            return TextArea(htmlHelper, propertyName, className, columns, rows, null);
        }

        public static IHtmlString TextArea<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className, int columns, int rows, int? tabIndex)
        {
            return TextAreaHelper(htmlHelper, propertyName, htmlHelper.GetValueForProperty(propertyName), className, columns, rows, tabIndex);
        }

        public static IHtmlString Password<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName)
        {
            return Password(htmlHelper, propertyName, string.Empty);
        }

        public static IHtmlString Password<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className)
        {
            return Password(htmlHelper, propertyName, className, null, null);
        }

        public static IHtmlString Password<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className, string placeholder)
        {
            return Password(htmlHelper, propertyName, className, placeholder, null);
        }

        public static IHtmlString Password<TModel>(this HtmlHelpers<TModel> htmlHelper, string propertyName, string className, string placeholder, int? tabIndex)
        {
            return InputHelper(htmlHelper, "password", propertyName, null, className, placeholder, tabIndex);
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

        private static IHtmlString InputHelper<TModel>(HtmlHelpers<TModel> htmlHelper, string inputType, string propertyName, string value, string className, string placeholder, int? tabIndex)
        {
            bool hasError = htmlHelper.GetErrorsForProperty(propertyName).Any();
            string cssClass = hasError ? "{0} {1}".With(className, "error").Trim() : className;
            return new NonEncodedHtmlString(InputTemplate.With(inputType, propertyName, propertyName, value, cssClass, placeholder, tabIndex));
        }

        private static IHtmlString TextAreaHelper<TModel>(HtmlHelpers<TModel> htmlHelper, string propertyName, string value, string className, int columns, int rows, int? tabIndex)
        {
            bool hasError = htmlHelper.GetErrorsForProperty(propertyName).Any();
            string cssClass = hasError ? "{0} {1}".With(className, "error").Trim() : className;
            return new NonEncodedHtmlString(TextAreaTemplate.With(propertyName, propertyName, value, cssClass, columns, rows, tabIndex));
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
                        value = val.ToCommaSeperatedString();
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
            ModelValidationResult validationResult = htmlHelper.RenderContext.Context.ModelValidationResult;
            if (validationResult.IsValid)
            {
                return Enumerable.Empty<ModelValidationError>();
            }

            var errorsForField = validationResult.Errors.Where(x => x.MemberNames.Any(y => y.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)));
            return errorsForField;
        }
    }
}
