using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPlay = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else { Instance = this; }

        DontDestroyOnLoad(gameObject);
    }


    public void ChangePlayState()
    {
        isPlay =!isPlay;
    }

    [ContextMenu("Pause")]
    public void Pause()
    {  
       Time.timeScale = 0.0f;
        ChangePlayState();
    }
    [ContextMenu("Resume")]
    public void Resume()
    {
        Time.timeScale = 1.0f;
        ChangePlayState();
    }

    public void Endgame(){}
}
