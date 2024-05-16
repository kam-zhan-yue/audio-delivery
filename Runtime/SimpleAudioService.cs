using Kuroneko.UtilityDelivery;
using UnityEngine;

namespace Kuroneko.AudioDelivery
{
    public class SimpleAudioService : MonoBehaviour, IAudioService
    {
        public SoundDatabase soundDatabase;

        private void Awake()
        {
            ServiceLocator.Instance.Register<IAudioService>(this);
        }

        public void Play(string clipName)
        {
            if (soundDatabase.TryGetSound(clipName, out Sound sound))
            {
                sound.clip.Play();
            }
        }

        public void Stop(string clipName)
        {
            if (soundDatabase.TryGetSound(clipName, out Sound sound))
            {
                sound.clip.Stop();
            }
        }
    }
}