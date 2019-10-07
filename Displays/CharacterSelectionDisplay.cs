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
    public class CharacterSelectionDisplay : Display
    {
        Texture2D BG;
        Texture2D P1;
        Texture2D P2;

        int boxWidth = 150;
        int boxHeight = 75;


        public void LoadContent(ContentManager Content)
        {
            BG = Content.Load<Texture2D>("Assets\\characterS");
            P1 = Content.Load<Texture2D>("Assets\\p2logo");
            P2 = Content.Load<Texture2D>("Assets\\p1logo");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(P1, new Rectangle(310, 350, boxWidth, boxHeight), Color.White);
            spriteBatch.Draw(P2, new Rectangle(310, 250, boxWidth, boxHeight), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            //Checks to see if the mosue is within the button boundaries, if so and the mouse is clicked player 1 is selected
            //For the Player 1 button
            if (state.Position.X > 310 && state.Position.X < 310 + boxWidth
                && state.Position.Y > 350 && state.Position.Y < 350 + boxHeight)
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    Game1.GAME.GameDisplay.player1 = true;
                    Game1.GAME.GameDisplay.player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\p2SS");
                    Game1.GAME.GameDisplay.player.playerMoveSpeed = 2;
                    Game1.GAME.GameDisplay.TearsCount = 1;
                    Game1.GAME.Leaderboard.refresh();
                    Game1.GAME.CurrentDisplay = Game1.GAME.GameDisplay; }
            }

            //Checks to see if the mosue is within the button boundaries, if so and the mouse is clicked player 2 is selected
            //For the player 2 button
            if (state.Position.X > 310 && state.Position.X < 310 + boxWidth
                && state.Position.Y > 250 && state.Position.Y < 250 + boxHeight)
            {
                if (state.LeftButton == ButtonState.Pressed)
                { Game1.GAME.GameDisplay.player2 = true;  Game1.GAME.CurrentDisplay = Game1.GAME.GameDisplay; }
            }
            
            if (Game1.GAME.GameDisplay.CurrentWave == -1)
            {
                Game1.GAME.CurrentDisplay = Game1.GAME.LoadingScreenDisplay;
            }

            }
        }

    public class CopyOfCharacterSelectionDisplay : Display
    {
        Texture2D BG;
        Texture2D P1;
        Texture2D P2;

        int boxWidth = 150;
        int boxHeight = 75;


        public void LoadContent(ContentManager Content)
        {
            BG = Content.Load<Texture2D>("Assets\\characterS");
            P1 = Content.Load<Texture2D>("Assets\\p2logo");
            P2 = Content.Load<Texture2D>("Assets\\p1logo");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(P1, new Rectangle(310, 350, boxWidth, boxHeight), Color.White);
            spriteBatch.Draw(P2, new Rectangle(310, 250, boxWidth, boxHeight), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            //Checks to see if the mosue is within the button boundaries, if so and the mouse is clicked player 1 is selected
            //For the Player 1 button
            if (state.Position.X > 310 && state.Position.X < 310 + boxWidth
                && state.Position.Y > 350 && state.Position.Y < 350 + boxHeight)
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    Game1.GAME.GameDisplay.player1 = true;
                    Game1.GAME.GameDisplay.player.PlayerTexture = Game1.GAME.Content.Load<Texture2D>("Assets\\p2SS");
                    Game1.GAME.GameDisplay.player.playerMoveSpeed = 2;
                    Game1.GAME.GameDisplay.TearsCount = 1;
                    Game1.GAME.Leaderboard.refresh();
                    Game1.GAME.CurrentDisplay = Game1.GAME.GameDisplay;
                }
            }

            //Checks to see if the mosue is within the button boundaries, if so and the mouse is clicked player 2 is selected
            //For the player 2 button
            if (state.Position.X > 310 && state.Position.X < 310 + boxWidth
                && state.Position.Y > 250 && state.Position.Y < 250 + boxHeight)
            {
                if (state.LeftButton == ButtonState.Pressed)
                { Game1.GAME.GameDisplay.player2 = true; Game1.GAME.CurrentDisplay = Game1.GAME.GameDisplay; }
            }

            if (Game1.GAME.GameDisplay.CurrentWave == -1)
            {
                Game1.GAME.CurrentDisplay = Game1.GAME.LoadingScreenDisplay;
            }

        }
    }
    }

