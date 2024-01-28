using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    public string id = "";

    [SerializeField] private bool _hasSausage = false, _hasOnion = false, _hasChili = false, _hasBeans = false;



    public void CheckIngredient(string typeOfIngredient)
    {
            switch (typeOfIngredient)
            {
                case "Sausage":
                    CheckBools(ref _hasSausage);
                    break;
                case "Onion":
                    CheckBools(ref _hasOnion);
                    break;
                case "Chili":
                    CheckBools(ref _hasChili);
                    break;
                case "Beans":
                    CheckBools(ref _hasBeans);
                    break;
                default:
                    break;
            }

        if (_hasSausage && _hasOnion && _hasChili && _hasBeans)
        {
            Debug.Log(id + "Wins");
            GameManager.Instance.Endgame();
        }
    }

    private void CheckBools(ref bool current)
    {
        if (!current)
        {
            current = true;
        }else SetDefault();  
    }

    private void SetDefault()
    {
        //Show Warning
        _hasSausage = false;
        _hasOnion = false;
        _hasChili = false;
        _hasBeans = false;
    }
}
