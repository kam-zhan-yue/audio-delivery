using UnityEngine;
using UnityEngine.Audio;

namespace Kuroneko.AudioDelivery
{
	/// <summary>
	/// Holds settings for playing sound using <see cref="AudioUnit"/>.
	/// </summary>
	[CreateAssetMenu(fileName = "Clip", menuName = "ScriptableObjects/AudioDelivery/Config", order = 1)]
	public class AudioConfig : ScriptableObject
	{
		[SerializeField] private bool isUsingClips;
		[SerializeField] private AudioClip clip;
		[SerializeField] private AudioClip[] clips;
		[SerializeField] private AudioMixerGroup mixerGroup;
		[SerializeField, Range(0f, 1f)] private float volume = 1f;
		[SerializeField] private bool loop;
		[SerializeField] private PitchVariation pitchVariation;

		internal bool IsUsingClips => isUsingClips;
		internal AudioClip Clip => clip;
		internal AudioClip[] Clips => clips;
		internal AudioMixerGroup MixerGroup => mixerGroup;
		internal bool Loop => loop;
		internal PitchVariation PitchVariation => pitchVariation;
		internal float Volume => volume;

		/// <summary>
		/// Indicates whether or not this <see cref="AudioConfig"/> is playing any <see cref="AudioUnit"/>.
		/// </summary>
		public bool IsPlaying => AudioPool.IsPlaying(this);

		/// <summary>
		/// Plays a sound using <see cref="AudioUnit"/>.
		/// </summary>
		public void Play()
		{
			// Making sure that clip references are not null
			bool issueDetected = false;
			if (isUsingClips)
			{
				foreach (UnityEngine.AudioClip c in clips)
				{
					if (c == null) issueDetected = true;
				}
			}
			else
			{
				if (clip == null) issueDetected = true;
			}

			if (issueDetected)
			{
				Debug.LogError("AudioClip: Null clip reference detected!");
				return;
			}

			AudioUnit audioUnit = AudioPool.Get();
			audioUnit.Setup(this);
			audioUnit.Play();
		}

		/// <summary>
		/// Stops all <see cref="AudioUnit"/> currently playing this <see cref="AudioConfig"/>.
		/// </summary>
		public void Stop()
		{
			AudioPool.Stop(this);
		}
	}
}