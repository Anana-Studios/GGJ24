using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZone : MonoBehaviour
{
    public List<GameObject> ingredient = new List<GameObject>();
    public GameObject newIngredient;

    [SerializeField] private Transform _pickPosition;
    [SerializeField] private float _timer = 5;
    public bool canUse = true;



    public void StartPreparing()
    {
        if (newIngredient != null && canUse)
        {
            canUse = false;
            ingredient.Add(newIngredient);
            newIngredient.GetComponent <MeshRenderer>().enabled=false;
            newIngredient.GetComponent <Collider>().enabled=false;
            newIngredient.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(Preparing());
        }
    }

    public void EndPreparing()
    {
        if (newIngredient != null)
        {
            ingredient.Remove(newIngredient);
            newIngredient.transform.position = _pickPosition.position;
            newIngredient.GetComponent<MeshRenderer>().enabled = true;
            newIngredient.GetComponent<Collider>().enabled = true;
            newIngredient.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            canUse = true;
            newIngredient = null;
        }
    }

    IEnumerator Preparing()
    {
        yield return new WaitForSecondsRealtime(_timer);
        EndPreparing();
        StopCoroutine(Preparing());
    }
}
