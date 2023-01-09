using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    internal class Paddle : GameObject
    {
        public Paddle(Game game, int width, int height, Vector2 position, Color objectColor) : base(game, width, height, position, objectColor)
        {
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Move this logic
            // TODO: Window size (in this case, 1280 x 720, from PlayArea in scene, when implemented)
            Position = new Vector2(
                // Player position X is the mouse position
                MathHelper.Clamp(Mouse.GetState().X, 0, 1280 - Width), 
                // Player's position Y should never change
                720 - Height);
        }
    }
}
