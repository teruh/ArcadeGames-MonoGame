using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class BreakoutGame : Game
    {
        public readonly int WINDOW_WIDTH = 1280;
        public readonly int WINDOW_HEIGHT = 720;

        /// <summary>
        /// Player paddle width in pixels
        /// </summary>
        private readonly int PADDLE_WIDTH = 128;

        /// <summary>
        /// Player paddle height in pixels
        /// </summary>
        private readonly int PADDLE_HEIGHT = 16;

        /// <summary>
        /// Breaker ball width in pixels
        /// </summary>
        private readonly int BALL_WIDTH = 16;

        /// <summary>
        /// Breaker ball height in pixels
        /// </summary>
        private readonly int BALL_HEIGHT = 16;

        /// <summary>
        /// Brick width in pixels
        /// </summary>
        private readonly int BRICK_WIDTH = 128;

        /// <summary>
        /// Brick ball height in pixels
        /// </summary>
        private readonly int BRICK_HEIGHT = 32;

        /// <summary>
        /// Ball's move speed
        /// </summary>
        private readonly int BALL_SPEED = 200;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Paddle _paddle;

        private Ball _ball;

        private List<Brick> _bricks;
        private List<Color> _brickColors;

        public BreakoutGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            // The colors of each brick row
            // 1 color = 1 row
            _brickColors = new List<Color>() { 
                Color.Crimson, 
                Color.Tomato, 
                Color.Orange, 
                Color.Yellow, 
                Color.SeaGreen, 
                Color.Turquoise, 
                Color.DodgerBlue, 
                Color.SlateBlue };

            _bricks = new List<Brick>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _paddle = new Paddle(this, PADDLE_WIDTH, PADDLE_HEIGHT, Vector2.Zero, Color.White);

            _ball = new Ball(this, 
                BALL_WIDTH, 
                BALL_HEIGHT, 
                new Vector2(_graphics.GraphicsDevice.Viewport.Bounds.Width / 2, 
                _graphics.GraphicsDevice.Viewport.Bounds.Height / 2), 
                Color.White, 
                BALL_SPEED);

            // Build bricks
            // Columns
            for (int i = 0; i < WINDOW_WIDTH / BRICK_WIDTH; i++)
            {
                // Rows
                for (int j = 0; j < _brickColors.Count; j++)
                {
                    _bricks.Add(new Brick(this, BRICK_WIDTH, BRICK_HEIGHT, new Vector2(i * BRICK_WIDTH + i, j * BRICK_HEIGHT + j), _brickColors[j]));
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _paddle.Update(gameTime);
            _ball.Update(gameTime);

            // Check for ball & paddle collison with AABB
            // TODO: Move this, of course...
            //if (_playerPosition.X < _ball.Position.X + BALL_WIDTH &&
            //    _playerPosition.X + PADDLE_WIDTH > BALL_WIDTH &&
            //    _playerPosition.Y < _ball.Position.Y + BALL_HEIGHT &&
            //    PADDLE_HEIGHT + _playerPosition.Y > _ball.Position.Y)
            //{
            //    Debug.WriteLine("Collison between BALL and PADDLE!");
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _paddle.Draw(_spriteBatch);
            _ball.Draw(_spriteBatch);

            foreach (Brick brick in _bricks)
            {
                brick.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}