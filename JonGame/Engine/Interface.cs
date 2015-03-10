using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JonGame.Engine
{
    public class Interface
    {
        Game1 mGame;

        SpriteFont mFont;

        string mEntityCount = "";
        string mInstructions = "                   Press space to spawn Balls!\nHover over Balls with you'r mouse to remove them!";
        string mMadeBy = "                   Jon Felix Jennemann\njon@jonfelix.net www.JonFelix.Net";
        string mTimer;
        string mFPSCounter = "";
        Vector2 mInstructionPosition;
        Vector2 mMadeByPosition;
        Vector2 mTimerPosition;

        int mRemoveCounter;

        public SpriteFont Font
        {
            get
            {
                return mFont;
            }
            set
            {
                mFont = value;
            }
        }

        public int RemoveCounter
        {
            set
            {
                mRemoveCounter += value;
            }
        }

        public Interface(Game1 Game)
        {
            mGame = Game;
        }

        public int Init()
        {

            return 0;
        }

        public int Update(GameTime gameTime)
        {

            BallCount(gameTime);
            mInstructionPosition = new Vector2(mGame.Width / 2 - (mFont.MeasureString(mInstructions).X / 2), mGame.Height - 10 - mFont.MeasureString(mInstructions).Y);
            mMadeByPosition = new Vector2(mGame.Width - 10 - mFont.MeasureString(mMadeBy).X, mGame.Height - 10 - mFont.MeasureString(mMadeBy).Y);
            Timer(gameTime);
            FPSCounter();
            return 0;
        }

        public int Draw()
        {
            mGame.SpriteBatch.DrawString(mFont, mEntityCount, new Vector2(10, 10), Color.Black);
            mGame.SpriteBatch.DrawString(mFont, mInstructions, mInstructionPosition, Color.Black);
            mGame.SpriteBatch.DrawString(mFont, mMadeBy, mMadeByPosition, Color.Black);
            mGame.SpriteBatch.DrawString(mFont, mTimer, mTimerPosition, Color.Black);
            mGame.SpriteBatch.DrawString(mFont, mFPSCounter, new Vector2(10, mGame.Height - 10 - mFont.MeasureString(mFPSCounter).Y), Color.Black);
            return 0;
        }

        public int BallCount(GameTime gameTime)
        {
            mEntityCount = mGame.EntityManager.EntityCount.ToString() + " Balls active!\n" + mRemoveCounter + " Balls removed!";
            return 0;
        }

        public int Timer(GameTime gameTime)
        {
            mTimer = "";
            while (mTimer.Length + gameTime.TotalGameTime.Minutes.ToString().Length < 2)
            {
                mTimer += "0";
            }
            mTimer += gameTime.TotalGameTime.Minutes + ":";
            while (mTimer.Length + gameTime.TotalGameTime.Seconds.ToString().Length < 5)
            {
                mTimer += "0";
            }
            mTimer += gameTime.TotalGameTime.Seconds;
            mTimerPosition = new Vector2(mGame.Width / 2 - (mFont.MeasureString(mTimer).X / 2), 10);
            return 0;
        }

        public int FPSCounter()
        {
            mFPSCounter = "FPS: " + mGame.FPS.AverageFPS + "(Average)\n        " + mGame.FPS.CurrentFPS + "(Current)";
            return 0;
        }
    }
}
