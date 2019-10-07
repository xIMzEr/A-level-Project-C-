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
    public class MainMenuDisplay : Display
    {
        Texture2D BG;
        Texture2D Play;
        Texture2D PlayWC;
        Texture2D Exit;

        int boxWidth = 175;
        int boxHeight = 100;


        public void LoadContent(ContentManager Content)
        {
            BG = Content.Load<Texture2D>("Assets\\MainMenu\\Volniir");
            Play = Content.Load<Texture2D>("Assets\\play");
            PlayWC = Content.Load<Texture2D>("Assets\\playWC");
            Exit = Content.Load<Texture2D>("Assets\\exit");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(Play, new Rectangle(310, 150, boxWidth, boxHeight), Color.White);
            spriteBatch.Draw(PlayWC, new Rectangle(310, 250, boxWidth, boxHeight), Color.White);
            spriteBatch.Draw(Exit, new Rectangle(310, 350, boxWidth, boxHeight), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            //Checks to see if the mosue is within the button boundaries, if so and the mouse is clicked change screens
            //For the Playbutton
            if(state.Position.X > 310 && state.Position.X < 310 + boxWidth 
                && state.Position.Y > 150 && state.Position.Y < 150 + boxHeight)
            { if(state.LeftButton == ButtonState.Pressed)
            { Game1.GAME.CurrentDisplay = Game1.GAME.CharacterSelectionDisplay; }
            }

            //Checks to see if the mosue is within the button boundaries, if so and the mouse is clicked change screens
            //For the Play with cutscene button
            if (state.Position.X > 310 && state.Position.X < 310 + boxWidth
                && state.Position.Y > 250 && state.Position.Y < 250 + boxHeight)
            {
                if (state.LeftButton == ButtonState.Pressed)
                { Game1.GAME.CurrentDisplay = Game1.GAME.LoadingScreenDisplay; }
            }


            //Checks to see if the mosue is within the button boundaries, if so and the mouse is clicked change screens
            //For the exit button
            if (state.Position.X > 310 && state.Position.X < 310 + boxWidth
                && state.Position.Y > 350 && state.Position.Y < 350 + boxHeight)
            {
                if (state.LeftButton == ButtonState.Pressed)
                { Game1.GAME.Exit(); }
            }
        }
    }
}
