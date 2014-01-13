using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Projet
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        int GameState = 1;
        Menu _menu;
        Level _niveau;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = 900; 
            _graphics.PreferredBackBufferWidth = 1600;
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            _menu = new Menu(this, _graphics);
            _niveau = new Level(this, _graphics);
            _niveau.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("SpriteFont1");
            _menu.LoadContent(this.Content);

            _niveau.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (/* Keyboard.GetState().IsKeyDown(Keys.Escape) || */GameState == -1)
                this.Exit();

            if (GameState == 0)
            {
                _niveau.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.NumPad1) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    GameState = 1;

                base.Update(gameTime);
            }

            if (GameState == 1)
            {
                _menu.Update(gameTime, ref GameState);
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                    GameState = 0;
            }

        }

        protected override void Draw(GameTime gameTime)
        {

            if (GameState == 0)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                _niveau.Draw(gameTime);

                base.Draw(gameTime);
            }

            if (GameState == 1)
            {
                GraphicsDevice.Clear(Color.MediumPurple);
                _menu.Draw(_spriteBatch);
            }

        }
    }
}