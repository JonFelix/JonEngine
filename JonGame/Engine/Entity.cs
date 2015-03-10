using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JonGame.Engine
{
    public class Entity
    {
        Entity mNext = null;
        Entity mPrevious = null;
        int mID;
        Game1 mGame;
        bool mIsActive = false;
        bool mIsInitialized = false;
        Texture2D mTexture;
        List<Entity> mCollisionList;
        bool mIsCollidable;
        
        Component mFirstComponent;
        Components.Transformation mTransformation;

        public Entity Next
        {
            get
            {
                return mNext;
            }
            set
            {
                mNext = value;
            }
        }

        public Entity Previous
        {
            get
            {
                return mPrevious;
            }
            set
            {
                mPrevious = value;
            }
        }

        public int ID
        {
            get
            {
                return mID;
            }
        }

        public Game1 Game
        {
            get
            {
                return mGame;
            }
        }

        public bool IsActive
        {
            get
            {
                return mIsActive;
            }
            set
            {
                mIsActive = value;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return mIsInitialized;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return mTexture;
            }
            set
            {
                mTexture = value;
            }
        }

        public Entity[] CollisionList
        {
            get
            {
                return mCollisionList.ToArray();
            }
            set
            {
                mCollisionList.AddRange(value);
            }
        }

        public bool IsCollidable
        {
            get
            {
                return mIsCollidable;
            }
            set
            {
                mIsCollidable = value;
            }
        }

        public Component FirstComponent
        {
            get
            {
                return mFirstComponent;
            }
        }

        public Components.Transformation Transformation
        {
            get
            {
                return mTransformation;
            }
        }

        public Entity(int X, int Y, Game1 game)
        {
            mGame = game;
            mID = ID;
            mFirstComponent = new Components.Transformation(this, null);
            mTransformation = (Components.Transformation)mFirstComponent;
            Transformation.X = X;
            Transformation.Y = Y;
            mCollisionList = new System.Collections.Generic.List<Entity>();
        }

        public int Init(int ID)
        {
            mID = ID;
            mIsActive = true;
            mIsInitialized = true;
            mIsCollidable = true;
            return 0;
        }

        public int Update(GameTime gameTime)
        {
            Component mCurrentComponent = mFirstComponent;
            int mError = 0;
            while (mCurrentComponent != null)
            {
                if (!mCurrentComponent.IsActive)
                {
                    if (!mCurrentComponent.IsInitialized)
                    {
                        mError += mCurrentComponent.Init();
                    }
                    else
                    {
                        mCurrentComponent = DeleteComponent(mCurrentComponent);
                    }
                }
                if (mCurrentComponent != null)
                {
                    mError += mCurrentComponent.Update(gameTime);
                    mCurrentComponent = mCurrentComponent.Next;
                }
            }
            return 0;
        }

        public int Draw()
        {
            if (mTexture != null)
            {
                mGame.SpriteBatch.Draw(mTexture, Transformation.Position, Color.White);
            }
            return 0;
        }

        public Component DeleteComponent(Component component)
        {
            Component mReturnedComponent = null;
            if (component == mFirstComponent)
            {
                if (mFirstComponent.Next != null)
                {
                    mFirstComponent = mFirstComponent.Next;
                    mFirstComponent.Previous = null;
                }
                else
                {
                    mFirstComponent = null;
                }
            }
            else
            {
                if (component.Previous != null)
                {
                    component.Previous.Next = component.Next;
                }
                if (component.Next != null)
                {
                    component.Next.Previous = component.Previous;
                }
                mReturnedComponent = component.Previous;
                component.Next = null;
                component.Previous = null;
            }
            return mReturnedComponent;
        }

        public Component GetComponent<t>()
        {
            Component mCurrentComponent = mFirstComponent;
            while (mCurrentComponent != null)
            {
                if (mCurrentComponent is t)
                {
                    return mCurrentComponent;
                }
                mCurrentComponent = mCurrentComponent.Next;
            }
            return null;
        }

        public int AddComponent(Component component)
        {
            Component mCurrentEntity = mFirstComponent;
            while (mCurrentEntity.Next != null)
            {
                mCurrentEntity = mCurrentEntity.Next;
            }
            mCurrentEntity.Next = component;
            return 0;
        }

        public Component LastComponent()
        {
            Component mCurrentComponent = mFirstComponent;
            if(mCurrentComponent == null)
            {
                return null;
            }
            while(mCurrentComponent.Next != null)
            {
                mCurrentComponent = mCurrentComponent.Next;
            }
            return mCurrentComponent;
        }

        public int ResetCollisionList()
        {
            mCollisionList = new List<Entity>();
            return 0;
        }
    }
}
