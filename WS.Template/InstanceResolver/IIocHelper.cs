using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections;
using System.Reflection;

namespace WS.Template.InstanceResolver
{
    public interface IIocHelper
    {
        void Dispose();

        void Install(params IWindsorInstaller[] installers);

        void InstallFromAssembly(Assembly assembly);

        void InstallFromAssembly(Type typeOfAssembly);

        IWindsorContainer AddFacility(IFacility facility);

        IWindsorContainer AddFacility<T>() where T : IFacility, new();

        IWindsorContainer AddFacility<T>(Action<T> onCreate) where T : IFacility, new();

        void RemoveChildContainer(IWindsorContainer childContainer);

        object Resolve(Type service, IDictionary arguments);

        object Resolve(Type service, object argumentsAsAnonymousType);

        object Resolve(Type service);

        object Resolve(string key, Type service);

        object Resolve(string key, Type service, IDictionary arguments);

        object Resolve(string key, Type service, object argumentsAsAnonymousType);

        T Resolve<T>(IDictionary arguments);

        T Resolve<T>(object argumentsAsAnonymousType);

        T Resolve<T>(string key, IDictionary arguments);

        T Resolve<T>(string key, object argumentsAsAnonymousType);

        T Resolve<T>();

        T Resolve<T>(string key);

        T[] ResolveAll<T>();

        Array ResolveAll(Type service);

        Array ResolveAll(Type service, IDictionary arguments);

        Array ResolveAll(Type service, object argumentsAsAnonymousType);

        T[] ResolveAll<T>(IDictionary arguments);

        T[] ResolveAll<T>(object argumentsAsAnonymousType);

        void Release(object obj);

        IWindsorContainer GetContainer();

        void ResetContainer();
    }
}