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
    class Options
    {
         //***********Attributs**********\\
        protected Texture2D _button;
        protected int _selectorChoice;
        protected string[] _buttonsId = { "lang_fr", "lang_en", "back" };
        protected SpriteFont _menuFont;
        protected Vector2 _position;
        float _delay;
        protected Texture2D buttonTexture;
        Button buttonLangFr;
        Button buttonLangEn;
        Button buttonBack;

        //***********Constructeurs******\\

        public Options(Game game, GraphicsDeviceManager graphics)
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
            buttonLangFr = new Button(buttonTexture);
            buttonLangEn = new Button(buttonTexture);
            buttonBack = new Button(buttonTexture);

            buttonLangFr.SetPosition(new Vector2(620, 280));
            buttonLangEn.SetPosition(new Vector2(620, 420));
            buttonBack.SetPosition(new Vector2(620, 560));
        }

        public virtual void Update(GameTime gametime, ref Projet.Game1.GameState currentgamestate)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// MENU PRINCIPAL
            MouseState mouse = Mouse.GetState();

            if (_delay <= 0f)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (_selectorChoice != 2) LangChange(_selectorChoice);
                    if (_selectorChoice == 2) currentgamestate = Projet.Game1.GameState.MainMenu; System.Threading.Thread.Sleep(100);
                }

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (buttonLangFr.isMouseOver || buttonLangEn.isMouseOver) LangChange(_selectorChoice);
                    if (buttonBack.isMouseOver) currentgamestate = Projet.Game1.GameState.MainMenu; System.Threading.Thread.Sleep(100);
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

                if (_selectorChoice == 0) buttonLangFr.isSelected = true;
                if (_selectorChoice == 1) buttonLangEn.isSelected = true;
                if (_selectorChoice == 2) buttonBack.isSelected = true;

                buttonLangFr.Update(mouse);
                buttonLangEn.Update(mouse);
                buttonBack.Update(mouse);

                if (buttonLangFr.isMouseOver) _selectorChoice = 0;
                if (buttonLangEn.isMouseOver) _selectorChoice = 1;
                if (buttonBack.isMouseOver) _selectorChoice = 2;
            }

            if (_delay > 0f)
                _delay -= gametime.ElapsedGameTime.Milliseconds;

        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
                spritebatch.Begin();

                _position.Y = 300;

                buttonLangFr.Draw(spritebatch);
                buttonLangEn.Draw(spritebatch);
                buttonBack.Draw(spritebatch);

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

        public virtual void LangChange(int _selectorChoice)
        {
            string[] copy;
            copy = System.IO.File.ReadAllLines("config.ini");

            if (_selectorChoice == 0) System.IO.File.WriteAllText("config.ini", "language=french" + "\r");
            if (_selectorChoice == 1) System.IO.File.WriteAllText("config.ini", "language=english" + "\r");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("config.ini", true))
            {
                for (int i = 1; i < copy.Length - 1; i++)
                {
                    file.WriteLine(copy[i]);
                }
                file.Write(copy[copy.Length - 1]);
            }

            Language.Initialize();
            System.Threading.Thread.Sleep(100);
        }

    }
}
