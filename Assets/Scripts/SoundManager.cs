using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance { get; private set; }

    //[SerializeField] private AudioMixerGroup audioMixerGroup;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlaySound(Transform transform, AudioClip audioClip, float volume, float pitchOffset = 0.0f)
    {
        var obj = new GameObject();
        obj.transform.position = transform.position;
        var audio = obj.AddComponent<AudioSource>();
        audio.clip = audioClip;
        audio.volume = volume;
        audio.pitch = 1.0f + Random.Range(-pitchOffset, pitchOffset);
        //audio.outputAudioMixerGroup = audioMixerGroup;
        //audio.spatialBlend = 1.0f;
        audio.Play();
        Destroy(obj, audioClip.length);
    }
}
