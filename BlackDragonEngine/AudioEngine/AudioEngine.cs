using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace BlackDragonEngine.AudioEngine
{
    public sealed class AudioEngine
    {
        private static AudioEngine _instance;
        private readonly Dictionary<string, SoundEffectInstance> _music;
        private readonly Dictionary<string, SoundEffectInstance> _loopingEffects;
        private readonly Dictionary<string, SoundEffect> _sounds;

        private AudioEngine()
        {
            _sounds = new Dictionary<string, SoundEffect>();
            _loopingEffects = new Dictionary<string, SoundEffectInstance>();
            _music = new Dictionary<string, SoundEffectInstance>();
        }

        public static AudioEngine GetInstance()
        {
            return _instance ?? (_instance = new AudioEngine());
        }

        public void AddSound(string name, SoundEffect sound)
        {
            _sounds.Add(name, sound);
        }

        public void AddMusic(string name, SoundEffect music)
        {
            _music.Add(name, music.CreateInstance());
        }

        public void PlaySound(string name, bool loop = false)
        {
            SoundEffect sound;
            if (!_sounds.TryGetValue(name, out sound)) return;
            sound.Play();
            if (!loop) return;
            try
            {
                _loopingEffects.Add(name, sound.CreateInstance());
            }
            catch
            {
            }
        }

        public void PlayMusic(string name, bool loop = false)
        {
            SoundEffectInstance music;
            if (!_music.TryGetValue(name, out music)) return;
            music.Play();
            if (!loop) return;
            try
            {
                _loopingEffects.Add(name, music);
            }
            catch
            {
            }
            
        }

        public void StopAllLoops()
        {
            foreach (var effect in _loopingEffects)
            {
                effect.Value.Stop();
            }
        }

        public void StopLoop(string name)
        {
            foreach(var effect in _loopingEffects)
            {
                if(effect.Key == name)
                {
                    effect.Value.Stop();
                }
            }
        }

        public void EnsureLoops()
        {
            foreach (var soundEffect in _loopingEffects)
            {
                soundEffect.Value.Play();
            }
        }
    }
}