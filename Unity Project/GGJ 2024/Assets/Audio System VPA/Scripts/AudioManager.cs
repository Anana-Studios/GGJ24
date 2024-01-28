using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<Sound> sfx, music, dialogues;
    
    public Dictionary<string, Sound> sfxDictionary = new Dictionary<string, Sound>(), musicDictionary = new Dictionary<string, Sound>(), dialogueDictionary = new Dictionary<string, Sound>();

    private GameObject _npSoundsSourceParent;

    private void Awake()
    {
        if (instance != null)
        {
            SendSoundsToAMInstance();
            Destroy(gameObject);
            return;
        }
        else { instance = this; }

        DontDestroyOnLoad(gameObject);

        SortSoundLists();
    }

    #region Singleton Management
    private void SendSoundsToAMInstance()
    {
        instance.CleanSoundDictionaries();

        instance.sfx.AddRange(sfx);
        instance.music.AddRange(music);
        instance.dialogues.AddRange(dialogues);
        
        instance.SortSoundLists();
    }
    #endregion

    #region List Arrangement
    public void SortSoundLists()
    {
        _npSoundsSourceParent = new GameObject();
        _npSoundsSourceParent.name = "Non persistent sound audiosources";

        if (sfx != null) { SortAudio(sfx, sfxDictionary); }
        if (music != null) { SortAudio(music, musicDictionary); }
        if (dialogues != null) { SortAudio(dialogues, dialogueDictionary); }
    }
    public void CleanSoundDictionaries()
    {
        if (sfx != null) { CleanDictionary(sfx, sfxDictionary); }
        if (music != null) { CleanDictionary(music, musicDictionary); }
        if (dialogues != null) { CleanDictionary(dialogues, dialogueDictionary); }
    }
    private void CleanDictionary(List<Sound> listToClean, Dictionary<string, Sound> dictionaryToClean)
    {
        List<Sound> soundsToRemove = new List<Sound>();
        foreach (Sound soundToCheck in dictionaryToClean.Values) 
        {
            if (!soundToCheck.persistentSound)
            {
                soundsToRemove.Add(soundToCheck);
            }
        }
        foreach (Sound soundToErase in soundsToRemove)
        {
                dictionaryToClean.Remove(soundToErase.fileName);
                listToClean.Remove(soundToErase);
        }
    }
    private void SortAudio(List<Sound> listToSort, Dictionary<string, Sound> soundDictionary)
    {
        foreach (Sound s in listToSort)
        {
            if (!soundDictionary.ContainsKey(s.fileName))
            {
                soundDictionary.Add(s.fileName, s);
            }

            if (s.source != null) { s.source.transform.parent = s.persistentSound ? transform : null; }
            else { s.source = CreateAudiosource(s.fileName, s.persistentSound); }

            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.panStereo = s.stereoPan;
            s.source.spatialBlend = s.spatialBlend;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    private AudioSource CreateAudiosource(string soundName, bool persistentSnd)
    {
        GameObject newAudiosource = new GameObject();
        newAudiosource.name = $"{soundName} Audiosource";
        newAudiosource.AddComponent<AudioSource>();
        newAudiosource.GetComponent<AudioSource>().playOnAwake = false;
        newAudiosource.transform.parent = persistentSnd ? transform : _npSoundsSourceParent.transform;
        return newAudiosource.GetComponent<AudioSource>();
    }
#endregion
    
    public AudioSource SendSoundSource(int listIndex, string fileToSearch)
    {
        Sound soundFound = null;

        try
        {
            switch (listIndex)
            {
                case 0:
                    soundFound = FindSoundSource(sfxDictionary, fileToSearch);
                    break;
                case 1:
                    soundFound = FindSoundSource(musicDictionary, fileToSearch);
                    break;
                case 2:
                    soundFound = FindSoundSource(dialogueDictionary, fileToSearch);
                    break;
            }
            return soundFound.source;
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    private Sound FindSoundSource(Dictionary<string, Sound> dictionaryToSearch, string soundName)
    {
        try
        {
            return dictionaryToSearch[soundName];
        }
        catch (System.Exception)
        {
            return null;
        }
    }
}
