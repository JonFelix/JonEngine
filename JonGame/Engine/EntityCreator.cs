using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace JonGame.Engine
{
    public class EntityCreator
    {
        Random mRandDir;
        Game1 mGame;

        public EntityCreator(Game1 Game)
        {
            mGame = Game;
            mRandDir = new Random();
        }

        public int Ball(int X, int Y)
        {
            Entity mEntity = new Entity(X, Y, mGame);
            mEntity.Texture = mGame.Content.Load<Texture2D>("Sprites/Ball");
            mEntity.HSpeed = mRandDir.Next(-50, 50);
            mEntity.VSpeed = mRandDir.Next(-50, 50);
            mGame.EntityManager.AddEntity(mEntity);
            return 0;
        }
    }
}
