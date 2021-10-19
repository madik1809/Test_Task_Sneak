using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            Debug.Log("Change");
            GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
        }
    }
}
