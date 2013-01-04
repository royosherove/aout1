using System;
using System.Collections.Generic;
using Castle.Core;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.Windsor;
using Rhino.Mocks;

namespace AOUT.CH8.Logan.Tests
{
    public class MockingContainer:WindsorContainer,IFacility,ISubDependencyResolver
    {
        Dictionary<Type,object> services = new Dictionary<Type, object>();
        public override object Resolve(Type service)
        {
            return services[service];
        }

        private List<Type> dontMock = new List<Type>();
        private MockRepository mocks;

        public MockingContainer(MockRepository mocks)
        {
            Kernel.AddFacility("MockingFacility",this);
            this.mocks = mocks;
        }

        public T ResolveRealObject<T>()
        {
            dontMock.Add(typeof(T));
            AddComponent<T>();
            return Resolve<T>();
        }
        #region IFacility Members

        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.Resolver.AddSubResolver(this);           
        }

        public void Terminate()
        {
        }

        #endregion

        #region ISubDependencyResolver Members

        public object Resolve(CreationContext context, ISubDependencyResolver parentResolver, ComponentModel model,
                                  DependencyModel dependency)
        {
//            AddComponent(dependency.TargetType.FullName,dependency.TargetType);
            
            if (dontMock.Contains(dependency.TargetType))
            {
                return Kernel[dependency.TargetType];
            }
            return mocks.CreateMock(dependency.TargetType);
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver parentResolver, ComponentModel model,
                               DependencyModel dependency)
        {
            return dependency.DependencyType == DependencyType.Service;
        }
        #endregion
    }
}
