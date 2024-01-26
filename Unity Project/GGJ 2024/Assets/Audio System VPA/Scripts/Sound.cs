using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [Header("File attributes")]
    
    [Space(0.5f)]    
    [Tooltip("Write the name that corresponds to this sound.\n\nIt will be used to search the file by the manager.")]
    public string fileName;
    
    [Tooltip("Attach the audio file for this sound.")]
    public AudioClip clip;
    
    [Space(2.5f)]
    [Header("Audiosource attributes")]
    
    [Space(0.5f)]
    [Tooltip("Check this box if the sound should stay active through different scenes. \n " +
                "When this is not checked the sound will be destroyed when loading a new scene and also will be removed from the managers sound list.")]
    public bool persistentSound;
    
    [Tooltip("If available, attach the scene source that will play this sound. \n " +
                "In the case of not having a defined source, the manager will create a new one with the correspondig settings for this sound.")]
    public AudioSource source;
    
    [Tooltip("Check this box if this sound will be looped during gameplay.")]
    public bool loop;
    
    [Tooltip("This value will assign the sound's volume in it's audiosource.")]
    [Range(0.1f, 1f)]
    public float volume = 1;
    
    [Tooltip("This value will assign the sound's pitch in it's audiosource.")]
    [Range(-3f, 3f)]
    public float pitch = 1;
    
    [Tooltip("This value will assign the sound's stereo panning in it's audiosource.\n\nLowest being left and highest being the right.")]
    [Range(-1f, 1f)]
    public float stereoPan = 0;
    
    [Tooltip("This value will assign the sound's spatial blend in it's audiosource.\n\nThis sets how will the sound be affected by the audiosource's scene position.")]
    [Range(0.1f, 1f)]
    public float spatialBlend = 0;
    
    [Tooltip("Select the mixer group that corresponds to this sound.")]
    public AudioMixerGroup mixerGroup;
}