// Copyright (c) Peter Nylander.  All rights reserved.

using System.Threading.Tasks;

namespace GameFramework.Contracts
{
    public interface IGame
    {
        Task CreateResourcesAsync();

        void BeginDraw();

        void Draw(GameTime gameTime);

        void Draw(GameTime gameTime, IDrawingSession drawingSession);

        void Initialize();

        void Update(GameTime gameTime);

        void EndDraw();
    }
}
