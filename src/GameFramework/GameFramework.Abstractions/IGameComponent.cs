// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace GameFramework.Abstractions
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
