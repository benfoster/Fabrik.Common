using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Fabrik.Common.Web.Example.Models
{
    public class HomeView : HomeCommand
    {
        public string Message { get; set; }
        public SelectList SubscriptionTypes { get; set; }
    }

    public class HomeCommand : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(18, int.MaxValue, ErrorMessage = "You must be over 18 or over to subscribe")]
        public int Age { get; set; }

        public string SubscriptionType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age < 60 && SubscriptionType == "Gold")
                yield return new ValidationResult(
                    "Sorry, Gold Subscription are only available if you are over 60.", 
                    new[] { "SubscriptionType" });
        }
    }
}