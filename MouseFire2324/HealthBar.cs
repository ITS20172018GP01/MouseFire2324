using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFeatures
{
    public class HealthBar
    {
        public int health;
        private Texture2D _txHealthBar; // hold the texture
        Rectangle _healthRect;   // display the Health bar size
        Vector2 position; // Position on the screen

        public HealthBar(Game g,int health, Vector2 position)
        {
            this.health = health;
            this.position = position;
            _txHealthBar = new Texture2D(g.GraphicsDevice, 1, 1);
            _txHealthBar.SetData(new[] { Color.White });

        }

        public Rectangle HealthRect
        {
            get
            {
                return new Rectangle((int)position.X , (int)position.Y , health, 10);
            }

            set
            {
                _healthRect = value;
            }
        }
    public void Draw(SpriteBatch spriteBatch)
        {
            if (health > 60)
                spriteBatch.Draw(_txHealthBar, HealthRect, Color.Green);
            else if (health > 30 && health <= 60)
                spriteBatch.Draw(_txHealthBar, HealthRect, Color.Orange);
            else if (health > 0 && health <= 30)
                spriteBatch.Draw(_txHealthBar, HealthRect, Color.Red);

        }
    }
}
