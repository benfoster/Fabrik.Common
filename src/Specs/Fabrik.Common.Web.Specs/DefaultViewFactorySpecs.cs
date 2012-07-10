using System.Web.Mvc;
using Machine.Fakes;
using Machine.Specifications;

namespace Fabrik.Common.Web.Specs
{
    public class DefaultViewFactorySpecs
    {       
        [Subject(typeof(DefaultViewFactory), "Creating a view")]
        public class Creating_a_view_with_no_parameters
        {
            static IViewFactory factory;
            static TestView view;
            static IViewBuilder<TestView> builder;

            public class When_no_view_builder_exists : WithFakes
            {
                Establish ctx = () =>
                {
                    var resolver = An<IDependencyResolver>();                   
                    DependencyResolver.SetResolver(resolver); // will just return null
                    factory = new DefaultViewFactory();
                };

                Because of = ()
                    => view = factory.CreateView<TestView>();

                It Should_instantiate_a_instance_of_the_view = ()
                    => view.ShouldNotBeNull();

                It Should_use_the_default_constructor = ()
                    => view.Message.ShouldEqual(new TestView().Message);
            }

            public class When_a_view_builder_exists : WithFakes
            {
                Establish ctx = () =>
                {
                    builder = An<IViewBuilder<TestView>>();
                    var resolver = An<IDependencyResolver>();
                    resolver.WhenToldTo(x => x.GetService(Param.Is(typeof(IViewBuilder<TestView>))))
                        .Return(builder);

                    DependencyResolver.SetResolver(resolver);
                    factory = new DefaultViewFactory();                   
                };

                Because of = ()
                    => view = factory.CreateView<TestView>();

                It Should_use_the_builder_to_create_the_view = ()
                    => builder.WasToldTo(x => x.Build());
            }
        }

        [Subject(typeof(DefaultViewFactory), "Creating a view with parameters")]
        public class Creating_a_view_with_parameters
        {
            static IViewFactory factory;
            static TestView view;
            static IViewBuilder<string, TestView> builder;
            
            public class When_no_view_builder_exists : WithFakes
            {
                Establish ctx = () =>
                {
                    var resolver = An<IDependencyResolver>();
                    DependencyResolver.SetResolver(resolver);
                    factory = new DefaultViewFactory();
                };

                Because of = ()
                    => view = factory.CreateView<string, TestView>("Test");

                It Should_instantiate_a_instance_of_the_view = ()
                    => view.ShouldNotBeNull();

                It Should_use_the_constructor_with_parameters = ()
                    => view.Message.ShouldEqual("Test");
            }

            public class When_a_view_builder_exists : WithFakes
            { 
                Establish ctx = () =>
                {
                    builder = An<IViewBuilder<string, TestView>>();
                    builder.WhenToldTo(x => x.Build(Param.IsAny<string>()))
                        .Return<string>(s => new TestView("Builder " + s));

                    var resolver = An<IDependencyResolver>();
                    resolver.WhenToldTo(x => x.GetService(Param.Is(typeof(IViewBuilder<string, TestView>))))
                        .Return(builder);

                    DependencyResolver.SetResolver(resolver);
                    factory = new DefaultViewFactory();
                };

                Because of = ()
                    => view = factory.CreateView<string, TestView>("Test");

                It Should_use_the_builder_to_create_the_view = ()
                    => view.Message.ShouldEqual("Builder Test");
            }
        }

        public class TestView
        {
            public string Message { get; set; }

            public TestView()
            {
                Message = "Default";
            }

            public TestView(string message)
            {
                Message = message;
            }
        }
    }
}
