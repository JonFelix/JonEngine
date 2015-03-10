using System;
using Microsoft.Xna.Framework;

namespace JonGame.Engine.Components
{
    public class Transformation : Component
    {
        Vector2 mPosition;

        public Vector2 Position
        {
            get
            {
                return mPosition;
            }
            set
            {
                mPosition = value;
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
                mPosition.X = value;
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
                mPosition.Y = value;
            }
        }

        public Transformation(Entity parent, Component previous) : base(parent, previous)
        {

        }
    }
}
