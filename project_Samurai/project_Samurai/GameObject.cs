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

        // Check Collision method
        public bool CheckCollision(GameObject objectOne, GameObject objectTwo)
        {
            return true;
        }
    }
}
