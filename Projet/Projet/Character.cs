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
        protected bool _stop;

        //***********Constructeurs***********\\

        public Character(Game game, SimpleAnimationDefinition definition, GraphicsDeviceManager graphics)
            : base(game,definition)
        {
            _window = new Vector2(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
        }


        //***********Methodes***********\\

        public override void Initialize()
        {
            Framerate = _definition.FrameRate;
            _position = new Vector2(_window.X / 2, _window.Y / 2);
            _stop = true;
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
            _mousePosition = new Vector2(mouse.X, mouse.Y);

            Vector2 direction = _mousePosition - _position;
            direction.Normalize();

            //if (mouse.LeftButton == ButtonState.Pressed)
            _rotation = (float)Math.Atan2((double)direction.Y, (double)direction.X);

            

            //Deplacement
            _stop = true;
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

        }


        public override void Draw(GameTime time, bool DoBeginEnd)
        {
            if (DoBeginEnd) _spriteBatch.Begin();

            if (_stop)
            {
                _spriteBatch.Draw(_sprite,
                                  new Rectangle((int)_position.X - Width / 2, (int)_position.Y - Height / 2, _definition.FrameSize.X, _definition.FrameSize.Y),
                                  new Rectangle(1 * _definition.FrameSize.X, 1 * _definition.FrameSize.Y, _definition.FrameSize.X, _definition.FrameSize.Y),
                                  Color.White, _rotation, new Vector2(Width / 2, Height / 2), SpriteEffects.None, 0f);
            }

            else
            {
                _spriteBatch.Draw(_sprite,
                                      new Rectangle((int)_position.X - Width / 2, (int)_position.Y - Height / 2, _definition.FrameSize.X, _definition.FrameSize.Y),
                                      new Rectangle(_currentFrame.X * _definition.FrameSize.X, _currentFrame.Y * _definition.FrameSize.Y, _definition.FrameSize.X, _definition.FrameSize.Y),
                                      Color.White, _rotation, new Vector2(Width / 2, Height / 2), SpriteEffects.None, 0f);
            }

            if (DoBeginEnd) _spriteBatch.End();
        }
    }
}


