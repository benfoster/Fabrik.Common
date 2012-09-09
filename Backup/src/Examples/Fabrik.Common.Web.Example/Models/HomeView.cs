using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Fabrik.Common.Web.Example.Models
{
    public class HomeView : HomeCommand
    {
        public string Message { get; set; }
        public SelectList SubscriptionSourcesList { get; set; }
    }

    public class HomeCommand : IValidatableObject
    {
        [Required]
        [Display(Description = "Your Name")]
        public string Name { get; set; }

        [Required]
        [Description("You can only apply if you are 18 or over. <a href='#'>Terms</a>.")]
        [Range(18, int.MaxValue, ErrorMessage = "You must be over 18 or over to subscribe")]
        public int Age { get; set; }

        [DisplayName("Subscription")]
        public SubscriptionType SubscriptionType { get; set; }

        [DisplayName("How did you hear about us?")]
        public IEnumerable<string> SubscriptionSources { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age < 60 && SubscriptionType == SubscriptionType.GoldSubscription)
                yield return new ValidationResult(
                    "Sorry, Gold Subscription are only available if you are over 60.", 
                    new[] { "SubscriptionType" });
        }
    }

    public enum SubscriptionType
    {
        BronzeSubscription,
        SilverSubscription,
        GoldSubscription
    }
}