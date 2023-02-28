using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_Samurai
{
    class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle boundingBox;

        // Properties
        public Rectangle BoundingBox
        {
            get { return boundingBox; }
        }

        // Check Collision method
        public bool CheckCollision(GameObject objectOne, GameObject objectTwo)
        {
            if (objectOne.boundingBox.Intersects(objectTwo.boundingBox))
            {
                return true;
            }

            return false;
        }

        // Check Moving Collision
        public bool CheckDistantCollision(Cat player, GameObject objectTwo)
        {
            Rectangle checkRectangle = new Rectangle((int)(player.XPos + player.MovementX), (int)(player.YPos + player.MovementY), player.texture.Width, player.texture.Height);

            if (checkRectangle.Intersects(objectTwo.BoundingBox))
            {
                return true;
            }

            return false;
        }

        // Determine Player Sector
        public List<Tile> GetNearbyTiles(Cat player, Tile[,] tileLayout)
        {
            List<Tile> tilesToCheck = new List<Tile>();

            for (int i = 0; i < tileLayout.GetLength(0); i++)
            {
                for (int j = 0; j < tileLayout.GetLength(1); j++)
                {
                    if (tileLayout[i, j] != null && player.boundingBox.Intersects(tileLayout[i, j].boundingBox))
                    {
                        tilesToCheck.Add(tileLayout[i, j]);
                    }
                }
            }

            return tilesToCheck;
        }
    }
}
