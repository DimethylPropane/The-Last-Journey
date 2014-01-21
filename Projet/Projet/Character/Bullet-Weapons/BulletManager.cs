using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projet
{
    class BulletManager
    {
        private static List<Bullet> _bulletList = new List<Bullet>();
        private static List<Bullet> _bulletListRemove = new List<Bullet>();

        public static void Add(Bullet bullet)
        {
            _bulletList.Add(bullet);
        }

        public static void Remove(Bullet bullet)
        {
            _bulletListRemove.Add(bullet);
        }

        public static void Update()
        {
            foreach (Bullet bullet in _bulletList)
            {
                bullet.Update();
            }

            foreach (Bullet bullet in _bulletListRemove)
            {
                _bulletList.Remove(bullet);
            }


            _bulletListRemove.Clear();
        }

        public static void Draw(SpriteBatch spritebatch)
        {
            foreach (Bullet bullet in _bulletList)
            {
                bullet.Draw(spritebatch);
            }
        }

    }
}
