﻿// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Input;
using Microsoft.Extensions.Configuration;
using System;

namespace GameFramework
{
    public class Game : GameBase
    {
        // GamePlatform
        // GameWindow
        // SoundDevice
        // ResourceManagement
        // InputDevices
        public Game(IConfiguration configuration, IGraphicsDevice graphicsDevice, IResourceManager resourceManager, IInputManager inputManager)
        {
            this.Configuration = configuration;
            this.GraphicsDevice = graphicsDevice;
            this.ResourceManager = resourceManager;

            this.GameComponents.Add((InputManager)inputManager);
        }

        public IServiceProvider Container { get; private set; }

        public IGraphicsDevice GraphicsDevice { get; }

        public IResourceManager ResourceManager { get; }

        protected IConfiguration Configuration { get; }
    }
}
