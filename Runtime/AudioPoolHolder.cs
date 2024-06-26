#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Kuroneko.AudioDelivery
{
	/// <summary>
	/// Represents a <see cref="AudioPool"/> of <see cref="AudioUnit"/> instantiated in the scene.
	/// </summary>
	[ExecuteAlways]
	internal class AudioPoolHolder : MonoBehaviour
	{
		private void Awake()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
			}
#endif

			if (Application.isPlaying)
			{
				// Make sure to have this pool not associated with any scene
				DontDestroyOnLoad(gameObject);
			}
		}

		private void OnDestroy()
		{
#if UNITY_EDITOR
			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
		}

#if UNITY_EDITOR
		private void OnPlayModeStateChanged(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.ExitingEditMode)
			{
				DestroyImmediate(gameObject);
			}
		}
#endif
	}
}