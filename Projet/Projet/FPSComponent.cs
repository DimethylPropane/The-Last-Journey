using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projet
{
    class FPSComponent : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public FPSComponent(Game game) : base(game) { }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteFont = Game.Content.Load<SpriteFont>("SpriteFont1");
        }

        protected override void LoadContent() { }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            frameCounter++;
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, frameRate.ToString() + " FPS", new Vector2(0, 700), Color.White);
            spriteBatch.End();
        }
    }
}
