using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace BlackDragonEngine.Managers
{
    public static class AudioManager
    {
        #region Declarations

        private static AudioEngine audioEngine;
        private static SoundBank soundBank;

        private static WaveBank sfxBank;
        private static WaveBank bgmBank;

        private static AudioCategory sfxCategory;
        private static AudioCategory bgmCategory;

        private static float bgmVolume = 1.0f;
        private static float sfxVolume = 1.0f;

        public static Cue CurrentBgmCue;
        private static Cue currentSfxCue;

        #endregion

        #region Initialization

        public static void Initialize(Dictionary<string, string> parameters)
        {
            try
            {
                audioEngine = new AudioEngine(parameters["settingsFile"]);
                bgmBank = new WaveBank(audioEngine, parameters["bgmBank"]);
                sfxBank = new WaveBank(audioEngine, parameters["sfxBank"]);
                soundBank = new SoundBank(audioEngine, parameters["soundBank"]);
            }
            catch (NoAudioHardwareException)
            {
                audioEngine = null;
                bgmBank = null;
                sfxBank = null;
                soundBank = null;
            }
            sfxCategory = audioEngine.GetCategory(parameters["sfxCategory"]);
            bgmCategory = audioEngine.GetCategory(parameters["bgmCategory"]);

            sfxCategory.SetVolume(sfxVolume);
            bgmCategory.SetVolume(bgmVolume);
        }

        #endregion

        #region Cue Methods

        public static Cue GetCue(string cueName)
        {
            if (soundBank == null)
                return null;
            return soundBank.GetCue(cueName);
        }

        public static void PlayCue(string cueName)
        {
            if (soundBank != null)
                soundBank.PlayCue(cueName);
        }

        public static void PlaySfx(string cueName)
        {
            if (audioEngine == null)
                return;
            if (currentSfxCue != null)
                currentSfxCue.Stop(AudioStopOptions.AsAuthored);

            currentSfxCue = GetCue(cueName);

            if (currentSfxCue != null)
                currentSfxCue.Play();
        }

        public static void PlayBgm(string cueName)
        {
            if (audioEngine == null)
                return;
            if (CurrentBgmCue != null)
                CurrentBgmCue.Stop(AudioStopOptions.AsAuthored);

            CurrentBgmCue = GetCue(cueName);

            if (CurrentBgmCue != null)
                CurrentBgmCue.Play();
        }

        #endregion

        #region Update and Volume Control

        public static void Update()
        {
            if (audioEngine != null)
                audioEngine.Update();
        }

        public static void SetBgmVolume(float volume)
        {
            bgmVolume = MathHelper.Clamp(volume, .1f, 10f);
        }

        public static void SetSfxVolume(float volume)
        {
            sfxVolume = MathHelper.Clamp(volume, .1f, 10f);
        }

        #endregion
    }
}