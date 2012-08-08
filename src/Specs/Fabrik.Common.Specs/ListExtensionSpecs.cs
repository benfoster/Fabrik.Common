using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace Fabrik.Common.Specs
{
    [Subject(typeof(ListExtensions), "Move")]
    public class List_Move
    {
        static List<int> list;

        public class When_no_matching_item_is_found
        {
            static Exception exception;

            Establish ctx = () => {
                list = new List<int>();
            };

            Because of = ()
                => exception = Catch.Exception(() => list.Move(x => x == 10, 10));

            It Should_throw_an_exception = ()
                => exception.ShouldBeOfType<ArgumentException>();
        }

        public class When_new_index_is_higher
        {
            Establish ctx = () => {
                list = new List<int> { 1, 2, 3, 4, 5 };
            };

            Because of = ()
                => list.Move(x => x == 3, 3); // shift 3 up one

            It Should_be_moved_to_the_specified_index = () =>
                {
                    list[0].ShouldEqual(1);
                    list[1].ShouldEqual(2);
                    list[2].ShouldEqual(4);
                    list[3].ShouldEqual(3);
                    list[4].ShouldEqual(5);
                };
        }

        public class When_new_index_is_lower
        {
            Establish ctx = () => {
                list = new List<int> { 1, 2, 3, 4, 5 };
            };

            Because of = ()
                => list.Move(x => x == 4, 2); // shift 4 down one

            It Should_be_moved_to_the_specified_index = () =>
            {
                list[0].ShouldEqual(1);
                list[1].ShouldEqual(2);
                list[2].ShouldEqual(4);
                list[3].ShouldEqual(3);
                list[4].ShouldEqual(5);
            };
        }

        // edge cases

        public class When_moving_the_first_item
        {
            Establish ctx = () => {
                list = new List<int> { 1, 2, 3, 4, 5 };
            };

            Because of = ()
                => list.Move(x => x == 1, 1); // shift up 1

            It Should_be_moved_to_the_specified_index = () =>
            {
                list[0].ShouldEqual(2);
                list[1].ShouldEqual(1);
                list[2].ShouldEqual(3);
                list[3].ShouldEqual(4);
                list[4].ShouldEqual(5);
            };
        }

        public class When_moving_the_last_item
        {
            Establish ctx = () => {
                list = new List<int> { 1, 2, 3, 4, 5 };
            };

            Because of = ()
                => list.Move(x => x == 5, 3); // shift down 1

            It Should_be_moved_to_the_specified_index = () =>
            {
                list[0].ShouldEqual(1);
                list[1].ShouldEqual(2);
                list[2].ShouldEqual(3);
                list[3].ShouldEqual(5);
                list[4].ShouldEqual(4);
            };
        }
    }

    [Subject(typeof(ListExtensions), "MoveToEnd")]
    public class List_MoveToEnd 
    {
        static List<int> list;
        
        public class When_list_has_more_than_one_item
        {
            Establish ctx = () =>
            {
                list = new List<int> { 1, 2, 3, 4, 5 };
            };

            Because of = ()
                => list.MoveToEnd(x => x == 1); // move to end

            It Should_be_moved_to_the_end_of_the_list = () =>
            {
                list[0].ShouldEqual(2);
                list[1].ShouldEqual(3);
                list[2].ShouldEqual(4);
                list[3].ShouldEqual(5);
                list[4].ShouldEqual(1);
            };
        }
    }

    [Subject(typeof(ListExtensions), "MoveToBeginning")]
    public class List_MoveToBeginning
    {
        static List<int> list;

        public class When_list_has_more_than_one_item
        {
            Establish ctx = () =>
            {
                list = new List<int> { 1, 2, 3, 4, 5 };
            };

            Because of = ()
                => list.MoveToBeginning(x => x == 5); // move to end

            It Should_be_moved_to_the_end_of_the_list = () =>
            {
                list[0].ShouldEqual(5);
                list[1].ShouldEqual(1);
                list[2].ShouldEqual(2);
                list[3].ShouldEqual(3);
                list[4].ShouldEqual(4);
            };
        }
    }
}
