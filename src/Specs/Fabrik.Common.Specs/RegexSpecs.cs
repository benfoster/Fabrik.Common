using Machine.Specifications;

namespace Fabrik.Common.Specs
{   
    [Subject(typeof(RegexUtils), "SlugRegex")]
    public class RegexUtils_SlugRegex
    {
        static bool result;
            
        public class When_the_string_contains_invalid_characters
        {
            Because of = () =>
                result = RegexUtils.SlugRegex.IsMatch("This // is \\ a test?!");

            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_valid_slug
        {
            Because of = () =>
                result = RegexUtils.SlugRegex.IsMatch("this-is-a-test");

            It Should_match = ()
                => result.ShouldBeTrue();
        }

        // edge cases

        public class When_the_string_has_leading_or_trailing_whitespace
        {
            Because of = () =>
                result = RegexUtils.SlugRegex.IsMatch(" this-is-a-test ");

            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_has_leading_or_trailing_hyphens
        {
            Because of = () =>
                result = RegexUtils.SlugRegex.IsMatch("-this-is-a-test-");

            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_uppercase
        {
            Because of = () =>
                result = RegexUtils.SlugRegex.IsMatch("THIS-IS-A-TEST");

            It Should_not_match = ()
                => result.ShouldBeFalse();
        }
    }

    [Subject(typeof(RegexUtils), "UrlRegex")]
    public class RegexUtils_UrlRegex
    {
        static bool result;

        public class When_the_string_is_not_a_valid_URL
        {
            Because of = ()
                => result = RegexUtils.UrlRegex.IsMatch("http://domain.com/some/file!.html"); // contains "!"

            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_relative_URL
        {
            Because of = ()
                => result = RegexUtils.UrlRegex.IsMatch("some/file.html");
            
            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_valid_absolute_URL
        {
            Because of = ()
                => result = RegexUtils.UrlRegex.IsMatch("http://domain.com/some/file.html");

            It Should_match = ()
                => result.ShouldBeTrue();
        }
    }

    [Subject(typeof(RegexUtils), "IPAddressRegex")]
    public class RegexUtils_IPAddressRegex 
    {
        static bool result;

        public class When_the_string_is_not_a_valid_IP_address
        {
            Because of = ()
                => result = RegexUtils.IPAddressRegex.IsMatch("256.60.124.136"); // (the first group must be “25″ and a number between zero and five)
            
            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_valid_IP_address
        {
            Because of = ()
                => result = RegexUtils.IPAddressRegex.IsMatch("73.60.124.136");
            
            It Should_match = ()
                => result.ShouldBeTrue();
        }
    }

    [Subject(typeof(RegexUtils), "EmailRegex")]
    public class RegexUtils_EmailRegex
    {
        static bool result;

        public class When_the_string_is_not_a_valid_email_address
        {
            Because of = ()
                => result = RegexUtils.EmailRegex.IsMatch("john@doe.something"); // TLD is way too long
            
            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_valid_email_address
        {
            Because of = ()
                => result = RegexUtils.EmailRegex.IsMatch("john@doe.com");
            
            It Should_match = ()
                => result.ShouldBeTrue();
        }
    }
}
