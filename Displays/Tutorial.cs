using Game1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace Game1.Displays
{
    public class Tutorial : Display
    {
        //Texture storage
        Texture2D BG;
        Texture2D Exit;

        //KeyStates
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;

        //Button area
        int boxWidth = 175;
        int boxHeight = 100;

        public void LoadContent(ContentManager Content)
        {
          BG = Content.Load<Texture2D>("Assets\\tutorial");
          Exit = Content.Load<Texture2D>("Assets\\exit");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(Exit, new Rectangle(600, 25, boxWidth, boxHeight), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Game1.GAME.IsMouseVisible = true;
            MouseState state = Mouse.GetState();

            //Checks to see if the mosue is within the button boundaries, if so and the mouse is clicked change screens
            //For the exit button
            if (state.Position.X > 600 && state.Position.X < 600 + boxWidth
                && state.Position.Y > 25 && state.Position.Y < 25 + boxHeight)
            {
                if (state.LeftButton == ButtonState.Pressed)
                { Game1.GAME.Exit(); }
            }


            // Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousKeyboardState = currentKeyboardState;
            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();

             if (previousKeyboardState.IsKeyDown(Keys.Escape) && currentKeyboardState.IsKeyUp(Keys.Escape))
            {
                Game1.GAME.CurrentDisplay = Game1.GAME.GameDisplay;
            }

            
        }
    }
}
