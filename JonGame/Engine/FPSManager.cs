using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JonGame.Engine
{
    public class FPSManager
    {
        Game1 mGame;

        DateTime mTimeCounter;

        int mDrawCounter;
        int mCurrentFPS;
        int mAverageFPS;
        int[] mLastTenResults;

        public int CurrentFPS
        {
            get
            { 
                return mCurrentFPS;
            }
        }

        public int AverageFPS
        {
            get
            {
                return mAverageFPS;
            }
        }

        public FPSManager(Game1 Game)
        {
            mGame = Game;
        }

        public int Init()
        {
            mTimeCounter = new DateTime();
            mTimeCounter = DateTime.Now;
            mLastTenResults = new int[10];
            return 0;
        }

        public int Update(GameTime gameTime)
        {
            
            if(DateTime.Now.Subtract(mTimeCounter).TotalSeconds >= 1)
            {
                mCurrentFPS = mDrawCounter;
                mTimeCounter = DateTime.Now;
                mDrawCounter = 0;
                for (int i = 1; i < mLastTenResults.Length; i++)
                {
                    mLastTenResults[i - 1] = mLastTenResults[i];
                }
                mLastTenResults[9] = mCurrentFPS;
                mAverageFPS = 0;
                for(int i = 0; i < mLastTenResults.Length; i++)
                {
                    mAverageFPS += mLastTenResults[i];
                }
                mAverageFPS /= 10;
                
            }
            return 0;
        }

        public int Draw()
        {
            mDrawCounter++;
            return 0;
        }
    }
}
