using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_Samurai
{
    class Tile : GameObject
    {
        // Tile Fields
        Rectangle tileData;
        Texture2D tileTexture;

        // Tile Properties
        public int TileX
        {
            get { return tileData.X; }
        }

        public int TileY
        {
            get { return tileData.Y; }
        }

        public Texture2D Texture
        {
            get { return tileTexture; }
        }

        // Tile Constructor
        public Tile(Rectangle data, Texture2D text)
        {
            tileData = data;
            tileTexture = text;
        }

        // Tile Methods
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(tileTexture, tileData, Color.White);
        }
    }
}
