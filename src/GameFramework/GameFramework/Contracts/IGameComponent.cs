// Copyright (c) Peter Nylander.  All rights reserved.

using System.Threading.Tasks;

namespace GameFramework.Contracts
{
    public interface IGameComponent
    {
        void Dispose();

        void Initialize();

        Task CreateResourcesAsync();

        void Update(GameTime gameTime);

        void BeginDraw();

        void Draw(GameTime gameTime);

        void EndDraw();
    }
}
