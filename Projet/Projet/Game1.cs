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
        Menu mainMenu;
        Options menuOptions;
        Level _niveau;
        Weapon testweapon;
        FPSComponent fpsMonitor;

        public enum GameState { MainMenu, Options, Playing, Exit }
        public GameState CurrentGameState = GameState.MainMenu;

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
            mainMenu = new Menu(this, _graphics);
            menuOptions = new Options(this, _graphics);
            _niveau = new Level(this, _graphics);
            _niveau.Initialize();
            Language.Initialize();
            ContentManagerGet.Initialize(this.Content);
            testweapon = new Weapon("test", 50, 50, 50f, false, new Vector2(700, 700), 1f, null);
            for (int i = 0; i < 5; i++)
            {
                Weapon weapon = new Weapon("test", 50, 50, 50f, false, new Vector2(100 * i, 100 * i), i, null);
            }
            fpsMonitor = new FPSComponent(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("SpriteFont1");
            mainMenu.LoadContent(this.Content);
            menuOptions.LoadContent(this.Content);
            _niveau.LoadContent();
            fpsMonitor.Initialize();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            switch(CurrentGameState)
            {
                case Game1.GameState.MainMenu:
                    mainMenu.Update(gameTime, ref CurrentGameState);
                    break;

                case Game1.GameState.Options:
                    menuOptions.Update(gameTime, ref CurrentGameState);
                    break;

                case Game1.GameState.Playing:
                    WeaponManager.Update(gameTime);
                    _niveau.Update(gameTime);
                    BulletManager.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) CurrentGameState = GameState.MainMenu;
                    base.Update(gameTime);
                    break;

                default:
                    this.Exit();
                    break;
            }

            fpsMonitor.Update(gameTime);

        } 


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (CurrentGameState)
            {
                case Game1.GameState.MainMenu:
                    mainMenu.Draw(_spriteBatch);
                    break;

                case Game1.GameState.Options:
                    menuOptions.Draw(_spriteBatch);
                    break;

                case Game1.GameState.Playing:
                    WeaponManager.Draw(_spriteBatch);
                    BulletManager.Draw(_spriteBatch);
                    _niveau.Draw(gameTime);
                    base.Draw(gameTime);
                    break;
            }

            fpsMonitor.Draw(gameTime);
        }
    }
}