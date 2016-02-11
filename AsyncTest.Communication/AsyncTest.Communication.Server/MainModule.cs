﻿using Caliburn.Micro;
using Ninject.Modules;

namespace AsyncTest.Communication.Server
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWindowManager>().To<WindowManager>();

            Bind<IMainViewModel>().To<MainViewModel>();
        }
    }
}