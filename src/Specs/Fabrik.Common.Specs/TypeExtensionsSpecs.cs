using Machine.Specifications;

namespace Fabrik.Common.Specs
{
    [Subject(typeof(TypeExtensions), "Implements")]
    public class Type_Implements
    {
        static bool result;

        public class When_the_current_type_is_a_subclass_of_type
        {
            Because of = () => result = typeof(Foo).Implements<BaseFoo>();

            It Should_return_true = () => result.ShouldBeTrue();
        }

        public class When_the_current_type_implements_interface_type
        {
            Because of = () => result = typeof(Foo2).Implements<IFoo>();

            It Should_return_true = () => result.ShouldBeTrue();
        }

        public class When_the_current_type_does_not_implement_type
        {
            Because of = () => result = typeof(Foo3).Implements<BaseFoo>();

            It Should_return_false = () => result.ShouldBeFalse();
        }

        private class Foo : BaseFoo
        {

        }

        private class Foo2 : IFoo
        {

        }

        private class Foo3
        {

        }

        private class BaseFoo
        {

        }

        private class IFoo
        {

        }
    }   
}
