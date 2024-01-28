using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngredientsPooling : MonoBehaviour
{
   

    public GameObject onionPrefab, chiliPrefab, beansPrefab, sausagePrefab;

    [Header("Pool Parameters")]

    public Transform supplyPosition;
    private GameObject[] _ingredients;

    [SerializeField] private int _amount = 5;


    [Header("Values")]

    private int _currentIndex = 0;
    [SerializeField] private float _timer = 1;
    [SerializeField] private float _minForce=0, _maxForce=0.8f;

    private void Awake()
    {
        InitializeIngredients(onionPrefab, chiliPrefab, beansPrefab, sausagePrefab);

    }
    private void Start()
    {
        StartCoroutine(WaitForSupply());
    }
    [ContextMenu("Get New Ingredient")]
    public void GetIngredient()
    {
        var newIngredient = GetPooledObject();
        if (newIngredient != null)
        {      
            newIngredient.transform.position = supplyPosition.position;
            newIngredient.GetComponent<Rigidbody>().velocity = Vector3.zero;
            newIngredient.SetActive(true);
            if (newIngredient.GetComponent<ObjectController>() != null) newIngredient.GetComponent<ObjectController>().Initialize();
            newIngredient.GetComponent<Rigidbody>().AddForce(launchForce(), ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Wait For Supplies");
        }
    }

    public GameObject GetPooledObject()
    {
       
            int randomIndex = Random.Range(0, _ingredients.Length);
       
            
            return _ingredients[randomIndex];
            
        
    }

    private Vector3 launchForce()
    {
        float x = Random.Range(-_maxForce, _maxForce);
        float y = Random.Range(_minForce, _maxForce);
        float z = Random.Range(-_maxForce, _maxForce);

        return new Vector3(x, y, z);
    }

    IEnumerator WaitForSupply()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timer);
          GetIngredient();
        }
    }

    private void InitializeIngredients(params GameObject[] prefabs)
    {
        _ingredients = new GameObject[prefabs.Length * _amount];

        for (int j = 0; j < prefabs.Length; j++)
        {
            for (int i = 0; i < _amount; i++)
            {
                GameObject newIngredient = Instantiate(prefabs[j]);
                _ingredients[j * _amount + i] = newIngredient;
                newIngredient.SetActive(false);
            }
        }
    }

}
