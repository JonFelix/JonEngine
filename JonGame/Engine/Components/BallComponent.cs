using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace JonGame.Engine.Components
{
    class BallComponent : Component
    {
        bool mIsClicked;
        int mIsClickedTimer;
        Vector2 mSpeed;

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

        public BallComponent(Entity parent) : base(parent)
        {

        }

        public override int Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!mIsClicked)
            {
                ApplyCollision();

                Click();

                Parent.ResetCollisionList();
            }
            else
            {
                mIsClickedTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (mIsClickedTimer >= 25)
                {
                    mIsClickedTimer = 0;
                    if (Parent.Texture.Name == "Sprites/Ball")
                    {
                        Parent.Texture = Parent.Game.Content.Load<Texture2D>("Sprites/Ball1");
                    }
                    else if (Parent.Texture.Name == "Sprites/Ball1")
                    {
                        Parent.Texture = Parent.Game.Content.Load<Texture2D>("Sprites/Ball2");
                    }
                    else if (Parent.Texture.Name == "Sprites/Ball2")
                    {
                        Parent.Texture = Parent.Game.Content.Load<Texture2D>("Sprites/Ball3");
                    }
                    else if (Parent.Texture.Name == "Sprites/Ball3")
                    {
                        Parent.Texture = Parent.Game.Content.Load<Texture2D>("Sprites/Ball4");
                    }
                    else if (Parent.Texture.Name == "Sprites/Ball4")
                    {
                        Parent.IsActive = false;
                    }
                }
            }
            Parent.Transformation.Position += mSpeed * (float)(gameTime.ElapsedGameTime.Milliseconds * .01);
            return base.Update(gameTime);
        }

        public int ApplyCollision()
        {
            for (int i = 0; i < Parent.CollisionList.Length; i++)
            {
                mSpeed.X = Parent.Transformation.Position.X - Parent.CollisionList[i].Transformation.Position.X;
                mSpeed.Y = Parent.Transformation.Position.Y - Parent.CollisionList[i].Transformation.Position.Y;

                if (Vector2.Distance(Parent.Transformation.Position, new Vector2(Parent.CollisionList[i].Transformation.Position.X, Parent.CollisionList[i].Transformation.Position.Y)) <= Parent.Texture.Width)
                {

                }
            }
            if (Parent.Transformation.Position.Y + Parent.Texture.Height >= Parent.Game.Height || Parent.Transformation.Position.Y <= 0)
            {
                mSpeed.Y *= -1;
                if (Parent.Transformation.Position.Y < 0)
                {
                    Parent.Transformation.Y = 0;
                }
                else if (Parent.Transformation.Position.Y > Parent.Game.Height - Parent.Texture.Height)
                {
                    Parent.Transformation.Y = Parent.Game.Height - Parent.Texture.Height;
                }
            }
            if (Parent.Transformation.Position.X + Parent.Texture.Width >= Parent.Game.Width || Parent.Transformation.Position.X <= 0)
            {
                mSpeed.X *= -1;
                if (Parent.Transformation.Position.X < 0)
                {
                    Parent.Transformation.X = 0;
                }
                else if (Parent.Transformation.Position.X > Parent.Game.Width - Parent.Texture.Width)
                {
                    Parent.Transformation.X = Parent.Game.Width - Parent.Texture.Width;
                }
            }
            return 0;
        }

        public int Click()
        {
            if (Vector2.Distance(new Vector2(Parent.Transformation.Position.X + (Parent.Texture.Width / 2), Parent.Transformation.Position.Y + (Parent.Texture.Height / 2)), new Vector2(Mouse.GetState().X, Mouse.GetState().Y)) <= (Parent.Texture.Width / 2))
            {
                mIsClicked = true;
                Parent.IsCollidable = false;
                Parent.Game.Interface.RemoveCounter = 1;
            }
            return 0;
        }

    }
}
