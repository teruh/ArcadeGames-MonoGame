using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class BreakoutGame : Game
    {
        private readonly int WINDOW_WIDTH = 1280;
        private readonly int WINDOW_HEIGHT = 720;

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

        private Vector2 _playerPosition;

        private Vector2 _ballPosition;
        private Vector2 _ballVelocity;

        private Texture2D _playerTexture;
        private Texture2D _ballTexture;

        private Random _random;

        private Dictionary<Color, Texture2D> _brickTextures;
        private List<Color> _brickColors;

        public BreakoutGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _random = new Random();
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            // Start ball at the center of the screen
            _ballPosition = new Vector2(_graphics.GraphicsDevice.Viewport.Bounds.Width / 2, _graphics.GraphicsDevice.Viewport.Bounds.Height / 2);

            // Start ball at random velocity moving towards the bottom of the screen
            _ballVelocity = new Vector2(_random.Next(2) * 2 - 1, 1);
            _ballVelocity.Normalize();

            _brickTextures = new Dictionary<Color, Texture2D>();

            // The colors of each brick row
            _brickColors = new List<Color>() { Color.Crimson, Color.Tomato, Color.Orange, Color.Yellow, Color.SeaGreen, Color.Turquoise, Color.DodgerBlue, Color.SlateBlue };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _playerTexture = new Texture2D(this._graphics.GraphicsDevice, 1, 1);
            _playerTexture.SetData(new Color[] { Color.White });

            _ballTexture = new Texture2D(this._graphics.GraphicsDevice, 1, 1);
            _ballTexture.SetData(new Color[] { Color.White });

            foreach (Color color in _brickColors)
            {
                _brickTextures.Add(color, new Texture2D(this._graphics.GraphicsDevice, 1, 1));
                _brickTextures[color].SetData(new Color[] { color });
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _ballPosition += _ballVelocity * BALL_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _playerPosition = new Vector2(MathHelper.Clamp(Mouse.GetState().X, 0, WINDOW_WIDTH - PADDLE_WIDTH), WINDOW_HEIGHT - PADDLE_HEIGHT);

            // Check for ball & paddle collison with AABB
            // TODO: Move this, of course...
            if (_playerPosition.X < _ballPosition.X + BALL_WIDTH &&
                _playerPosition.X + PADDLE_WIDTH > BALL_WIDTH &&
                _playerPosition.Y < _ballPosition.Y + BALL_HEIGHT &&
                PADDLE_HEIGHT + _playerPosition.Y > _ballPosition.Y)
            {
                _ballVelocity *= -1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            // Draw ball
            _spriteBatch.Draw(_ballTexture, new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, BALL_WIDTH, BALL_HEIGHT), null, 
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);

            // Draw player
            _spriteBatch.Draw(_playerTexture, new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, PADDLE_WIDTH, PADDLE_HEIGHT), null, 
                Color.White, 0.0f, new Vector2(_ballTexture.Width / 2, _ballTexture.Height / 2), SpriteEffects.None, 0);

            // Draw bricks
            for (int i = 0; i < WINDOW_WIDTH / BRICK_WIDTH; i++)
            {
                for (int j = 0; j < _brickColors.Count; j++)
                {
                    _spriteBatch.Draw(_brickTextures[_brickColors[j]], new Rectangle(i * BRICK_WIDTH, j * BRICK_HEIGHT, BRICK_WIDTH, BRICK_HEIGHT), null,
                        Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}