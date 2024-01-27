using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectController : MonoBehaviour
{

    [Header("Meshes")]
    private MeshFilter _mf;
    [SerializeField] private Mesh _firstMesh, _newMesh;
    [Header("Values")]
    [SerializeField] private bool _isLaunched = false, _isReady=false;
    [SerializeField] private bool _needsCut = true, _needsCook = true;
    private bool _needsCutO, _needsCookO;
    [SerializeField]  private float _weight=0.5f;

    private void Awake()
    {
        _needsCookO = _needsCook;
        _needsCutO = _needsCut;
        _mf = GetComponent<MeshFilter>();
        CheckReadiness();
    }

    public void SetFirstValues()
    {
        _needsCut = _needsCutO;
        _needsCook = _needsCookO;   
        _mf.mesh = _firstMesh;
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
                OnLaunch();

            }
            else OnLaunch();          
        }
       
        if (collision.gameObject.CompareTag("CuttingZone") && _needsCut && collision.gameObject.GetComponent<CZone>().canUse)
        {
            _needsCut = false;
            SetPrepare(collision);
        }      
        if (collision.gameObject.CompareTag("CookingZone") && _needsCook && collision.gameObject.GetComponent<CZone>().canUse)
        {
            _needsCook = false;
            SetPrepare(collision);
        }   
    }
      
    public void OnLaunch()
    {
        _isLaunched = false;
        gameObject.SetActive(false);
    }
    public void SetPrepare(Collision collision)
    {
        _mf.mesh = _newMesh;
        CheckReadiness();
        collision.gameObject.GetComponent<CZone>().newIngredient = this.gameObject;
        collision.gameObject.GetComponent<CZone>().StartPreparing();
    }
}
