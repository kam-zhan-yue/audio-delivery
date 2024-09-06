using System;
using Kuroneko.AudioDelivery;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[Serializable]
public class Sound
{
    [TableColumnWidth(100, Resizable = false)]
    public string id;
    [InlineEditor]
    public AudioConfig config;
}