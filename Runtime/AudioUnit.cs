using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Kuroneko.AudioDelivery
{
	/// <summary>
	/// Handle playing sound on-demand using <see cref="AudioSource"/> component.
	/// </summary>
	internal class AudioUnit : MonoBehaviour
	{
		public string ID { get; set; }
		private AudioSource source;
		private UnityEngine.AudioClip[] clips;
		private AudioMixerGroup mixerGroup;
		private PitchVariation pitchVariation;
		private bool loop;
		private Coroutine returningToPool;

		internal AudioSource Source
		{
			get
			{
				if (source == null) source = GetComponent<AudioSource>();
				return source;
			}

			private set { source = value; }
		}
		internal bool IsPlaying => Source.isPlaying;
		internal AudioConfig AudioConfig { get; private set; }

		internal void Setup(AudioConfig audioConfig)
		{
			AudioConfig = audioConfig;
			clips = audioConfig.IsUsingClips ? clips : new UnityEngine.AudioClip[] { audioConfig.Clip };
			mixerGroup = audioConfig.MixerGroup;
			pitchVariation = audioConfig.PitchVariation;
			loop = audioConfig.Loop;

			Source = GetComponent<AudioSource>();
			Source.playOnAwake = false;
			Source.volume = audioConfig.Volume;
		}

		internal void Play()
		{
			Source.clip = clips[Random.Range(0, clips.Length)];
			Source.outputAudioMixerGroup = mixerGroup;
			Source.pitch = SetPitch(pitchVariation);
			Source.loop = loop;

			gameObject.name += Source.clip.name.ToString();

			gameObject.SetActive(true);
			Source.Play();

			if (!loop)
			{
				returningToPool = StartCoroutine(WaitBeforeReturningToPool());
			}
		}

		internal void Stop()
		{
			Source.Stop();
			AudioConfig = null;
			if (returningToPool != null) StopCoroutine(returningToPool);

			AudioPool.Return(this);
		}

		internal void Pause()
		{
			Source.Pause();
		}

		internal void Resume()
		{
			Source.UnPause();
		}

		private float SetPitch(PitchVariation variation)
		{
			switch (variation)
			{
				case PitchVariation.VerySmall:
					return Random.Range(0.95f, 1.05f);

				case PitchVariation.Small:
					return Random.Range(0.9f, 1.1f);

				case PitchVariation.Medium:
					return Random.Range(0.75f, 1.25f);

				case PitchVariation.Large:
					return Random.Range(0.5f, 1.5f);
			}
			return 1f;
		}

		private IEnumerator WaitBeforeReturningToPool()
		{
			yield return new WaitForSeconds(Source.clip.length);
			AudioPool.Return(this);
		}
	}
}