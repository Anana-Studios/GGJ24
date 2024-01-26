using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRequester : MonoBehaviour
{
    private enum SoundType
    {
        Sfx, Music, Dialogue
    }
    [SerializeField]
    [Tooltip("Choose the list that contains the sound you want to play in the Audiomanager.")]
    private SoundType listToSearch;
    [SerializeField]
    [Tooltip("Enter the sound's file name. Be careful to write the name correctly as it won't find it if there is a typo.")]
    private string soundFile;
    [SerializeField]
    [Tooltip("Check this box if you want to play a sound as soon as this object is loaded. \n " +
                "\ti.e. \n" +
                "\tA music track for the level. \n" +
                "\tOr an ambiance sfx.")]
    private bool playOnAwake;
    [SerializeField]
    private AudioSource _sourceToUse;
    private Dictionary<string, int> _soundListDictionary = new Dictionary<string, int>();

    private void Awake()
    {
        _soundListDictionary.Add("Sfx", 0);
        _soundListDictionary.Add("Music", 1);
        _soundListDictionary.Add("Dialogue", 2);
    }
    private void Start()
    {
        SetAudiosource();

        if (playOnAwake) { PlayAudio(); }
    }
    private void SetAudiosource()
    {
        try
        {
            _sourceToUse = AudioManager.aMInstance.SendSoundSource(_soundListDictionary[listToSearch.ToString()], soundFile);
        }
        catch 
        {
            Debug.LogError("Couldn\'t find source.");
        }
    }
    public void PlayAudio()
    {
        _sourceToUse.Play();
    }
    public void StopAudio()
    {
        _sourceToUse.Stop();
    }
}
