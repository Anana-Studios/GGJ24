using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngredientsPooling : MonoBehaviour
{
    [Header("Pool Parameters")]

    public GameObject ingredientPrefab;
    public Transform supplyPosition;
    private List<GameObject> _ingredients= new List<GameObject>();

    [SerializeField] private int _amount = 10;


    [Header("Values")]

    private bool _canSupply = true;
    [SerializeField] private float _timer=1;

    private void Awake()
    {
        for (int i = 0; i < _amount; i++)
        {
            GameObject newIngredient = Instantiate(ingredientPrefab);
            _ingredients.Add(newIngredient);    
            newIngredient.SetActive(false);
        }
    }

    [ContextMenu("Get New Ingredient")]
    public void GetIngredient()
    {  
        var newIngredient = GetPooledObject();
        if (_canSupply && newIngredient != null)
        {
            _canSupply = false;
            newIngredient.transform.position = supplyPosition.position;
            newIngredient.SetActive(true);
            if (newIngredient.GetComponent<ObjectController>()!=null) newIngredient.GetComponent<ObjectController>().SetFirstValues();
        }
        else
        {
            Debug.Log("Wait For Supplies");
        }
        StartCoroutine(WaitForSupply());
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _amount; i++)
        {
            if (!_ingredients[i].activeInHierarchy)
            {
                return _ingredients[i];
            }
        }
        return null;
    }

    IEnumerator WaitForSupply()
    {
        yield return new WaitForSecondsRealtime(_timer);
        _canSupply = true;
        StopCoroutine(WaitForSupply());
    }
}
