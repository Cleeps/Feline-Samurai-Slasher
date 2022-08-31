using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_Samurai
{
    enum playerStates
    { 
        idleLeft,
        idleRight,
        counter,
        slash,
        dash
    }

    enum directions
    {
        up,
        down,
        left,
        right,
        upLeft,
        upRight,
        downLeft,
        downRight
    }

    class Cat : GameObject
    {
        // Fields
        private playerStates playerState = playerStates.idleRight;
        private directions currentDirection = directions.right;
        private playerStates prevPlayerState;
        private int counterTimer;
        private int dashTimer;
        private Vector2 movement;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        // Constructor
        public Cat(Texture2D text, Vector2 pos, Rectangle box)
        {
            texture = text;
            position = pos;
            boundingBox = box;
        }

        // Update method
        public void Update(GameObject[] enemyList)
        {
            currentKeyboardState = Keyboard.GetState();

            switch (playerState)
            {
                // Idle States
                case playerStates.idleRight:
                case playerStates.idleLeft:

                    // Movement input
                    if (currentKeyboardState.IsKeyDown(Keys.W))
                    {
                        if ()
                        { 
                        
                        }
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.S))
                    {

                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.A))
                    {

                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.D))
                    { 
                    
                    }



                        if (currentKeyboardState.IsKeyDown(Keys.LeftShift) && !previousKeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        prevPlayerState = playerState;
                        playerState = playerStates.counter;
                    }
                    break;

                // Counter State
                case playerStates.counter:

                    counterTimer++;

                    if (counterTimer < 15)
                    {
                        // If a collision is detected here, send into the dash state

                        // Maybe switch for a helper method?
                        for (int i = 0; i < enemyList.Length; i++)
                        {
                            if (enemyList[i] != null)
                            {
                                if (CheckCollision(this, enemyList[i]))
                                {
                                    playerState = playerStates.dash;
                                }
                            }
                        }
                        //
                    }
                    else
                    {
                        counterTimer = 0;
                        playerState = prevPlayerState;
                    }

                    break;

                // Dash State
                case playerStates.dash:

                    if (dashTimer < 5)
                    {
                        this.movement.X = 20;

                        // Maybe switch for a helper method?
                        for (int i = 0; i < enemyList.Length; i++)
                        {
                            if (enemyList[i] != null)
                            {
                                if (CheckCollision(this, enemyList[i]))
                                {
                                    // Reset dash timer if a collision is dectected during movement
                                    dashTimer = 0;
                                }
                            }
                        }
                        //
                    }
                    else
                    {
                        movement.X = 0;
                        dashTimer = 0;
                        playerState = prevPlayerState;
                    }

                    dashTimer++;

                    break;

            }

            position += movement;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;

            previousKeyboardState = Keyboard.GetState();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            switch (playerState)
            {
                // Idle States
                case playerStates.idleRight:
                    _spriteBatch.Draw(texture, position, Color.White);
                    break;

                case playerStates.idleLeft:
                    _spriteBatch.Draw(texture, position, Color.White);
                    break;

                // Counter State
                case playerStates.counter:
                    _spriteBatch.Draw(texture, position, Color.Purple);
                    break;

                // Dash State
                case playerStates.dash:
                    _spriteBatch.Draw(texture, position, Color.White);
                    break;
            }
        }
    }
}
