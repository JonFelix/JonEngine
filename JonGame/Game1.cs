using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using JonGame.Engine;

namespace JonGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager mGraphics;
        SpriteBatch mSpriteBatch;

        string mTitle = "Jons Game";
        string mVersion = "0.01A";

        string mAssets = "Content";

        int mHeight = 480;
        int mWidth = 800;

        string mGameFont = "Fonts/PuristaSemiBold";

        EntityManager mEntityManager;
        EntityCreator mEntityCreator;
        InputManager mInputManager;
        Interface mInterface;
        FPSManager mFPSManager;

        public int Height
        {
            get
            {
                return mHeight;
            }
        }

        public int Width
        {
            get
            {
                return mWidth;
            }
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return mSpriteBatch;
            }
        }

        public EntityManager EntityManager
        {
            get
            {
                return mEntityManager;
            }
        }

        public EntityCreator EntityCreator
        {
            get
            {
                return mEntityCreator;
            }
        }

        public Interface Interface
        {
            get
            {
                return mInterface;
            }
        }

        public FPSManager FPS
        {
            get
            {
                return mFPSManager;
            }
        }

        public Game1() : base()
        {
            mGraphics = new GraphicsDeviceManager(this);
            mEntityManager = new EntityManager(this);
            mEntityCreator = new EntityCreator(this);
            mInputManager = new InputManager(this);
            mInterface = new Interface(this);
            mFPSManager = new FPSManager(this);
            
            Content.RootDirectory = mAssets;
        }

        protected override void Initialize()
        {
            base.Initialize();
            mEntityManager.Init();
            mFPSManager.Init();
            base.Window.Title = mTitle + " V" + mVersion;
            base.IsMouseVisible = true;
            mInterface.Font = Content.Load<SpriteFont>(mGameFont);
            mSpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            mInputManager.Update(gameTime);
            mEntityManager.Update(gameTime);
            mInterface.Update(gameTime);
            mFPSManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            base.Draw(gameTime);
            mSpriteBatch.Begin();
            mEntityManager.Draw();
            mInterface.Draw();
            mSpriteBatch.End();
            mFPSManager.Draw();
        }
    }
}
