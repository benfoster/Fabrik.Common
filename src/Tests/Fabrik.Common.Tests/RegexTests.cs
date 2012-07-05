using Machine.Specifications;

namespace Fabrik.Common.Tests
{   
    [Subject("SlugRegex")]
    public class SlugRegex
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

    [Subject("UrlRegex")]
    public class UrlRegex
    {
        static bool result;

        public class When_the_string_is_not_a_valid_url
        {
            Because of = ()
                => result = RegexUtils.UrlRegex.IsMatch("http://domain.com/some/file!.html"); // contains "!"

            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_relative_url
        {
            Because of = ()
                => result = RegexUtils.UrlRegex.IsMatch("some/file.html");
            
            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_valid_absolute_url
        {
            Because of = ()
                => result = RegexUtils.UrlRegex.IsMatch("http://domain.com/some/file.html");

            It Should_match = ()
                => result.ShouldBeTrue();
        }
    }

    [Subject("IPAddressRegex")]
    public class IPAddressRegex 
    {
        static bool result;

        public class When_the_string_is_not_a_valid_ip
        {
            Because of = ()
                => result = RegexUtils.IPAddressRegex.IsMatch("256.60.124.136"); // (the first group must be “25″ and a number between zero and five)
            
            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_valid_ip
        {
            Because of = ()
                => result = RegexUtils.IPAddressRegex.IsMatch("73.60.124.136");
            
            It Should_match = ()
                => result.ShouldBeTrue();
        }
    }

    [Subject("EmailRegex")]
    public class EmailRegex
    {
        static bool result;

        public class When_the_string_is_not_a_valid_email
        {
            Because of = ()
                => result = RegexUtils.EmailRegex.IsMatch("john@doe.something"); // TLD is way too long
            
            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_a_valid_email
        {
            Because of = ()
                => result = RegexUtils.EmailRegex.IsMatch("john@doe.com");
            
            It Should_match = ()
                => result.ShouldBeTrue();
        }
    }
}
