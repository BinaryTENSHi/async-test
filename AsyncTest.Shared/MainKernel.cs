using System;
using Ninject;
using Ninject.Modules;

namespace AsyncTest.Shared
{
    public class MainKernel : IDisposable
    {
        private static MainKernel _instance;

        private MainKernel()
        {
            Kernel = new StandardKernel();
        }

        public static MainKernel Instance => _instance ?? (_instance = new MainKernel());

        public IKernel Kernel { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static T Get<T>()
        {
            return Instance.Kernel.Get<T>();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Kernel != null)
                {
                    Kernel.Dispose();
                    Kernel = null;
                }
            }
        }

        public static void Load<T>()
            where T : INinjectModule, new()
        {
            Instance.Kernel.Load<T>();
        }
    }
}