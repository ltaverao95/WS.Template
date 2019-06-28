using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WS.Template.InstanceResolver
{
    public class IocHelper : IDisposable, IIocHelper
    {
        private WindsorContainer instance;

        static IocHelper()
        {
            Instance = new IocHelper();
        }

        private IocHelper()
        {
            this.instance = new WindsorContainer();
        }

        public static IocHelper Instance { get; private set; }

        public void Dispose()
        {
            GC.SuppressFinalize((object)this);
            this.Dispose(true);
        }

        public void Install(params IWindsorInstaller[] installers)
        {
            this.instance.Install(installers);
        }

        public void InstallFromAssembly(Assembly assembly)
        {
            foreach (Type type in ((IEnumerable<Type>)assembly.GetTypes()).Where<Type>((Func<Type, bool>)(type => typeof(IWindsorInstaller).IsAssignableFrom(type))))
                this.Install(type.GetConstructor(new Type[0]).Invoke(new object[0]) as IWindsorInstaller);
        }

        public void InstallFromAssembly(Type typeOfAssembly)
        {
            foreach (Type type in ((IEnumerable<Type>)typeOfAssembly.Assembly.GetTypes()).Where<Type>((Func<Type, bool>)(type => typeof(IWindsorInstaller).IsAssignableFrom(type))))
                this.Install(type.GetConstructor(new Type[0]).Invoke(new object[0]) as IWindsorInstaller);
        }

        public IWindsorContainer AddFacility(IFacility facility)
        {
            return this.instance.AddFacility(facility);
        }

        public IWindsorContainer AddFacility<T>() where T : IFacility, new()
        {
            return this.instance.AddFacility<T>();
        }

        public IWindsorContainer AddFacility<T>(Action<T> onCreate) where T : IFacility, new()
        {
            return this.instance.AddFacility<T>(onCreate);
        }

        public void RemoveChildContainer(IWindsorContainer childContainer)
        {
            this.instance.RemoveChildContainer(childContainer);
        }

        public object Resolve(Type service, IDictionary arguments)
        {
            return this.instance.Resolve(service, arguments);
        }

        public object Resolve(Type service, object argumentsAsAnonymousType)
        {
            return this.instance.Resolve(service, argumentsAsAnonymousType);
        }

        public object Resolve(Type service)
        {
            return this.instance.Resolve(service);
        }

        public object Resolve(string key, Type service)
        {
            return this.instance.Resolve(key, service);
        }

        public object Resolve(string key, Type service, IDictionary arguments)
        {
            return this.instance.Resolve(key, service, arguments);
        }

        public object Resolve(string key, Type service, object argumentsAsAnonymousType)
        {
            return this.instance.Resolve(key, service, argumentsAsAnonymousType);
        }

        public T Resolve<T>(IDictionary arguments)
        {
            return this.instance.Resolve<T>(arguments);
        }

        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return this.instance.Resolve<T>(argumentsAsAnonymousType);
        }

        public T Resolve<T>(string key, IDictionary arguments)
        {
            return this.instance.Resolve<T>(key, arguments);
        }

        public T Resolve<T>(string key, object argumentsAsAnonymousType)
        {
            return this.instance.Resolve<T>(key, argumentsAsAnonymousType);
        }

        public T Resolve<T>()
        {
            return this.instance.Resolve<T>();
        }

        public T Resolve<T>(string key)
        {
            return this.instance.Resolve<T>(key);
        }

        public T[] ResolveAll<T>()
        {
            return this.instance.ResolveAll<T>();
        }

        public Array ResolveAll(Type service)
        {
            return this.instance.ResolveAll(service);
        }

        public Array ResolveAll(Type service, IDictionary arguments)
        {
            return this.instance.ResolveAll(service, arguments);
        }

        public Array ResolveAll(Type service, object argumentsAsAnonymousType)
        {
            return this.instance.ResolveAll(service, argumentsAsAnonymousType);
        }

        public T[] ResolveAll<T>(IDictionary arguments)
        {
            return this.instance.ResolveAll<T>(arguments);
        }

        public T[] ResolveAll<T>(object argumentsAsAnonymousType)
        {
            return this.instance.ResolveAll<T>(argumentsAsAnonymousType);
        }

        public void Release(object obj)
        {
            this.instance.Release(obj);
        }

        public IWindsorContainer GetContainer()
        {
            return (IWindsorContainer)this.instance;
        }

        public void ResetContainer()
        {
            this.instance.Dispose();
            this.instance = new WindsorContainer();
            GC.Collect();
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            this.instance.Dispose();
        }

        public void SetContainer(IWindsorContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            lock (this)
            {
                if (this.instance != null)
                {
                    this.instance.Dispose();
                    GC.Collect();
                }
                this.instance = (WindsorContainer)container;
            }
        }
    }
}