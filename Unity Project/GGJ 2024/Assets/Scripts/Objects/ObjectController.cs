using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectController : MonoBehaviour
{

    [SerializeField] private bool _isLaunched = false, _isReady=false;
    [SerializeField] private bool _needsCut = true, _needsCook = true;
    [SerializeField]  private float _weight=0.5f;

    private void Awake()
    { 
        CheckReadiness();
    }


    private void CheckReadiness()
    {
        if(!_needsCut && !_needsCook) _isReady = true; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isLaunched)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Invoke Function to stun player
                _isLaunched = false;
                gameObject.SetActive(false);

            }
            else
            {
                _isLaunched = false;
                gameObject.SetActive(false);
            }            
        }
       
        if (collision.gameObject.CompareTag("CuttingZone") && _needsCut)
        {
         //invoke Cut
        }      
        if (collision.gameObject.CompareTag("CookingZone") && _needsCook)
        {
         // invoke Cook
        }   
    }
      
}
