using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet
{
    class Level 
    {
        //***********Attributs**********\\
        protected SimpleAnimationSprite _pointer;
        protected Character _perso;
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;
        protected Game _game;
        //protected Map _map;


        //***********Constructeurs******\\

        public Level(Game game, GraphicsDeviceManager graphics) 
        {
            _game = game;
            _graphics = graphics;
        }

        //***********Méthodes***********\\
        public  void Initialize()
        {
            _perso = new Character(_game, new SimpleAnimationDefinition()
            {
                AssetName = "homme",
                FrameRate = 50,
                FrameSize = new Point(50, 50),
                Loop = true,
                NbFrames = new Point(8, 4)
            }, _graphics);
            _perso.Initialize();

            _pointer = new SimpleAnimationSprite(_game, new SimpleAnimationDefinition()
            {
                AssetName = "cursor",
                FrameRate = 1,
                FrameSize = new Point(44, 44),
                Loop = true,
                NbFrames = new Point(1, 1)
            });
            _pointer.Initialize();
        }

        public void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            _perso.LoadContent(_spriteBatch);
            _pointer.LoadContent(_spriteBatch);
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            _pointer.PositionCenter = new Vector2(mouse.X, mouse.Y);

            _perso.Update(gameTime);
            _pointer.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _perso.Draw(gameTime, true);
            _pointer.Draw(gameTime, true);
        }
    }
}

