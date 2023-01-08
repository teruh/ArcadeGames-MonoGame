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

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _playerPosition;
        private Texture2D _playerTexture;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _playerTexture = new Texture2D(this._graphics.GraphicsDevice, 1, 1);
            _playerTexture.SetData(new Color[] { Color.IndianRed });
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _playerPosition = new Vector2(MathHelper.Clamp(Mouse.GetState().X, 0, WINDOW_WIDTH - PADDLE_WIDTH), WINDOW_HEIGHT - PADDLE_HEIGHT);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerTexture, new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, PADDLE_WIDTH, PADDLE_HEIGHT), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}