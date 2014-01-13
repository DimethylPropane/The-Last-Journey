using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projet
{
    class Entity
    {
        //***********Attributs***********\\
        protected Game _game;
        protected Vector2 _position;
        protected Texture2D _texture;
        protected Vector2 _speed;
        protected bool _active;

        //***********Constructeurs***********\\
        public Entity (Game game) 
        {
            _game = game;
            _position = Vector2.Zero;
            _speed = Vector2.One;
            _active = true;
        }

        public Entity (Game game, Vector2 position): this(game) 
        {
            _position = position;
        }

        //***********Accesseurs***********\\

        //Position de l'entité
        public Vector2 Position
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
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        // Largeur de l'image
        public int Width
        {
            get { return _texture.Width; }
        }

        // Hauteur de l'image
        public int Height
        {
            get { return _texture.Height; }
        }

        // Définit si le sprite est actif
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        // Objet Game
        public Game Game
        {
            get { return _game; }
        }

        // ContentManager pour charger les ressources
        public ContentManager Content
        {
            get { return _game.Content; }
        }

        //***********Méthodes***********\\
        public virtual void Initialize()
        {
            _active = true;
        }

        public virtual void LoadContent(string textureName)
        {
            _texture = Content.Load<Texture2D>(textureName);
        }
        
        public virtual void UnloadContent()
        {
            if (_texture != null)
                _texture.Dispose();
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (_active)
                spriteBatch.Draw(_texture, _position, Color.White);

            spriteBatch.End();
        }
    }
}
