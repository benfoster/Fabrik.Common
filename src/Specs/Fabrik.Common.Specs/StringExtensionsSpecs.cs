using System;
using Machine.Specifications;

namespace Fabrik.Common.Specs
{
    [Subject(typeof(StringExtensions), "IsNullOrEmpty")]
    public class String_IsNullOrEmpty
    {
        static bool result;
            
        public class When_the_string_is_null
        {
            Because of = ()
                => result = ((string)null).IsNullOrEmpty();

            It Should_return_true = ()
                => result.ShouldBeTrue();
        }

        public class When_the_string_is_empty
        {
            Because of = ()
                => result = "".IsNullOrEmpty();

            It Should_return_true = ()
                => result.ShouldBeTrue();
        }

        public class When_the_string_is_not_null_or_empty
        {
            Because of = ()
                => result = "test".IsNullOrEmpty();

            It Should_return_false = ()
                => result.ShouldBeFalse();
        }
    }

    [Subject(typeof(StringExtensions), "IsNotNullOrEmpty")]
    public class String_IsNotNullOrEmpty
    {
        static bool result;
            
        public class When_the_string_is_null
        {
            Because of = ()
                => result = ((string)null).IsNotNullOrEmpty();

            It Should_return_false = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_empty
        {
            Because of = ()
                => result = "".IsNotNullOrEmpty();

            It Should_return_false = ()
                => result.ShouldBeFalse();
        }

        public class When_the_string_is_not_null_or_empty
        {
            Because of = ()
                => result = "test".IsNotNullOrEmpty();

            It Should_return_true = ()
                => result.ShouldBeTrue();
        }
    }

    [Subject(typeof(StringExtensions), "FormatWith")]
    public class String_FormatWith
    {
        static string result;

        Because of = ()
            => result = "{0} potato, {1} potato, {2} potato, four.".FormatWith("One", "two", "three");


        It Should_format_format_the_string_with_the_provided_arguments = ()
            => result.ShouldEqual("One potato, two potato, three potato, four.");
    }

    [Subject(typeof(StringExtensions), "NullIfEmpty")]
    public class String_NullIfEmpty
    {
        static string result;
            
        public class When_the_string_is_null
        {
            Because of = ()
                => result = ((string)null).NullIfEmpty();

            It Should_return_null = ()
                => result.ShouldBeNull();
        }

        public class When_the_string_is_empty
        {
            Because of = ()
                => result = "".NullIfEmpty();

            It Should_return_null = ()
                => result.ShouldBeNull();
        }

        public class When_the_string_is_not_null_or_empty
        {
            Because of = ()
                => result = "test".NullIfEmpty();

            It Should_return_the_original_value = ()
                => result.ShouldEqual("test");
        }
    }

    [Subject(typeof(StringExtensions), "ToSlug")]
    public class String_ToSlug
    {
        static string result;
        static Exception exception;

        public class When_the_string_is_null
        {
            Because of = ()
                => exception = Catch.Exception(() => ((string)null).ToSlug());

            It Should_throw_ArgumentNullException = ()
                => exception.ShouldBeOfType<ArgumentNullException>();
        }
          
        public class When_it_contains_no_invalid_characters
        {
            Because of = ()
                => result = "i-am-a-valid-slug".ToSlug();

            It Should_return_the_original_value = ()
                => result.ShouldEqual("i-am-a-valid-slug");
        }

        public class When_it_contains_invalid_characters
        {
            Because of = ()
                => result = "Boom!#% // What's \\ (up) 'with' the £$price # of **fuel** these days?!".ToSlug();

            It Should_remove_them = ()
                => result.ShouldEqual("boom-whats-up-with-the-price-of-fuel-these-days");
        }

        public class When_the_string_contains_uppercase_characters
        {
            Because of = ()
                => result = "LOUD NOISES".ToSlug();

            It Should_convert_them_to_lowercase = ()
                => result.ShouldEqual("loud-noises");
        }

        // specific edge cases

        public class When_the_string_starts_or_ends_with_hyphens
        {
            Because of = ()
                => result = "- What's up?? -".ToSlug();

            It Should_remove_all_leading_and_trailing_hyphens = ()
                => result.ShouldEqual("whats-up");
        }
    }

    [Subject(typeof(StringExtensions), "ToSlug with max length")]
    public class String_ToSlug_with_max_length
    {
        static string result;
            
        public class When_the_string_is_less_than_the_max_length
        {
            Because of = ()
                => result = "This is a test".ToSlug(100);

            It Should_return_the_full_slugified_string = ()
                => result.ShouldEqual("this-is-a-test");
        }

        public class When_the_string_is_greater_than_the_max_length
        {
            Because of = ()
                => result = "This is a test".ToSlug(10);

            It Should_cut_and_trim_the_string = ()
                => result.ShouldEqual("this-is-a");
        }
    }

    [Subject(typeof(StringExtensions), "ToSlugWithSegments")]
    public class String_ToSlugWithSegments
    {
        static string result;
            
        public class When_the_string_contains_segments
        {
            Because of = ()
                => result = @"blog's/archived posts/2012\".ToSlugWithSegments();

            It Should_slugify_each_segment = ()
                => result.ShouldEqual("blogs/archived-posts/2012");
        }

        // edge cases

        public class When_the_string_has_leading_or_trailing_slashes
        {
            Because of = ()
                => result = "/blogs/archived/posts/";

            It Should_remove_them = ()
                => result = "blogs/archived/posts";
        }

        public class When_the_string_contains_double_slashes
        {
            Because of = ()
                => result = "// This // actually // happened //".ToSlugWithSegments();

            It Should_replace_them_with_single_slashes = ()
                => result.ShouldEqual("this/actually/happened");
        }
    }
}
