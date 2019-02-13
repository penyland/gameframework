//// Copyright (c) Peter Nylander.  All rights reserved.

//using GameFramework.Contracts;
//using Microsoft.Extensions.DependencyInjection;
//using System;

//namespace Win2D.UWPCore
//{
//    public class Win2DGameFactory : IGameFactory
//    {
//        public IGame Create(IServiceProvider serviceProvider)
//        {
//            // Configure services here
//            return serviceProvider.GetService<Win2DGame>();
//        }

//        public void AddGame(IServiceCollection services)
//        {
//            services.AddSingleton<IGame, Win2DGame>();
//        }
//    }
//}
