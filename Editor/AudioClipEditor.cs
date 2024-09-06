﻿using UnityEditor;
using UnityEngine;

namespace Kuroneko.AudioDelivery
{
	[CustomEditor(typeof(AudioConfig))]
	internal class AudioClipEditor : Editor
	{
		AudioConfig context;

		private void OnEnable()
		{
			context = target as AudioConfig;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			if (AudioPool.IsPlaying(context))
			{
				if (GUILayout.Button(EditorGUIUtility.IconContent("d_PauseButton@2x")))
				{
					context.Stop();
				}
			}
			else
			{
				if (GUILayout.Button(EditorGUIUtility.IconContent("d_PlayButton@2x")))
				{
					// We are stopping all audio units, only when the game is not running
					if (!Application.isPlaying) AudioPool.StopAll();

					context.Play();
				}
			}

			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

			SerializedProperty isUsingClips = serializedObject.FindProperty("isUsingClips");
			EditorGUILayout.PropertyField(isUsingClips);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("mixerGroup"));

			if (isUsingClips.boolValue) EditorGUILayout.PropertyField(serializedObject.FindProperty("clips"));
			else EditorGUILayout.PropertyField(serializedObject.FindProperty("clip"));


			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("loop"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("volume"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("pitchVariation"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}