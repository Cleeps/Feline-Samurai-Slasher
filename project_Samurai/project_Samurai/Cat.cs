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

    class Cat : GameObject
    {
        // Fields
        private playerStates playerState = playerStates.idleRight;
        private playerStates prevPlayerState;
        private int counterTimer;
        private int dashTimer;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        // Constructor
        public Cat(Texture2D text, Vector2 pos)
        {
            texture = text;
            position = pos;
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

                        for (int i = 0; i < enemyList.Length; i++)
                        {
                            if (CheckCollision(this, enemyList[i]))
                            {
                                playerState = playerStates.dash;
                            }
                        }
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
                        this.position.X += 20;
                    }
                    else
                    {
                        dashTimer = 0;
                        playerState = prevPlayerState;
                    }

                    dashTimer++;

                    break;

            }

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
