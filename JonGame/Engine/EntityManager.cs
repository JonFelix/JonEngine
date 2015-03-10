using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JonGame.Engine
{
    public class EntityManager
    {
        Entity mFirstEntity = null;
        Game1 mGame = null;
        int mIDIndex;
        int mEntityCount;

        public EntityManager(Game1 Game)
        {
            mGame = Game;
        }

        public int EntityCount
        {
            get
            {
                return mEntityCount;
            }
        }

        public int Init()
        {
            mIDIndex = 0;
            return 0;
        }

        public int Update(GameTime gameTime)
        {
            Entity mCurrentEntity = mFirstEntity;
            mEntityCount = 0;
            while(mCurrentEntity != null)
            {
                if (mCurrentEntity.IsCollidable)
                {
                    Entity mCheckEntity = mCurrentEntity.Next;
                    while (mCheckEntity != null)
                    {
                        if (mCheckEntity.IsCollidable)
                        {
                            if (Vector2.Distance(new Vector2(mCurrentEntity.X + (mCurrentEntity.Texture.Width / 2), mCurrentEntity.Y + (mCurrentEntity.Texture.Height / 2)),
                                                new Vector2(mCheckEntity.X + (mCheckEntity.Texture.Width / 2), mCheckEntity.Y + (mCheckEntity.Texture.Height / 2))) <= mCurrentEntity.Texture.Width)
                            {
                                mCurrentEntity.CollisionList = new[] { mCheckEntity };
                                mCheckEntity.CollisionList = new[] { mCurrentEntity };
                            }
                        }
                        mCheckEntity = mCheckEntity.Next;
                    }
                }
                mCurrentEntity = mCurrentEntity.Next;
            }

            mCurrentEntity = mFirstEntity;
            int mError = 0;
            while(mCurrentEntity != null)
            {
                if(!mCurrentEntity.IsActive)
                {
                    if(!mCurrentEntity.IsInitialized)
                    {
                        mError += mCurrentEntity.Init(mIDIndex++);
                    }
                    else
                    {
                        mCurrentEntity = DeleteEntity(mCurrentEntity);
                    }
                }
                if (mCurrentEntity != null)
                {
                    mError += mCurrentEntity.Update(gameTime);
                    mEntityCount++;
                    mCurrentEntity = mCurrentEntity.Next;
                }
            }
            return 0;
        }

        public int Draw()
        {
            Entity mCurrentEntity = mFirstEntity;
            int mError = 0;
            while (mCurrentEntity != null)
            {
                mError += mCurrentEntity.Draw();
                mCurrentEntity = mCurrentEntity.Next;
            }
            return 0;
        }

        public int AddEntity(Entity entity)
        {
            if (mFirstEntity != null)
            {
                Entity mCurrentEntity = mFirstEntity;
                while (mCurrentEntity.Next != null)
                {
                    mCurrentEntity = mCurrentEntity.Next;
                }
                mCurrentEntity.Next = entity;
                mCurrentEntity.Next.Previous = mCurrentEntity;
            }
            else
            {
                mFirstEntity = entity;
            }
            return 0;
        }

        public Entity DeleteEntity(Entity entity)
        {
            Entity mReturnedEntity = null;
            if (entity == mFirstEntity)
            {
                if (mFirstEntity.Next != null)
                {
                    mFirstEntity = mFirstEntity.Next;
                    mFirstEntity.Previous = null;
                }
                else
                {
                    mFirstEntity = null;
                }
            }
            else
            {
                if (entity.Previous != null)
                {
                    entity.Previous.Next = entity.Next;
                }
                if(entity.Next != null)
                {
                    entity.Next.Previous = entity.Previous;
                }
                mReturnedEntity = entity.Previous;
                entity.Next = null;
                entity.Previous = null;
            }
            return mReturnedEntity;
        }
    }
}
