using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectController : MonoBehaviour
{
    [SerializeField] private string _typeOfIngredient="";

    public Coroutine _coroutine;

    [Header("Meshes")]
    private MeshFilter _mf;
    [SerializeField] private Mesh _firstMesh, _newMesh;

    [Header("Values")]

    [SerializeField] private bool isSausage = false;

    [SerializeField] private bool _isLaunched = false, _isReady=false;
    [SerializeField] private bool _needsCut = true, _needsCook = true;
    private bool _needsCutO, _needsCookO;
    [SerializeField]  private float _weight=0.5f;

    [SerializeField] private float _timeToRot = 30;

    private void Awake()
    {
        _needsCookO = _needsCook;
        _needsCutO = _needsCut;
        _mf = GetComponent<MeshFilter>();
        CheckReadiness();
    }

    public void Initialize()
    {
        _needsCut = _needsCutO;
        _needsCook = _needsCookO;   
        _mf.mesh = _firstMesh;
        GetRotten();
    }

    private void CheckReadiness()
    {
        if(!_needsCut && !_needsCook) _isReady = true; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isReady && other.CompareTag("PlayerZone"))
        {
            other.GetComponent<PlayerZone>().CheckIngredient(_typeOfIngredient);
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_isLaunched)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Invoke Function to stun player
                EndLaunch();

            }
            else EndLaunch();          
        }
       
        if (collision.gameObject.CompareTag("CuttingZone") && _needsCut && collision.gameObject.GetComponent<CZone>().canUse)
        {
            _needsCut = false;
            _mf.mesh = _newMesh;
            SetPrepare(collision);
            if (isSausage)
            {
                _needsCook = true;
            }
        }      
        if (collision.gameObject.CompareTag("CookingZone") && _needsCook && collision.gameObject.GetComponent<CZone>().canUse)
        {
            _needsCook = false;
            SetPrepare(collision);
        }
        
    }
      
    public void EndLaunch()
    {
        _isLaunched = false;
        gameObject.SetActive(false);
    }
    public void SetPrepare(Collision collision)
    {
        StopCoroutine(_coroutine);
       
        CheckReadiness();
        collision.gameObject.GetComponent<CZone>().newIngredient = this.gameObject;
        collision.gameObject.GetComponent<CZone>().StartPreparing();
      
    }

    public void GetRotten()
    {
        _coroutine = StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(_timeToRot);
        gameObject.SetActive(false);
        StopCoroutine(Deactivate());

    }
}
