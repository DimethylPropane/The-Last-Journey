using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet
{
    class Character : SimpleAnimationSprite
    {
        //***********Attributs***********\\
        protected Vector2 _window; //Pour permettre d'avoir les dimensions de la fenêtre
        protected Vector2 _mousePosition;
        private Weapon _weapon;
        private Vector2 _halfSprite;
        private float _minX, _minY, _maxX, _maxY;
        private Rectangle characterRectangle;
        protected bool _stop;
        private Texture2D _pixelTest;

        //***********Constructeurs***********\\

        public Character(Game game, SimpleAnimationDefinition definition, GraphicsDeviceManager graphics)
            : base(game,definition)
        {
            _window = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            characterRectangle = new Rectangle((int)_position.X, (int)_position.Y, 30, 30);
        }


        //***********Methodes***********\\

        public override void Initialize()
        {
            Framerate = _definition.FrameRate;
            _stop = true;
        }

        public override void LoadContent(SpriteBatch spritebatch)
        {
            _sprite = _game.Content.Load<Texture2D>(_definition.AssetName);
            if (spritebatch == null)
                _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            else
                _spriteBatch = spritebatch;

            _weapon = new Weapon("joli nom", 25, 25, 100f, true, _position, _rotation, this);
            _pixelTest = ContentManagerGet.Give().Load<Texture2D>("PIXELTEXTURETEST");

            _halfSprite = new Vector2(Width / 2, Height / 2);
            _position = new Vector2(_window.X / 2 - Width/2, _window.Y/2 - Height/2);
            _minX = _window.X* 9 / 20;
            _maxX = _window.X* 11 / 20;
            _minY = _window.Y* 9 / 20;
            _maxY = _window.Y* 11 / 20;
            _rotation = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (_finishedAnimation) return;
            lastFrameUpdatedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (lastFrameUpdatedTime > TimeBetweenFrame)
            {
                lastFrameUpdatedTime = 0;
                if (_definition.Loop)
                {
                    _currentFrame.X++;
                    if (_currentFrame.X >= _definition.NbFrames.X)
                    {
                        _currentFrame.X = 0;
                        _currentFrame.Y++;
                        if (_currentFrame.Y >= _definition.NbFrames.Y)
                            _currentFrame.Y = 0;
                    }
                }
                else
                {
                    _currentFrame.X++;
                    if (_currentFrame.X >= _definition.NbFrames.X)
                    {
                        _currentFrame.X = 0;
                        _currentFrame.Y++;
                        if (_currentFrame.Y >= _definition.NbFrames.Y)
                        {
                            _currentFrame.X = _definition.NbFrames.X - 1;
                            _currentFrame.Y = _definition.NbFrames.Y - 1;
                            _finishedAnimation = true;
                        }
                    }
                }
            }

            //Rotation sprite perso avec souris
            MouseState mouse = Mouse.GetState();
            _mousePosition.X = mouse.X;
            _mousePosition.Y = mouse.Y;

            Vector2 direction = Vector2.Zero;
            direction.X = _mousePosition.X - _position.X;
            direction.Y = _mousePosition.Y - _position.Y;
            //direction.Normalize();

            //if (mouse.LeftButton == ButtonState.Pressed)
            _rotation = (float)Math.Atan2((double)direction.Y, (double)direction.X);
            _rotation += MathHelper.PiOver2;
            

            //Deplacement
            _stop = true; //Vérifie si le perso est à l'arrêt
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _position.Y -= (int)_speed.Y;
                _stop = false;
                Mouse.SetPosition(mouse.X, mouse.Y - (int)_speed.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _position.Y += (int)_speed.Y;
                Mouse.SetPosition(mouse.X, mouse.Y + (int)_speed.Y);
                _stop = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _position.X -= (int)_speed.X;
                Mouse.SetPosition(mouse.X - (int)_speed.X, mouse.Y);
                _stop = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _position.X += (int)_speed.X;
                Mouse.SetPosition(mouse.X + (int)_speed.X, mouse.Y);
                _stop = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _stop = false;
                Mouse.SetPosition(mouse.X - (int)_speed.X, mouse.Y - (int)_speed.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _stop = false;
                Mouse.SetPosition(mouse.X + (int)_speed.X, mouse.Y - (int)_speed.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _stop = false;
                Mouse.SetPosition(mouse.X - (int)_speed.X, mouse.Y + (int)_speed.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _stop = false;
                Mouse.SetPosition(mouse.X + (int)_speed.X, mouse.Y + (int)_speed.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Up)
                || (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Right)))
            {
                _stop = false;
                Mouse.SetPosition(mouse.X, mouse.Y);
            }


     /*       //Vérification caméra
            if (_position.X < _minX)
                _position.X = _minX;
            if (_position.X > _maxX)
                _position.X = _maxX;
            if (_position.Y > _maxY)
                _position.Y = _maxY;
            if (_position.Y < _minY)
                _position.Y = _minY;
    */
            if (mouse.RightButton == ButtonState.Pressed)
                if (_weapon != null)
                    WeaponMove.Release(ref _weapon, this);
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                if (_weapon == null)
                    _weapon = WeaponMove.Take(this);
            }
            

            if (_weapon != null) _weapon.Update(gameTime, _rotation, _position);
            characterRectangle = new Rectangle((int)_position.X - 15, (int)_position.Y - 15, 30, 30);
        }


        public override void Draw(GameTime time, bool DoBeginEnd)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_pixelTest, characterRectangle, Color.ForestGreen);
            _spriteBatch.End();


            if (_weapon != null) _weapon.Draw(_spriteBatch);
            if (DoBeginEnd) _spriteBatch.Begin();

            if (_stop)
            {
                _spriteBatch.Draw(_sprite,
                                  new Rectangle((int)_position.X , (int)_position.Y , _definition.FrameSize.X, _definition.FrameSize.Y),
                                  new Rectangle(0 * _definition.FrameSize.X, 1 * _definition.FrameSize.Y, _definition.FrameSize.X, _definition.FrameSize.Y),
                                  Color.White, _rotation, _halfSprite, SpriteEffects.None, 1.0f);
            }

            else
            {
                _spriteBatch.Draw(_sprite,
                                      new Rectangle((int)_position.X , (int)_position.Y , _definition.FrameSize.X, _definition.FrameSize.Y),
                                      new Rectangle(_currentFrame.X * _definition.FrameSize.X, _currentFrame.Y * _definition.FrameSize.Y, _definition.FrameSize.X, _definition.FrameSize.Y),
                                      Color.White, _rotation, _halfSprite, SpriteEffects.None, 1.0f);
            }


            if (DoBeginEnd) _spriteBatch.End();
        }

        public Rectangle CharacterRectangle
        {
            get { return characterRectangle; }
        }

        public Vector2 Position
        {
            get { return _position; }
        }
    }
}


