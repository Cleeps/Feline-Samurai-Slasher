using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_Samurai
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D cat;
        private Cat felineSamurai;
        private TempEnemy tempEnemy;
        private GameObject[] enemyList;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            cat = Content.Load<Texture2D>("samurai");

            felineSamurai = new Cat(cat, new Vector2(150, 200));
            tempEnemy = new TempEnemy(cat, new Vector2(550, 200));

            //enemyList = new GameObject[4];

        // TODO: use this.Content to load your game content here
    }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //enemyList[0] = tempEnemy;

            felineSamurai.Update(enemyList);
            tempEnemy.Update();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            felineSamurai.Draw(_spriteBatch);
            tempEnemy.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
