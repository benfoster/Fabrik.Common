using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// A validation attribute to ensure that a boolean property is set to true.
    /// </summary>
    /// <remarks>
    /// Supports client-side validation.
    /// </remarks>
    public class MandatoryAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            return (!(value is bool) || (bool)value);
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationType = "mandatory";
            yield return rule;
        }
    }
}
