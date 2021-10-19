using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContol : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    void Start()
    {
        offset = new Vector3(transform.position.x - target.transform.position.x,transform.position.y - target.transform.position.y, transform.position.z + target.transform.position.z);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z-10);
    }
}
