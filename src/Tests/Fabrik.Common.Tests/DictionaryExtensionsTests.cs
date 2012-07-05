using System.Collections.Generic;
using Machine.Specifications;

namespace Fabrik.Common.Tests
{
    public abstract class With_test_dictionary
    {
        protected static Dictionary<string, int> dict;

        Establish ctx = ()
            => dict = new Dictionary<string, int>() { { "John", 35 }, { "Pete", 30 }, { "Scott", 40 } };
    }
    
    [Subject("Dictionary.GetOrDefault")]
    public class Dictionary_GetOrDefault
    {
        static int result;
        
        public class When_the_key_exists : With_test_dictionary
        {
            Because of = ()
                => result = dict.GetOrDefault("John");

            It Should_return_the_value_with_the_associated_key = ()
                => result.ShouldEqual(35);
        }

        public class When_the_key_does_not_exist_and_no_default_value_is_provided : With_test_dictionary
        {
            Because of = ()
                => result = dict.GetOrDefault("no exists");

            It Should_return_the_default_value_for_the_value_type = ()
                => result.ShouldEqual(0);
        }

        public class When_the_key_does_not_exist_and_a_default_value_is_provided : With_test_dictionary
        {
            Because of = ()
                => result = dict.GetOrDefault("no exists", 25);

            It Should_return_the_provided_value = ()
                => result.ShouldEqual(25);
        }
    }
}
