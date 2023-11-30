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
        public float Speed { get; set; } = 5f;

        public Player(Texture2D texture, Vector2 userPosition, int framecount) 
            : base(texture, userPosition, framecount)
        {
        }
        public Player(Texture2D texture, Vector2 userPosition, int framecount, Vector2 worldBound)
            : base(texture, userPosition, framecount)
        {
            WorldBound = worldBound;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

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
            base.Update(gametime);
        }
    }
}
