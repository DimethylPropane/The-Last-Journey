using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Projet
{
    class WeaponMove
    {
        public static void Release(ref Weapon weapon, Character character)
        {
            weapon._isEquipped = false;
            weapon.Position = new Vector2(weapon.Position.X - weapon.CenterCaracter.X, weapon.Position.Y - weapon.CenterCaracter.Y);
            weapon.WeaponRectangle = new Rectangle((int)weapon.Position.X - 10 - 20 * Convert.ToInt32(Math.Cos(weapon.Rotation - 2f)),
                (int)weapon.Position.Y - 10 - 20 * Convert.ToInt32(Math.Sin(weapon.Rotation - 2f)), 25, 25);

            weapon = null;
        }

        public static Weapon Take(Character character)
        {
            foreach (Weapon weapon in WeaponManager.WeaponList)
            {
                if (weapon.WeaponRectangle.Intersects(character.CharacterRectangle))
                {
                    weapon._isEquipped = true;
                    weapon.CenterCaracter = new Vector2(character.Width / 2, character.Height / 2);
                    return weapon;
                }
            }

            return null;
        }
    }
}
