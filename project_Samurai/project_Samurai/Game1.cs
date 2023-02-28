using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace project_Samurai
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Temporary Texture Data
        private Texture2D cat;
        private Texture2D tileTexture;

        // Player
        private Cat felineSamurai;

        // Level Editor Info
        private StageLoader stageLoader;

        // Temp Enemies
        private TempEnemy tempEnemy;
        private TempEnemy tempEnemy2;
        private TempEnemy tempEnemy3;

        // Change this to the list?
        private List<TempEnemy> enemyList;
        private List<Tile> tileList;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 384;
            _graphics.PreferredBackBufferHeight = 384;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            cat = Content.Load<Texture2D>("samurai");
            tileTexture = Content.Load<Texture2D>("temporaryTile");

            // Temporary creation calls
            felineSamurai = new Cat(cat, new Vector2(150, 200), new Rectangle(150, 200, 32, 64));

            // Stage Loader
            stageLoader = new StageLoader(tileTexture);
            stageLoader.LoadLevel(1);

            // Temp enemy list creation
            enemyList = new List<TempEnemy>();
            tempEnemy = new TempEnemy(cat, new Vector2(550, 200), new Rectangle(550, 200, 32, 64));
            tempEnemy2 = new TempEnemy(cat, new Vector2(680, 200), new Rectangle(680, 200, 32, 64));
            tempEnemy3 = new TempEnemy(cat, new Vector2(710, 100), new Rectangle(780, 300, 32, 64));

            // Temp enemy list addition
            enemyList.Add(tempEnemy);
            enemyList.Add(tempEnemy2);
            enemyList.Add(tempEnemy3);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            tempEnemy.Update();
            tempEnemy2.Update();
            tempEnemy3.Update();
            felineSamurai.Update(enemyList, stageLoader.TileLayout);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            // Drawing Enemies
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Draw(_spriteBatch);
            }

            // Drawing Tiles
            stageLoader.Draw(_spriteBatch);

            // Drawing the Player 
            felineSamurai.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
