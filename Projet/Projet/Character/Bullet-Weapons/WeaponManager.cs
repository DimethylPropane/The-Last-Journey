using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projet
{
    class WeaponManager
    {
        private static List<Weapon> weaponList = new List<Weapon>();
        private static Color colour = new Color(255, 255, 255, 255);
        private static bool down = false;
        private static Texture2D pixelTest = ContentManagerGet.Give().Load<Texture2D>("PIXELTEXTURETEST");

        public static void Add(Weapon weapon)
        {
            weaponList.Add(weapon);
            weapon.WeaponRectangle = new Rectangle((int)weapon.Position.X - 10 - 20 * Convert.ToInt32(Math.Cos(weapon.Rotation - 2f)),
                (int)weapon.Position.Y - 10 - 20 * Convert.ToInt32(Math.Sin(weapon.Rotation - 2f)),
                25, 25);
        }

        public static void Remove(Weapon weapon)
        {
            weaponList.Remove(weapon);
        }

        public static void Update(GameTime gameTime)
        {
            if (colour.A == 255) down = false;
            if (colour.A == 0) down = true;
            if (down) colour.A += 5; else colour.A -= 5;

        }

        public static void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            foreach (Weapon weapon in weaponList)
            {
                if (!weapon._isEquipped)
                {
                    spritebatch.Draw(pixelTest, weapon.WeaponRectangle, Color.White);
                    spritebatch.Draw(weapon.Texture, weapon.Position, null, colour, weapon.Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                }
            }
            spritebatch.End();
        }

        public static List<Weapon> WeaponList
        {
            get { return weaponList; }
        }

    }
}
