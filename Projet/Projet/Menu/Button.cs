using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projet
{
    class Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        Color colour = new Color(255, 255, 255, 255);
        public Vector2 size;


        public Button(Texture2D _texture)
        {
            texture = _texture;
            size = new Vector2(288, 144);
            position = new Vector2(25, 25);
        }

        bool down;
        public bool isClicked = false;
        public bool isMouseOver;
        public bool isSelected;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            isMouseOver = false;
            isMouseOver = mouseRectangle.Intersects(rectangle) || isSelected;
            if (isMouseOver)
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 5; else colour.A -= 5;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;

                isMouseOver = mouseRectangle.Intersects(rectangle);
            }

            else if (colour.A < 255)
            {
                colour.A += 5;
                isClicked = false;
            }

            isSelected = false;
        }

        public void SetPosition(Vector2 _position)
        {
            position = _position;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rectangle, colour);
        }

    }
}
