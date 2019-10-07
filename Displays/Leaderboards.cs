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
using System.IO;

namespace Game1.Displays
{
    public class Leaderboards : Display
    {
        //Stores background texture
        Texture2D BG;
        
        //BinaryReader
        BinaryReader BnRd;

        //KeyStates
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;

        //Leaderboard states
        public int wavesLasted;
        public int tearsLanded;
        public int enemiesKilled;
        public int itemsPicked;
        bool wonGame;
        public int wavesLasted2;
        public int tearsLanded2;
        public int enemiesKilled2;
        public int itemsPicked2;
        bool wonGame2;

        public void LoadContent(ContentManager Content)
        {
            BG = Content.Load<Texture2D>("Assets\\stats");
        }

        public void refresh()
        {
            //Reads the file and assigns the stored values to local variables
            BnRd = new BinaryReader(File.Open("LeaderBoards", FileMode.Open));
            wavesLasted = BnRd.ReadInt32();
            tearsLanded = BnRd.ReadInt32();
            enemiesKilled = BnRd.ReadInt32();
            itemsPicked = BnRd.ReadInt32();
            wonGame = BnRd.ReadBoolean();
            BnRd.Close();

            //Reads the file and assigns the stored values to local variables as a new copy
            BnRd = new BinaryReader(File.Open("LeaderBoards", FileMode.Open));
            wavesLasted2 = BnRd.ReadInt32();
            tearsLanded2 = BnRd.ReadInt32();
            enemiesKilled2 = BnRd.ReadInt32();
            itemsPicked2 = BnRd.ReadInt32();
            wonGame2 = BnRd.ReadBoolean();
            BnRd.Close();

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            //Displays the statistics on screen
            spriteBatch.Begin();
            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.DrawString(Game1.GAME.GameDisplay.player.HP, "Waves Lasted: " + wavesLasted2, new Vector2(300, 150), Color.White);
            spriteBatch.DrawString(Game1.GAME.GameDisplay.player.HP, "Tears Landed: " + tearsLanded2, new Vector2(300, 175), Color.White);
            spriteBatch.DrawString(Game1.GAME.GameDisplay.player.HP, "Enemies Killed: " + enemiesKilled2, new Vector2(300, 200), Color.White);
            spriteBatch.DrawString(Game1.GAME.GameDisplay.player.HP, "Items Picked Up: " + itemsPicked2, new Vector2(300, 225), Color.White);
            spriteBatch.DrawString(Game1.GAME.GameDisplay.player.HP, "Won the game: " + wonGame2, new Vector2(300, 250), Color.White);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            // Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousKeyboardState = currentKeyboardState;
            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();

            if (previousKeyboardState.IsKeyDown(Keys.Escape) && currentKeyboardState.IsKeyUp(Keys.Escape))
            {
                Game1.GAME.CurrentDisplay = Game1.GAME.GameDisplay;
                BnRd.Close();
            }

        }
    }
}
