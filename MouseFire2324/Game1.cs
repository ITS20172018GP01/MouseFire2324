using GameFeatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprites;

namespace MouseFire2324
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite ProjectileSprite;
        Player playerSprite;
        Vector2 _target;
        Vector2 _startPos;
        MouseState previous, current;
        HealthBar healthBarObject;
        SoundEffectInstance kissing,smacker;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D tx = Content.Load<Texture2D>("projectile");
            _startPos = GraphicsDevice.Viewport.Bounds.Center.ToVector2() - tx.Bounds.Center.ToVector2();

            playerSprite = new Player(Content.Load<Texture2D>("lips"), 
                _startPos,1, 
                GraphicsDevice.Viewport.Bounds.Size.ToVector2());

            ProjectileSprite = new Sprite(tx, _startPos, 1);
            playerSprite.Visible = true;
            healthBarObject = new HealthBar(this, 100, _startPos);

            // Make Sound Effect
            SoundEffect kiss = Content.Load<SoundEffect>("kiss");
            kissing = kiss.CreateInstance();
            smacker = Content.Load<SoundEffect>("smacker").CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Sample the mouse state one click at a time
            
            current = Mouse.GetState();
            // if the Mouse is in current Viewport
            if (GraphicsDevice.Viewport.Bounds.Contains(current.Position))
            {
                // if the mouse left button has been pressed and is now released and the Projectile sprite is invisible
                if (previous.LeftButton == ButtonState.Pressed &&
                 current.LeftButton == ButtonState.Released &&
                  !ProjectileSprite.Visible)
                {
                    // set the target for the movement to the current mouse position 
                    // Change the state of the Projectile and
                    // Decrease the healthbar value and play the sound effect
                    _target = current.Position.ToVector2();
                    ProjectileSprite.Visible = true;
                    healthBarObject.health -= 10;
                    if (kissing.State != SoundState.Playing)
                        kissing.Play();
                }
                else
                { // If We are moving
                  // Check for Target acquired
                    if (Vector2.Distance(ProjectileSprite.position, _target) < 10
                        && ProjectileSprite.Visible)
                    { // change the state of teh Projectile and move it back to it's initial position
                        ProjectileSprite.Visible = false;
                        ProjectileSprite.position = _startPos;
                    }
                    // Otherwise if we are moving then keep moving towards the target
                    else if (ProjectileSprite.Visible)
                        ProjectileSprite.position = Vector2.Lerp(ProjectileSprite.position, _target, 0.1f);
                }
            }
            // Update the projectile animation
            ProjectileSprite.Update(gameTime);
            previous = current;
            // Update the player for Key presses and then for animation
            playerSprite.Update(gameTime);
            // Check for collision between Projectile and player

            if(playerSprite.collisionDetect(ProjectileSprite) && ProjectileSprite.Visible)
            {
                if (smacker.State != SoundState.Playing)
                    smacker.Play();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            ProjectileSprite.Draw(spriteBatch);
            healthBarObject.Draw(spriteBatch);
            playerSprite.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
