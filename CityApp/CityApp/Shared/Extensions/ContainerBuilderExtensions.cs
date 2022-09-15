using System.Linq;
using System.Reflection;
using Autofac;
using CityApp.Core.ViewModels;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.PageLocators.Abstractions;
using CityApp.Infrastructure.PageLocators.Implementations;
using Xamarin.Forms;

namespace CityApp.Shared.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterMvvmComponents(this ContainerBuilder builder,
            params Assembly[] assemblies)
        {
            
            builder
                .RegisterType<PageLocator>()
                .As<IPageLocator>()
                .SingleInstance();
            builder
                .RegisterViewModels(assemblies)
                .RegisterViews(assemblies);

            return builder;
        }

        public static ContainerBuilder RegisterViewModels(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x =>
                    x.GetTypeInfo().IsClass &&
                    !x.GetTypeInfo().IsAbstract &&
                    x.GetTypeInfo().ImplementedInterfaces.Any(y => y == typeof(IViewModel)))
                .InstancePerDependency();

            return builder;
        }


        public static ContainerBuilder RegisterViews(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x =>
                    x.GetTypeInfo().IsClass &&
                    !x.GetTypeInfo().IsAbstract &&
                    x.GetTypeInfo().IsSubclassOf(typeof(Page)))
                .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder RegisterServices(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            return builder;
        }

        public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            return builder;
        }

        public static ContainerBuilder RegisterXamDependency<T>(this ContainerBuilder builder) where T : class
        {
            builder.Register(x => DependencyService.Get<T>()).SingleInstance();
            return builder;
        }
    }
}
