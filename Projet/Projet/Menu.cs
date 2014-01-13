using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Projet
{
    class Menu
    {
        //***********Attributs**********\\
        protected Texture2D _buttonStart;
        protected Texture2D _buttonExit;
        protected Texture2D _selector;
        protected int _selectorChoice;
        protected Vector2 _selectorPosition;


        //***********Constructeurs******\\

        public Menu(Game game, GraphicsDeviceManager graphics) {}
        //***********Accesseurs*********\\

        //***********Méthodes***********\\

        public virtual void LoadContent(ContentManager Content)
        {
            _buttonStart = Content.Load<Texture2D>("startbutton");
            _buttonExit = Content.Load<Texture2D>("exitbutton");
            _selector = Content.Load<Texture2D>("selector");
            _selectorChoice = 0;
            _selectorPosition = new Vector2(600, 292);
        }

        public virtual void Update(GameTime gametime, ref int gamestate)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                switch(_selectorChoice)
                {
                    case 0:
                        gamestate = 0;
                        break;
                    default:
                        gamestate = -1;
                        break;
                }
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Down)) && _selectorChoice == 0)
            {
                _selectorChoice = 1;
                _selectorPosition.Y = 432;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Up)) && _selectorChoice == 1)
            {
                _selectorChoice = 0;
                _selectorPosition.Y = 292;
            }

        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(_buttonStart, new Vector2(600, 300), Color.White);
            spritebatch.Draw(_buttonExit, new Vector2(600, 440), Color.White);
            spritebatch.Draw(_selector, _selectorPosition, Color.White);
            spritebatch.End();
        }

    }
}
