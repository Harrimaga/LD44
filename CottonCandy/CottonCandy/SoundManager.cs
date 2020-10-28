using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Microsoft.Xna.Framework.Audio;

namespace CottonCandy
{
    class SoundManager
    {

        private static SoundEffectInstance backgroundMusic = null;
        private static float MusicVolume = 1, SFXVolume = 1;

        /// <summary>
        /// Starts background music, if there is already music playing, it will be stopped.
        /// Make sure the file is added to the content manager and set to build not copy.
        /// </summary>
        /// <param name="name">The name off the file. Don't forget to add Sounds/ infront of the file name and NO extension after the name.</param>
        public static void StartBackgroundMusic(string name)
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
                backgroundMusic.Dispose();
            }
            try
            {
                SoundEffect music = Globals.contentmanager.Load<SoundEffect>(name);
                backgroundMusic = music.CreateInstance();
                backgroundMusic.IsLooped = true;
                backgroundMusic.Volume = MusicVolume;
                backgroundMusic.Play();
            }
            catch (Exception)
            {
                FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at SoundManager.cs->StartBackgroundMusic()] Probably the file given to the startbackground method could not be found.\r\nEnsure you put Sounds/ before the filename, and that you don't put the filetype behind it.\r\nIts also important that you put in the content manager the the action not on copy but build for this file.", "log.txt");
            }
        }

        /// <summary>
        /// Player a sound effect with the given name. Multiple sound effect can be played at once.
        /// Make sure the file is added to the content manager and set to build not copy.
        /// </summary>
        /// <param name="name">The name off the file. Don't forget to add Sounds/ infront of the file name and NO extension after the name.</param>
        public static void PlaySFX(string name)
        {
            try
            {
                SoundEffect s = Globals.contentmanager.Load<SoundEffect>(name);
                s.Play(SFXVolume, 0, 0);
                s.Name = name;
            }
            catch (Exception)
            {
                FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at SoundManager.cs->StartBackgroundMusic()] Probably the file given to the PlaySFX method could not be found.\r\nEnsure you put Sounds / before the filename, and that you don't put the filetype behind it.\r\nIts also important that you put in the content manager the the action not on copy but build for this file.", "log.txt");
            }
        }

        /// <summary>
        /// Sets the music volume to the given amount
        /// </summary>
        /// <param name="musicVolume">Volume as a float between 0.0f and 1.0f</param>
        public static void SetMusicVolume(float musicVolume)
        {
            MusicVolume = musicVolume;
            backgroundMusic.Volume = MusicVolume;
        }

        /// <summary>
        /// Sets the SFX volume to the given amount
        /// </summary>
        /// <param name="musicVolume">Volume as a float between 0.0f and 1.0f</param>
        public static void SetSFXVolume(float sfxvolume)
        {
            SFXVolume = sfxvolume;
        }

        /// <summary>
        /// Pauses the background music
        /// </summary>
        public static void PauseBackgroundMusic()
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Pause();
            }
        }

        /// <summary>
        /// Resumes the background music
        /// </summary>
        public static void ResumeBackgroundMusic()
        {
            backgroundMusic.Resume();
        }

    }
}
