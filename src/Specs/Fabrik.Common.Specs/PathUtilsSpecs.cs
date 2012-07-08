using System;
using Machine.Specifications;

namespace Fabrik.Common.Specs
{
    [Subject(typeof(PathUtils), "CombinePaths")]
    public class PathUtils_CombinePaths
    {
        static Exception exception;
        static string result;
        
        public class When_path1_is_null
        {
            Because of = ()
                => exception = Catch.Exception(() => PathUtils.CombinePaths(null, "dir"));

            It Should_throw_ArgumentNullException = ()
                => exception.ShouldBeOfType<ArgumentNullException>();
        }

        public class When_path2_is_null
        {
            Because of = ()
                => exception = Catch.Exception(() => PathUtils.CombinePaths("/root", null));

            It Should_throw_ArgumentNullException = ()
                => exception.ShouldBeOfType<ArgumentNullException>();
        }

        public class When_path2_is_empty
        {
            Because of = ()
                => result = PathUtils.CombinePaths("/root", "");
            
            It Should_return_path1 = ()
                => result.ShouldEqual("/root");
        }
        
        public class When_path1_is_empty
        {
            Because of = ()
                => result = PathUtils.CombinePaths("", "/dir");
            
            It Should_return_path2 = ()
                => result.ShouldEqual("/dir");
        }

        public class When_path2_is_fully_qualified_http
        {
            Because of = ()
                => result = PathUtils.CombinePaths("/root", "http://somedomain.com");

            It Should_return_path2 = ()
                => result.ShouldEqual("http://somedomain.com");
        }

        public class When_path2_is_fully_qualified_https
        {
            Because of = ()
                => result = PathUtils.CombinePaths("/root", "https://somedomain.com");

            It Should_return_path2 = ()
                => result.ShouldEqual("https://somedomain.com");
        }

        public class When_path2_is_absolute
        {
            Because of = ()
                => result = PathUtils.CombinePaths("/root", "/dir");

            It Should_make_it_relative_to_path1 = ()
                => result.ShouldEqual("/root/dir");
        }

        public class When_path1_and_path2_are_relative
        {
            Because of = ()
                => result = PathUtils.CombinePaths("root", "/dir/subdir/file.htm");

            It Should_combine_the_paths = ()
                => result.ShouldEqual("root/dir/subdir/file.htm");
        }
    }
}
