using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace BlackDragonEngine.AudioEngine
{
    public sealed class AudioEngine
    {
        private static AudioEngine _instance;
        private readonly List<SoundEffectInstance> _loopingEffects;
        private readonly Dictionary<string, SoundEffect> _sounds;

        private AudioEngine()
        {
            _sounds = new Dictionary<string, SoundEffect>();
            _loopingEffects = new List<SoundEffectInstance>();
        }

        public static AudioEngine GetInstance()
        {
            return _instance ?? (_instance = new AudioEngine());
        }

        public void AddSound(string name, SoundEffect sound)
        {
            _sounds.Add(name, sound);
        }

        public void Play(string name, bool loop = false)
        {
            SoundEffect sound;
            if (!_sounds.TryGetValue(name, out sound)) return;
            sound.Play();
            if (!loop) return;
            _loopingEffects.Add(sound.CreateInstance());
        }

        public void StopAllLoops()
        {
            foreach (var effect in _loopingEffects)
            {
                effect.Stop();
            }
        }

        public void EnsureLoops()
        {
            foreach (var soundEffect in _loopingEffects)
            {
                soundEffect.Play();
            }
        }
    }
}