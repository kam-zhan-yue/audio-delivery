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

        public void Play(string clipName, string instanceId = "")
        {
            if (soundDatabase.TryGetSound(clipName, out Sound sound))
                sound.config.Play(instanceId);
            else
                Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
        }

        public void Pause(string clipName, string instanceId = "")
        {
            if (soundDatabase.TryGetSound(clipName, out Sound sound))
                sound.config.Pause(instanceId);
            else
                Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
        }

        public void Resume(string clipName, string instanceId = "")
        {
            if (soundDatabase.TryGetSound(clipName, out Sound sound))
                sound.config.Resume(instanceId);
            else
                Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
        }

        public void Stop(string clipName, string instanceId = "")
        {
            if (soundDatabase.TryGetSound(clipName, out Sound sound))
                sound.config.Stop(instanceId);
            else
                Debug.LogWarning($"AudioDelivery | {clipName} could not be found in {soundDatabase.name}!");
        }
    }
}