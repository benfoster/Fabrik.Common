using System.Collections.Generic;
using Machine.Specifications;

namespace Fabrik.Common.Specs
{
    public abstract class DictionaryExtensionContext
    {
        protected static Dictionary<string, int> dict;
    }
    
    [Subject(typeof(DictionaryExtensions), "GetOrDefault")]
    public class Dictionary_GetOrDefault
    {
        static int result;
        
        public class When_the_key_exists : DictionaryExtensionContext
        {
            Establish ctx = ()
                => dict = new Dictionary<string, int>() { { "John", 35 }, { "Pete", 30 }, { "Scott", 40 } };
            
            Because of = ()
                => result = dict.GetOrDefault("John");

            It Should_return_the_value_from_the_dictionary_with_the_associated_key = ()
                => result.ShouldEqual(35);
        }

        public class When_the_key_does_not_exist_and_no_default_value_is_provided : DictionaryExtensionContext
        {
            Establish ctx = () =>
                dict = new Dictionary<string, int>();
            
            Because of = ()
                => result = dict.GetOrDefault("no exists");

            It Should_return_the_default_value_for_the_value_type = ()
                => result.ShouldEqual(0);
        }

        public class When_the_key_does_not_exist_and_a_default_value_is_provided : DictionaryExtensionContext
        {
            Establish ctx = ()
                => dict = new Dictionary<string, int>();
            
            Because of = ()
                => result = dict.GetOrDefault("no exists", 25);

            It Should_return_the_provided_value = ()
                => result.ShouldEqual(25);
        }
    }
}
