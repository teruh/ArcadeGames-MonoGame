using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    abstract class GameObject
    {
        public bool IsEnabled { get; set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Color ObjectColor { get; set; }
        public Texture2D Texture { get; private set; }

        protected Game _game;

        public GameObject(Game game, int width, int height, Vector2 position, Color objectColor)
        {
            this._game = game;
            this.Width = width;
            this.Height = height;
            this.Position = position;
            this.ObjectColor = objectColor;

            IsEnabled = true;

            Texture = new Texture2D(game.GraphicsDevice, 1, 1);
            Texture.SetData(new Color[] { Color.White });
        }

        public abstract void Update(GameTime gameTime);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsEnabled)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), null,
                    ObjectColor,
                    0.0f,
                    new Vector2(Texture.Width / 2, Texture.Height / 2),
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
