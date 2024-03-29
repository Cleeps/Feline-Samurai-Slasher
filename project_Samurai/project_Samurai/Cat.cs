﻿using System;
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
        private bool gravityActive;
        List<Tile> tilesToCheck;

        // Properties
        public float XPos
        {
            get { return position.X; }
        }

        public float YPos
        {
            get { return position.Y; }
        }

        public float MovementX
        {
            get { return movement.X; }
        }

        public float MovementY
        {
            get { return movement.Y; }
        }

        // Constructor
        public Cat(Texture2D text, Vector2 pos, Rectangle box)
        {
            texture = text;
            position = pos;
            boundingBox = box;
        }

        // Update method
        public void Update(List<TempEnemy> enemyList, Tile[,] tileLayout)
        {
            currentKeyboardState = Keyboard.GetState();
            
            /*if ((tilesToCheck = GetNearbyTiles(this, tileLayout)) != null)
            {
                foreach (Tile t in tilesToCheck)
                {
                    if (CheckDistantCollision(this, t))
                    {
                        while (!this.boundingBox.Intersects(t.BoundingBox))
                        {
                            this.position.X += MovementX / MovementX;
                            this.position.Y += MovementY / MovementY;
                        }

                        movement.X = 0;
                        movement.Y = 0;
                    }
                }
            }*/

            // Player State Machine
            switch (playerState)
            {
                // Idle States
                case playerStates.idleRight:
                case playerStates.idleLeft:

                    CheckDirection();

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
                        for (int i = 0; i < enemyList.Count; i++)
                        {
                            if (enemyList[i] != null)
                            {
                                if (CheckCollision(this, enemyList[i]))
                                {
                                    enemyList.RemoveAt(i);
                                    i -= 1;
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

                    if (dashTimer < 15)
                    {
                        // Direction State Machine
                        switch (currentDirection)
                        {
                            case directions.up:
                                this.movement.Y = -10;
                                this.movement.X = 0;
                                break;
                            case directions.left:
                                this.movement.X = -10;
                                this.movement.Y = 0;
                                break;
                            case directions.right:
                                this.movement.X = 10;
                                this.movement.Y = 0;
                                break;
                            case directions.down:
                                this.movement.Y = -10;
                                this.movement.X = 0;
                                break;

                            case directions.upLeft:
                                this.movement.X = -7.1f;
                                this.movement.Y = -7.1f;
                                break;

                            case directions.upRight:
                                this.movement.X = 7.1f;
                                this.movement.Y = -7.1f;
                                break;

                            case directions.downLeft:
                                this.movement.X = -7.1f;
                                this.movement.Y = 7.1f;
                                break;

                            case directions.downRight:
                                this.movement.X = 7.1f;
                                this.movement.Y = 7.1f;
                                break;
                        }

                        // Maybe switch for a helper method?
                        for (int i = 0; i < enemyList.Count; i++)
                        {
                            if (enemyList[i] != null)
                            {
                                if (CheckCollision(this, enemyList[i]))
                                {
                                    enemyList.RemoveAt(i);
                                    i -= 1;

                                    // Re-check the direction to account for any changes
                                    CheckDirection();

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
                        movement.Y = 0;
                        dashTimer = 0;
                        playerState = prevPlayerState;
                    }

                    dashTimer++;

                    break;
            }

            // Tile Collision
            if ((tilesToCheck = GetNearbyTiles(this, tileLayout)) != null)
            {
                foreach (Tile t in tileLayout)
                {
                    if (t != null && CheckCollision(this, t))
                    {
                        movement.X = 0;
                        movement.Y = 0;
                    }
                }
            }

            // Gravity State Machine
            if ((playerState == playerStates.idleLeft || playerState == playerStates.idleRight || playerState == playerStates.counter) &&
                 position.Y < 200)
            {
                // The player is affected by gravity
                gravityActive = true;
            }
            else
            {
                // The player is unaffected by gravity
                gravityActive = false;
            }

            position.X += movement.X;
            position.Y += movement.Y + (5 * (gravityActive ? 1 : 0));
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
                    /*_spriteBatch.Draw(texture, position, Color.White);
                    break;

                case playerStates.idleLeft:
                    _spriteBatch.Draw(texture, position, Color.White);*/

                    switch (currentDirection)
                    {
                        case directions.up:
                            _spriteBatch.Draw(texture, position, Color.Indigo);
                            break;
                        case directions.left:
                            _spriteBatch.Draw(texture, position, Color.Green);
                            break;
                        case directions.right:
                            _spriteBatch.Draw(texture, position, Color.White);
                            break;
                        case directions.down:
                            _spriteBatch.Draw(texture, position, Color.Orange);
                            break;

                        case directions.upLeft:
                            _spriteBatch.Draw(texture, position, Color.Blue);
                            break;
                        case directions.upRight:
                            _spriteBatch.Draw(texture, position, Color.Violet);
                            break;
                        case directions.downLeft:
                            _spriteBatch.Draw(texture, position, Color.Yellow);
                            break;
                        case directions.downRight:
                            _spriteBatch.Draw(texture, position, Color.Red);
                            break;
                    }

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

        private void CheckDirection()
        {
            // Movement input
            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                if (currentKeyboardState.IsKeyDown(Keys.A))
                {
                    currentDirection = directions.upLeft;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.D))
                {
                    currentDirection = directions.upRight;
                }
                else
                {
                    currentDirection = directions.up;
                }
            }
            else if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                if (currentKeyboardState.IsKeyDown(Keys.A))
                {
                    currentDirection = directions.downLeft;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.D))
                {
                    currentDirection = directions.downRight;
                }
                else
                {
                    currentDirection = directions.down;
                }
            }
            else if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                if (currentKeyboardState.IsKeyDown(Keys.W))
                {
                    currentDirection = directions.upLeft;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.S))
                {
                    currentDirection = directions.downLeft;
                }
                else
                {
                    currentDirection = directions.left;
                }
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                if (currentKeyboardState.IsKeyDown(Keys.W))
                {
                    currentDirection = directions.upRight;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.S))
                {
                    currentDirection = directions.downRight;
                }
                else
                {
                    currentDirection = directions.right;
                }
            }
            else
            {
                currentDirection = directions.right;
            }
        }
    }
}
