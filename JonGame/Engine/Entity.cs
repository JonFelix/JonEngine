using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Vector2 mPosition;
        Vector2 mSpeed;
        Texture2D mTexture;
        List<Entity> mCollisionList;
        bool mIsCollidable;
        bool mIsClicked;
        int mIsClickedTimer;

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

        public bool IsActive
        {
            get
            {
                return mIsActive;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return mIsInitialized;
            }
        }

        public float X
        {
            get
            {
                return mPosition.X;
            }
            set
            {
                mPosition.X = (float)value;
            }
        }

        public float Y
        {
            get
            {
                return mPosition.Y;
            }
            set
            {
                mPosition.Y = (float)value;
            }
        }

        public float HSpeed
        {
            get
            {
                return mSpeed.X;
            }
            set
            {
                mSpeed.X = (float)value;
            }
        }

        public float VSpeed
        {
            get
            {
                return mSpeed.Y;
            }
            set
            {
                mSpeed.Y = (float)value;
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
        }

        public Entity(int X, int Y, Game1 Game)
        {
            mGame = Game;
            mID = ID;
            mPosition.X = X;
            mPosition.Y = Y;
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
            if (!mIsClicked)
            {
                ApplyCollision();
                
                Click();
                
                mCollisionList = new List<Entity>();
            }
            else
            {
                mIsClickedTimer += gameTime.ElapsedGameTime.Milliseconds;
                if(mIsClickedTimer >= 25)
                {
                    mIsClickedTimer = 0;
                    if (mTexture.Name == "Sprites/Ball")
                    {
                        mTexture = mGame.Content.Load<Texture2D>("Sprites/Ball1");
                    }
                    else if (mTexture.Name == "Sprites/Ball1")
                    {
                        mTexture = mGame.Content.Load<Texture2D>("Sprites/Ball2");
                    }
                    else if (mTexture.Name == "Sprites/Ball2")
                    {
                        mTexture = mGame.Content.Load<Texture2D>("Sprites/Ball3");
                    }
                    else if (mTexture.Name == "Sprites/Ball3")
                    {
                        mTexture = mGame.Content.Load<Texture2D>("Sprites/Ball4");
                    }
                    else if (mTexture.Name == "Sprites/Ball4")
                    {
                        mIsActive = false;
                    }
                }
            }
            mPosition += mSpeed * (float)(gameTime.ElapsedGameTime.Milliseconds * .01);
            return 0;
        }

        public int Draw()
        {
            if (mTexture != null)
            {
                mGame.SpriteBatch.Draw(mTexture, mPosition, Color.White);
            }
            return 0;
        }

        public int ApplyCollision()
        {
            for (int i = 0; i < CollisionList.Length; i++)
            {
                mSpeed.X = mPosition.X - CollisionList[i].X;
                mSpeed.Y = mPosition.Y - CollisionList[i].Y;

                if (Vector2.Distance(mPosition, new Vector2(CollisionList[i].X, CollisionList[i].Y)) <= mTexture.Width)
                {

                }
            }
            if (mPosition.Y + mTexture.Height >= mGame.Height || mPosition.Y <= 0)
            {
                mSpeed.Y *= -1;
                if (mPosition.Y < 0)
                {
                    mPosition.Y = 0;
                }
                else if (mPosition.Y > mGame.Height - mTexture.Height)
                {
                    mPosition.Y = mGame.Height - mTexture.Height;
                }
            }
            if (mPosition.X + mTexture.Width >= mGame.Width || mPosition.X <= 0)
            {
                mSpeed.X *= -1;
                if (mPosition.X < 0)
                {
                    mPosition.X = 0;
                }
                else if (mPosition.X > mGame.Width - mTexture.Width)
                {
                    mPosition.X = mGame.Width - mTexture.Width;
                }
            }
            return 0;
        }
        
        public int Click()
        {
            if(Vector2.Distance(new Vector2(mPosition.X + (mTexture.Width / 2), mPosition.Y + (mTexture.Height / 2)), new Vector2(Mouse.GetState().X, Mouse.GetState().Y)) <= (mTexture.Width / 2))
            {
                mIsClicked = true;
                mIsCollidable = false;
                mGame.Interface.RemoveCounter = 1;
            }
            return 0;
        }
    }
}
