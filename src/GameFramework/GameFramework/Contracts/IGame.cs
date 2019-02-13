// Copyright (c) Peter Nylander.  All rights reserved.

using System.Threading.Tasks;

namespace GameFramework.Contracts
{
    public interface IGame
    {
        Task CreateResourcesAsync();

        void Draw(GameTime gameTime, IDrawingSession drawingSession);

        void Initialize();

        void Run();

        void Update(GameTime gameTime);
    }
}
