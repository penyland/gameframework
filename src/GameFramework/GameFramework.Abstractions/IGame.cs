// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using System.Threading.Tasks;

namespace GameFramework.Abstractions
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
