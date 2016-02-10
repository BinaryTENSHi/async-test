using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;

namespace AsyncTest.Shared
{
    public abstract class AsyncTestBootstrapper : BootstrapperBase
    {
        protected AsyncTestBootstrapper() : base(true)
        {
            Initialize();
        }

        protected override object GetInstance(Type service, string key)
        {
            return MainKernel.Instance.Kernel.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return MainKernel.Instance.Kernel.GetAll(service);
        }
    }
}