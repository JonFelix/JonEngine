using System;
using System.Collections;
using Microsoft.Xna.Framework;

namespace JonGame.Engine
{
    public class Component
    {
        Component mNext;
        Component mPrevious;
        Entity mParent;
        bool mIsActive = false;
        bool mIsInitialized = false;

        public Component Next
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

        public Component Previous
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

        public Entity Parent
        {
            get
            {
                return mParent;
            }
        }

        public Component(Entity parent, Component previous = null, Component next = null)
        {
            mParent = parent;
            if(previous == null)
            {
                mPrevious = Parent.LastComponent();
            }
            else
            {
                mPrevious = previous;
            }
            
            mNext = next;
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

        public virtual int Init()
        {
            mIsInitialized = true;
            mIsActive = true;
            return 0;
        }

        public virtual int Update(GameTime gameTime)
        {

            return 0;
        }
    }
}
