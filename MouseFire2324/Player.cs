using GameFeatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprites
{
    public class Player : Sprite
    {
        // Give the player a healthbar. Could be done with inheritance
        // in Animated Sprite if all characters have health bars
        public HealthBar healthBarObject;
        public float Speed { get; set; } = 5f;

        public Player(Game g,Texture2D texture, Vector2 userPosition, int framecount) 
            : base(g,texture, userPosition, framecount)
        {
            healthBarObject = new HealthBar(g, 100, userPosition);
        }
        public Player(Game g, Texture2D texture, Vector2 userPosition, int framecount, Vector2 worldBound)
            : base(g, texture, userPosition, framecount)
        {
            healthBarObject = new HealthBar(g, 100, userPosition);
            WorldBound = worldBound;
        }

        // No longer needed as Draw is handled in the base class
        // health bar is drawn as a separate DrawableGameComponent

        //public override void Draw(GameTime gameTime)
        //{
        //    base.Draw(gameTime);
        //}

        // We do need the draw as update method is virtual in the base class
        public override void Update(GameTime gametime)
        {// move the player
            // Up
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Move(new Vector2(0, -Speed));
            // Down
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Move(new Vector2(0, Speed));
            // Right
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Move(new Vector2(-Speed, 0));
            // Left
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Move(new Vector2(Speed, 0));
            healthBarObject.position = new Vector2(this.position.X, this.position.Y);
            base.Update(gametime);
        }
    }
}
