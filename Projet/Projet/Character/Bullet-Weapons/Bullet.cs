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
    class Bullet
    {
        protected Vector2 _position;
        protected Vector2 _speed;
        private Texture2D _texture;
        private float _rotAngle;
        

        public Bullet(Vector2 startPosition, string weaponID, float angle, ContentManager Content)
        {
            MouseState mouse = Mouse.GetState();
            _position = startPosition;
            _speed = new Vector2(Convert.ToInt32(20*Math.Sin(angle)),Convert.ToInt32(-20*Math.Cos(angle)));
            _rotAngle = angle;
            _texture = Content.Load<Texture2D>("bullet");
            BulletManager.Add(this);
        }

        public void Update()
        {
            _position.X += _speed.X;
            _position.Y += _speed.Y;

            if (_position.X < 0 || _position.Y < 0 || _position.X > 1500 || _position.Y > 1000)
            {
                BulletManager.Remove(this);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(_texture, _position, null, Color.White, _rotAngle, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spritebatch.End();
        }


    }
}
