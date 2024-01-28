using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public Transform playerHolder;
    public Color[] playerColor;

    private Vector3[] playerHoldPosition = new Vector3[4];
    private List<GameObject> _playerGo = new List<GameObject>();
    private PlayerInputManager _plyrInputManager;

    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }

        SetHolderPositions();

        _plyrInputManager = GetComponent<PlayerInputManager>();
    }
    private void SetHolderPositions()
    {
        for (int i = 0; i < playerHoldPosition.Length; i++)
        {
            playerHoldPosition[i] = playerHolder.GetChild(i).position;

        }
    }

    public void OnPlayerJoin(PlayerInput player)
    {
        _playerGo.Add(player.gameObject);
        player.GetComponent<PlayerController>().SetPointerColour(playerColor[_playerGo.IndexOf(player.gameObject)]);
        player.GetComponent<PlayerController>().TeleportPlayer(playerHoldPosition[_playerGo.IndexOf(player.gameObject)]);
    }
}
