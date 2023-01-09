using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Breakout
{
    internal class Brick : GameObject
    {
        public Brick(Game game, int width, int height, Vector2 position, Color color) : base(game, width, height, position, color)
        {
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
