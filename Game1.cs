using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Village_Racing
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        enum GameStates { Playing, TitleScreen };
        GameStates currentState = GameStates.TitleScreen;
        SpriteBatch spriteBatch;
        Player player;
        TileMap tiles;
        TitleScreen mainScreen;
        Camera camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            camera = new Camera(GraphicsDevice.Viewport);
            camera.Limits = new Rectangle(0, 0, 10000, 10000);
            mainScreen = new TitleScreen(Content.Load<Texture2D>("SmallCloud"), Content.Load<Texture2D>("LargeCloud"), Content.Load<Texture2D>("Hill1"), Content.Load<Texture2D>("Hill2"), Content.Load<Texture2D>("Background"));
            tiles = new TileMap(Content.Load<Texture2D>("Brick"), Content.Load<Texture2D>("BB1"), camera);
            player = new Player(Content.Load<Texture2D>("head"), Content.Load<Texture2D>("body"), Content.Load<Texture2D>("foot"), Color.DarkViolet, new Vector2(200, 200), tiles);
            tiles.toLevel();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (currentState == GameStates.Playing)
            {
                tiles.Update();
                player.Update(gameTime);
                camera.LookAt(player.Position);
                
            }
            else
            {
                mainScreen.Update(gameTime);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    currentState = GameStates.Playing;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(new Vector2(1.0f)));
            if (currentState == GameStates.Playing)
            {
                tiles.Draw(spriteBatch);
                player.Draw(spriteBatch);
            }
            else
            {
                mainScreen.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
