using System;
using Machine.Specifications;

namespace Fabrik.Common.Specs
{
    [Subject(typeof(Ensure), "That")]
    public class Ensure_That
    {
        static Exception exception;

        public class When_condition_is_false
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.That(false, "Condition cannot be false."));

            It Should_throw_exception_with_the_specified_message = () =>
            {
                exception.ShouldNotBeNull();
                exception.Message.ShouldEqual("Condition cannot be false.");
            };
        }

        public class When_condition_is_true
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.That(true));

            It Should_not_throw_exception = ()
                => exception.ShouldBeNull();
        }
    }

    [Subject(typeof(Ensure), "That<TException>")]
    public class Ensure_That_TException
    {
        static Exception exception;

        public class When_condition_is_false
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.That<ArgumentException>(false));

            It Should_throw_the_specified_exception_type = ()
                => exception.ShouldBeOfType<ArgumentException>();
        }

        public class When_condition_is_true
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.That(true));

            It Should_not_throw_exception = ()
                => exception.ShouldBeNull();
        }
    }

    [Subject(typeof(Ensure), "NotNull")]
    public class Ensure_NotNull
    {
        static Exception exception;

        public class When_the_object_is_null
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.NotNull(null));

            It Should_throw_a_NullReferenceException = ()
                => exception.ShouldBeOfType<NullReferenceException>();
        }

        public class When_the_object_is_not_null
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.NotNull("test"));

            It Should_not_throw_exception = ()
                => exception.ShouldBeNull();
        }
    }

    [Subject(typeof(Ensure), "Equal<T>")]
    public class Ensure_Equal
    {
        static Exception exception;

        public class When_the_objects_are_not_equal
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.Equal(10, 20));

            It Should_throw_an_exception = () =>
            {
                exception.ShouldNotBeNull();
                exception.Message.ShouldEqual("Values must be equal");
            };
        }

        public class When_the_objects_are_equal
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.Equal(10, 10));

            It Should_not_throw_exception = ()
                => exception.ShouldBeNull();
        }
    }

    [Subject(typeof(Ensure), "Contains<T>")]
    public class Ensure_Contains
    {
        static Exception exception;

        public class When_the_collection_does_not_contain_any_matching_values
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.Contains(new[] { 100, 200 }, i => i < 10));

            It Should_throw_an_exception = ()
                => exception.ShouldNotBeNull();
        }

        public class When_the_collection_does_contain_a_matching_value
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.Contains(new[] { 1, 2, 3 }, i => i < 10));

            It Should_not_throw_exception = ()
                => exception.ShouldBeNull();
        }
    }

    [Subject(typeof(Ensure), "Items<T>")]
    public class Ensure_Items
    {
        static Exception exception;

        public class When_not_all_items_match_the_predicate
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.Items(new[] { 1, 2, 100 }, i => i < 10));

            It Should_throw_an_exception = ()
                => exception.ShouldNotBeNull();
        }

        public class When_all_items_match_the_predicate
        {
            Because of = ()
                => exception = Catch.Exception(() => Ensure.Contains(new[] { 1, 2, 3 }, i => i < 10));

            It Should_not_throw_exception = ()
                => exception.ShouldBeNull();
        }
    }
}
