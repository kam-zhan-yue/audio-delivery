using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kuroneko.AudioDelivery
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AudioDelivery/SoundDatabase")]
    public class SoundDatabase : ScriptableObject
    {
        [TableList]
        public Sound[] sounds = Array.Empty<Sound>();

        public bool TryGetSound(string id, out Sound sound)
        {
            for (int i = 0; i < sounds.Length; ++i)
            {
                if (sounds[i].id == id)
                {
                    sound = sounds[i];
                    return true;
                }
            }

            sound = null;
            return false;
        }
    }
}
