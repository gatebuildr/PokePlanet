#region Using Statements

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace PokePlanet
{
    static class GameStates
    {
         public static readonly IGameState OverWorldState = new OverworldState();
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private IGameState _currentGameState;

        SpriteBatch _spriteBatch;
        //input states

        //background
        public static Texture2D MainBackground;
        public static Rectangle RectBackground;
        public static ParallaxingBackground BgLayer1;
        public static ParallaxingBackground BgLayer2;
        public static float FramesElapsed = 0;

        // We actually need this to be assigned in the constructor or XNA derps out and doesn't load
        // ReSharper disable once NotAccessedField.Local
        private GraphicsDeviceManager _graphics;
        public const float Scale = 4f; 


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
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
            BgLayer1 = new ParallaxingBackground();
            BgLayer2 = new ParallaxingBackground();
           

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Console.WriteLine("Game1.LoadContent");
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
            Input.UpdateControlStates(OverworldState.Player.Position);
            UpdatePlayer(gameTime);
            UpdatePlayerPosition();

            BgLayer1.Update(gameTime);
            BgLayer2.Update(gameTime);

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            OverworldState.Player.Update(gameTime);
        }

        void UpdatePlayerPosition()
        {
            Vector2 moveVector = Input.GetMoveVector(Player.PlayerMoveSpeed);
            OverworldState.Player.Move(moveVector);

            if (Input.InputDirection == Vector2.Zero)
                return;

            switch (Input.CurrentInputDirectionType)
            {
                    case Input.InputDirectionType.Absolute:
                    OverworldState.Player.Position += Input.InputDirection;
                    break;
                    case Input.InputDirectionType.Full:
                    Input.InputDirection.Normalize();
                    OverworldState.Player.Position += Input.InputDirection*Player.PlayerMoveSpeed;
                    break;
                    case Input.InputDirectionType.Scaled:
                    OverworldState.Player.Position += Input.InputDirection*Player.PlayerMoveSpeed;
                    break;
            }

            //make sure player stays in bounds

// ReSharper disable once PossibleLossOfFraction
            OverworldState.Player.Position.X = MathHelper.Clamp(OverworldState.Player.Position.X, OverworldState.Player.Width/2, GraphicsDevice.Viewport.Width - OverworldState.Player.Width/2);
// ReSharper disable once PossibleLossOfFraction
            OverworldState.Player.Position.Y = MathHelper.Clamp(OverworldState.Player.Position.Y, OverworldState.Player.Height/2, GraphicsDevice.Viewport.Height - OverworldState.Player.Height/2);
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
            _spriteBatch.Begin();

            //draw the background
            _spriteBatch.Draw(MainBackground, RectBackground, Color.White);
            BgLayer1.Draw(_spriteBatch);
            BgLayer2.Draw(_spriteBatch);

            //draw the player
            OverworldState.Player.Draw(_spriteBatch, Scale);
            //stop drawing
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
