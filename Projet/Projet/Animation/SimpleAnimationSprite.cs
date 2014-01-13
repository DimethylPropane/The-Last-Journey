using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projet
{
    public class SimpleAnimationSprite
    {
        //***********Attributs***********\\
        //public Point Position;
        
        protected Game _game;
        protected Vector2 _position;
        protected Vector2 _speed = new Vector2(5, 5);
        protected SimpleAnimationDefinition _definition;
        protected SpriteBatch _spriteBatch;
        protected Texture2D _sprite;
        protected Point _currentFrame;
        protected bool _finishedAnimation = false;

        //rotation
        protected float _rotation;

        #region Framerate Property
        private int _Framerate = 60;
        protected double TimeBetweenFrame = 16; // 60 fps
        protected double lastFrameUpdatedTime = 0;
        public int Framerate
        {
            get { return _Framerate; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Framerate can't be less or equal to 0");
                if (_Framerate != value)
                {
                    _Framerate = value;
                    TimeBetweenFrame = 1000.0d / (double)_Framerate;
                }
            }
        }
        #endregion


        //***********Constructeurs***********\\
        public SimpleAnimationSprite(Game game, SimpleAnimationDefinition definition)
        {
            _game = game;
            _definition = definition;
            //Position = new Point();
            _currentFrame = new Point();
        }

        //***********Accesseurs***********\\

        //Position de l'entité
        public Vector2 PositionCenter
        {
            get { return _position; }
            set { _position = value; }
        }

        // Vitesse
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        // Texture du Sprite
        /*public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }*/

        // Largeur de l'image
        public int Width
        {
            get { return _sprite.Width / _definition.NbFrames.X; }
        }

        // Hauteur de l'image
        public int Height
        {
            get { return _sprite.Height / _definition.NbFrames.Y; }
        }


        //***********Methodes***********\\
        public virtual void Initialize()
        {
            Framerate = _definition.FrameRate;
            _position =new Vector2 (0,0);
        }

        public void LoadContent(SpriteBatch spritebatch)
        {
            _sprite = _game.Content.Load<Texture2D>(_definition.AssetName);
            if (spritebatch == null)
                _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            else
                _spriteBatch = spritebatch;
         
            _rotation = 0;
        }

        public void Reset()
        {
            _currentFrame = new Point();
            _finishedAnimation = false;
            lastFrameUpdatedTime = 0;
        }

        public virtual void Update(GameTime time)
        {
            if (_finishedAnimation) return;
            lastFrameUpdatedTime += time.ElapsedGameTime.Milliseconds;
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


        }

        public virtual void Draw(GameTime time, bool DoBeginEnd)
        {
            if (DoBeginEnd) _spriteBatch.Begin();

            _spriteBatch.Draw(_sprite,
                                  new Rectangle((int)_position.X-_sprite.Width/2,(int) _position.Y-_sprite.Height/2, _definition.FrameSize.X, _definition.FrameSize.Y),
                                  new Rectangle(_currentFrame.X * _definition.FrameSize.X, _currentFrame.Y * _definition.FrameSize.Y, _definition.FrameSize.X, _definition.FrameSize.Y),
                                  Color.White); 

            if (DoBeginEnd) _spriteBatch.End();
        }
    }
}
