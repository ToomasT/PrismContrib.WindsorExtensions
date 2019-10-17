using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Prism.Ioc;

namespace PrismContrib.WindsorExtensions
{
    public class WindsorContainerExtension : IContainerExtension<IWindsorContainer>
    {
        public IWindsorContainer Instance { get; }

        public WindsorContainerExtension() : this(new WindsorContainer())
        {
        }

        public WindsorContainerExtension(IWindsorContainer container) => Instance = container;

        public void FinalizeExtension()
        {
        }

        public IContainerRegistry RegisterInstance(Type type, object instance)
        {
            Instance.Register(Component.For(type).Instance(instance));
            return this;
        }

        public IContainerRegistry RegisterInstance(Type type, object instance, string name)
        {
            Instance.Register(Component.For(type).Instance(instance).Named(name));
            return this;
        }

        public IContainerRegistry RegisterSingleton(Type @from, Type to)
        {
            Instance.Register(Component.For(from).ImplementedBy(to).LifeStyle.Singleton);
            return this;
        }

        public IContainerRegistry RegisterSingleton(Type @from, Type to, string name)
        {
            Instance.Register(Component.For(from).ImplementedBy(to).Named(name).LifeStyle.Singleton);
            return this;
        }

        public IContainerRegistry Register(Type @from, Type to)
        {
            Instance.Register(Component.For(from).ImplementedBy(to).LifeStyle.Transient);
            return this;
        }

        public IContainerRegistry Register(Type @from, Type to, string name)
        {
            Instance.Register(Component.For(from).ImplementedBy(to).Named(name).LifeStyle.Transient);
            return this;
        }


        public object Resolve(Type type)
        {
            return Instance.Resolve(type);
        }

        public object Resolve(Type type, params (Type Type, object Instance)[] parameters)
        {
            var arguments = new Arguments();
            parameters.ToList().ForEach(tuple => arguments.AddTyped(tuple.Type, tuple.Instance));
            return Instance.Resolve(type, arguments);
        }

        public object Resolve(Type type, string name)
        {
            return Instance.Resolve(name, type);
        }

        public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters)
        {
            var arguments = new Arguments();
            parameters.ToList().ForEach(tuple => arguments.AddTyped(tuple.Type, tuple.Instance));
            return Instance.Resolve(name, type, arguments);
        }


        public bool IsRegistered(Type type)
        {
            return Instance.Kernel.HasComponent(type);
        }

        public bool IsRegistered(Type type, string name)
        {
            return Instance.Kernel.HasComponent(name);
            // try
            // {
            //     return Resolve(type, name) != null;
            // }
            // catch (Exception e)
            // {
            //     return false;
            // }
        }
    }
}