using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JonGame.Engine
{
    class InputManager
    {
        Game1 mGame;
        float mElapsedTime;

        public InputManager(Game1 Game)
        {
            mGame = Game;
            mElapsedTime = 100f;
        }

        public int Update(GameTime gameTime)
        {
            if(mElapsedTime < 100f)
            {
                mElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                if(Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    mGame.EntityCreator.Ball(mGame.Width / 2, mGame.Height / 2);
                    mElapsedTime = 0f;
                }
            }
            
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                mGame.Exit();
            }

            return 0;
        }
    }
}
