using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_Samurai
{
    class TempEnemy : GameObject
    {
        private Vector2 movement;

        // Constructor
        public TempEnemy(Texture2D text, Vector2 pos, Rectangle box)
        {
            texture = text;
            position = pos;
            boundingBox = box;
        }

        public void Update()
        {
            position.X--;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(this.texture, this.position, Color.Red);
        }
    }
}
