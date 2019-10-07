using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Threading;
using System;

namespace Game1
{
    public class SoundTrack
    {
        //List to store the songs
        List<Song> queue = new List<Song>();
        Song CurrentSong;
        int CurrentSongIndex = 0;
        TimeSpan CurrentSongStart;

        public SoundTrack(params Song[] Songs) {
            queue.AddRange(Songs);
        }

        public void Update(GameTime gameTime)
        {
            //If there is no current song playing, play the first song in the queue
            if (CurrentSong == null)
            {
                CurrentSongIndex = 0;
                CurrentSong = queue[CurrentSongIndex];
                MediaPlayer.Play(CurrentSong);
                CurrentSongStart = TimeSpan.FromSeconds(gameTime.TotalGameTime.TotalSeconds);
            }
            else
            {
                if (gameTime.TotalGameTime.TotalSeconds > CurrentSongStart.TotalSeconds + CurrentSong.Duration.TotalSeconds)
                {
                    //If the last song has played restart the queue of songs
                    if (CurrentSongIndex == queue.Count)
                    {
                        CurrentSongIndex = 0;
                    }
                    //Once a song has finished increment the currentsongindex and play the next song
                    CurrentSongIndex++;
                    CurrentSong = queue[CurrentSongIndex];
                    MediaPlayer.Play(CurrentSong);
                    CurrentSongStart = TimeSpan.FromSeconds(gameTime.TotalGameTime.TotalSeconds);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentSong == null)
            {
                return;
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(Game1.GAME.GameDisplay.player.HP, "CurrentSong: " + CurrentSong.Name, new Vector2(300, 40), Color.White);
            spriteBatch.End();
        }
      }
}
