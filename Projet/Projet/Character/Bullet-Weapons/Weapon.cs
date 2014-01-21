using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projet
{
    class Weapon
    {
        public bool _isEquipped;
        private string _name;
        private Texture2D _spriteEquipped, _pixelTest;
        private Vector2 _position, _centerCaracter, size;
        private int _maxAmmo, _currentAmmo;
        private float _reloadTime, _reloading,_rotation;
        private Rectangle weaponRectangle;

        protected SpriteFont font;

        public Weapon(string name, int maxAmmo, int currentAmmo, float reloadTime, bool isEquipped, Vector2 position, float rotation, Character perso)
        {
            _name = name;
            _maxAmmo = maxAmmo;
            _currentAmmo = currentAmmo;
            _reloadTime = reloadTime;
            _isEquipped = isEquipped;
            _position = position;
            _reloading = reloadTime;
            _rotation = rotation;
            _spriteEquipped = ContentManagerGet.Give().Load<Texture2D>("wp_1");
            _pixelTest = ContentManagerGet.Give().Load<Texture2D>("PIXELTEXTURETEST");
            if (perso != null) _centerCaracter = new Vector2(perso.Width / 2, perso.Height / 2);
            font = ContentManagerGet.Give().Load<SpriteFont>("SpriteFont1");
            size = new Vector2(25, 25);
            weaponRectangle = new Rectangle((int)_position.X - Convert.ToInt32(Math.Cos(_rotation)), (int)_position.Y + 15* Convert.ToInt32(Math.Sin(_rotation)), (int)size.X, (int)size.Y);
            WeaponManager.Add(this);
        }

        public void Update(GameTime gametime, float rotation, Vector2 position)
        {
            if (_isEquipped)
            {
                _position.X = position.X;
                _position.Y = position.Y;
                _rotation = rotation;
                weaponRectangle = new Rectangle((int)_position.X -10- 20*Convert.ToInt32(Math.Cos(_rotation)), (int)_position.Y -10- 20*Convert.ToInt32(Math.Sin(_rotation)), (int)size.X, (int)size.Y);
                if (_reloading < _reloadTime)
                    _reloading += gametime.ElapsedGameTime.Milliseconds;

                MouseState mouse = Mouse.GetState();
                if (mouse.LeftButton == ButtonState.Pressed && _reloading >= _reloadTime && _currentAmmo > 0)
                {
                    Bullet bullet = new Bullet(_position, _name, _rotation, ContentManagerGet.Give());
                    _currentAmmo--;
                    _reloading = 0;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(_spriteEquipped, _position, null, Color.White, _rotation, new Vector2(_centerCaracter.X, _centerCaracter.Y ), 1f, SpriteEffects.None, 0f);
            spritebatch.Draw(_pixelTest, weaponRectangle, Color.White);
            spritebatch.DrawString(font, "Current ammo: " + _currentAmmo, Vector2.Zero, Color.White);
            spritebatch.End();
        }


        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Texture2D Texture
        {
            get { return _spriteEquipped; }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Rectangle WeaponRectangle
        {
            get { return weaponRectangle; }
            set { weaponRectangle = value; }
        }

        public Vector2 CenterCaracter
        {
            get { return _centerCaracter; }
            set { _centerCaracter = value; }
        }
    }
}
