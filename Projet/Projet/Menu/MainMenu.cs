using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.GamerServices;

namespace Projet
{
    class Menu
    {
        //***********Attributs**********\\
        protected Texture2D _button;
        protected int _selectorChoice;
        protected string[] _buttonsId = { "play", "options", "exit" };
        protected SpriteFont _menuFont;
        protected Vector2 _position;
        float _delay;
        protected Texture2D buttonTexture;
        Button buttonPlay;
        Button buttonOptions;
        Button buttonExit;

        //***********Constructeurs******\\

        public Menu(Game game, GraphicsDeviceManager graphics)
        {
            game.IsMouseVisible = true;
        }
        //***********Accesseurs*********\\

        //***********Méthodes***********\\

        public virtual void LoadContent(ContentManager Content)
        {
            _button = Content.Load<Texture2D>("button");
            _selectorChoice = 0;
            _position.X = 670;
            _position.Y = 300;
            _menuFont = Content.Load<SpriteFont>("SpriteFont_Menu");
            _delay = 0f;
            buttonTexture = Content.Load<Texture2D>("button");
            buttonPlay = new Button(buttonTexture);
            buttonOptions = new Button(buttonTexture);
            buttonExit = new Button(buttonTexture);

            buttonPlay.SetPosition(new Vector2(620, 280));
            buttonOptions.SetPosition(new Vector2(620, 420));
            buttonExit.SetPosition(new Vector2(620, 560));
        }

        public virtual void Update(GameTime gametime, ref Projet.Game1.GameState currentgamestate)
        {

            MouseState mouse = Mouse.GetState();

                if (_delay <= 0f)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) // || mouse.LeftButton == ButtonState.Pressed)
                    {
                        if(_selectorChoice == 0)
                            currentgamestate = Projet.Game1.GameState.Playing;
                        if (_selectorChoice == 1)
                        {
                            currentgamestate = Projet.Game1.GameState.Options;
                            System.Threading.Thread.Sleep(100);
                        }
                        if(_selectorChoice == 2)
                            currentgamestate = Projet.Game1.GameState.Exit;
                    }

                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (buttonPlay.isMouseOver) currentgamestate = Projet.Game1.GameState.Playing;
                        if (buttonOptions.isMouseOver) currentgamestate = Projet.Game1.GameState.Options; System.Threading.Thread.Sleep(100);
                        if (buttonExit.isMouseOver) currentgamestate = Projet.Game1.GameState.Exit;
                    }


                    if ((Keyboard.GetState().IsKeyDown(Keys.Down)))
                    {
                        _selectorChoice++;
                        _delay = 200f;
                        if (_selectorChoice > 2)
                            _selectorChoice = 2;
                    }

                    if ((Keyboard.GetState().IsKeyDown(Keys.Up)))
                    {
                        _selectorChoice--;
                        _delay = 200f;
                        if (_selectorChoice < 0)
                            _selectorChoice = 0;
                    }

                    if (_selectorChoice == 0) buttonPlay.isSelected = true;
                    if (_selectorChoice == 1) buttonOptions.isSelected = true;
                    if (_selectorChoice == 2) buttonExit.isSelected = true;

                    buttonPlay.Update(mouse);
                    buttonOptions.Update(mouse);
                    buttonExit.Update(mouse);

                    if (buttonPlay.isMouseOver) _selectorChoice = 0;
                    if (buttonOptions.isMouseOver) _selectorChoice = 1;
                    if (buttonExit.isMouseOver) _selectorChoice = 2;
                }

            if (_delay > 0f)
                _delay -= gametime.ElapsedGameTime.Milliseconds;

        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
                spritebatch.Begin();

                _position.Y = 300;

                buttonPlay.Draw(spritebatch);
                buttonOptions.Draw(spritebatch);
                buttonExit.Draw(spritebatch);

                for (int i = 0; i < _buttonsId.Length; i++)
                {
                        if (_selectorChoice == i)
                            spritebatch.DrawString(_menuFont, Language.GetWord(_buttonsId[i]), _position, Color.Orange);
                        else
                            spritebatch.DrawString(_menuFont, Language.GetWord(_buttonsId[i]), _position, Color.White);

                    _position.Y += 140;
                }
                spritebatch.End();
        }

    }
}
