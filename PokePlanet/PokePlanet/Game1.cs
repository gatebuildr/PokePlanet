#region Using Statements

using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace PokePlanet
{
    static class GameStates
    {
         public static readonly GameState OverWorldState = new OverworldState();
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GameState _currentGameState;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //input states

        //background
        public static Texture2D _mainBackground;
        public static Rectangle _rectBackground;
        public static ParallaxingBackground bgLayer1;
        public static ParallaxingBackground bgLayer2;
        public const float Scale = 4f; 


        public Game1()
            : base()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Input.Initialize();

            _currentGameState = GameStates.OverWorldState;
            _currentGameState.Initialize();

            //background
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();
           

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            SpriteManager.LoadContent(Content, GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Input.ExitPressed())
                Exit();

            // TODO: Add your update logic here
            //save previous input states so we can pick out single presses
            Input.UpdateControlStates(OverworldState.player.Position);
            UpdatePlayer(gameTime);
            UpdatePlayerPosition();

            bgLayer1.Update(gameTime);
            bgLayer2.Update(gameTime);

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            OverworldState.player.Update(gameTime);
        }

        void UpdatePlayerPosition()
        {
            if (Input.InputDirection == Vector2.Zero)
                return;

            switch (Input.CurrentInputDirectionType)
            {
                    case Input.InputDirectionType.Absolute:
                    OverworldState.player.Position += Input.InputDirection;
                    break;
                    case Input.InputDirectionType.Full:
                    Input.InputDirection.Normalize();
                    OverworldState.player.Position += Input.InputDirection*Player.PlayerMoveSpeed;
                    break;
                    case Input.InputDirectionType.Scaled:
                    OverworldState.player.Position += Input.InputDirection*Player.PlayerMoveSpeed;
                    break;
            }

            //make sure player stays in bounds

            OverworldState.player.Position.X = MathHelper.Clamp(OverworldState.player.Position.X, 0, GraphicsDevice.Viewport.Width - OverworldState.player.Width);
            OverworldState.player.Position.Y = MathHelper.Clamp(OverworldState.player.Position.Y, 0, GraphicsDevice.Viewport.Height - OverworldState.player.Height);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //start drawing
            spriteBatch.Begin();

            //draw the background
            spriteBatch.Draw(_mainBackground, _rectBackground, Color.White);
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);

            //draw the player
            OverworldState.player.Draw(spriteBatch, Scale);
            //stop drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
