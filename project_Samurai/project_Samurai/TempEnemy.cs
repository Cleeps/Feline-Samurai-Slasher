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
        // Constructor
        public TempEnemy(Texture2D text, Vector2 pos)
        {
            texture = text;
            position = pos;
        }

        public void Update()
        {
            position.X--;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(this.texture, this.position, Color.Red);
        }
    }
}
