using System;
using Microsoft.Xna.Framework;

namespace Breakout
{
    internal class Ball : GameObject
    {
        public float MoveSpeed { get; private set; }
        private Vector2 Velocity { get; set; }

        private Random _random;

        public Ball(Game game, int width, int height, Vector2 position, Color objectColor, float moveSpeed) : base(game, width, height, position, objectColor)
        {
            this.MoveSpeed = moveSpeed;

            _random = new Random();
            
            // Start ball at random velocity moving towards the bottom of the screen
            Velocity = new Vector2(_random.Next(2) * 2 - 1, 1);
            Velocity.Normalize();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                Position += Velocity * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
