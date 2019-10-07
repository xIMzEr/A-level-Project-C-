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
    public class LoadingScreenDisplay : Display
    {
        Texture2D BG;
        Song LSS;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        bool vs1, vs2, vs3, vs4 = false;
      

        public void LoadContent(ContentManager Content)
        {
            BG = Content.Load<Texture2D>("Assets\\LoadingScreen\\VLS1");
            LSS = Content.Load<Song>("Audio\\LoadingScreen\\VolniirLSM");
            MediaPlayer.Play(LSS);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Game1.GAME.IsMouseVisible = false;

            spriteBatch.Begin();
            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.End();

        }

        public void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            if(previousKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyUp(Keys.Enter))
            {
                if (vs3)
                {
                    vs4 = true;    
                }
                else if (vs2)
                {
                    vs3 = true;
                }
                else if(vs1)
                {
                    vs2 = true;
                }
                else
                {
                    vs1 = true;
                }
            }

           if(Game1.GAME.GameDisplay.CurrentWave == -1)
            {
                BG = Game1.GAME.Content.Load<Texture2D>("Assets\\LoadingScreen\\VLS5");
            }
            else if (gameTime.TotalGameTime.TotalSeconds >= 55 || vs4)
            {
                
                Game1.GAME.CurrentDisplay = Game1.GAME.CharacterSelectionDisplay; 
                BG = Game1.GAME.Content.Load<Texture2D>("Assets\\LoadingScreen\\VLS5"); vs4 = true;
            }
            else if (gameTime.TotalGameTime.TotalSeconds >= 40 || vs3)
            {
                BG = Game1.GAME.Content.Load<Texture2D>("Assets\\LoadingScreen\\VLS4"); vs3 = true;
            }
            else if (gameTime.TotalGameTime.TotalSeconds >= 30 || vs2)
            {
                BG = Game1.GAME.Content.Load<Texture2D>("Assets\\LoadingScreen\\VLS3"); vs2 = true;
            }
            else if (gameTime.TotalGameTime.TotalSeconds >= 20 || vs1)
            {
                BG = Game1.GAME.Content.Load<Texture2D>("Assets\\LoadingScreen\\VolniirLS"); vs1 = true;
            }
        }
    }
}
