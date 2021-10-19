using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    ModifySpawner modSpawner;

    [SerializeField]
    private GameObject exampleCP;

    [SerializeField]
    public Material[] materials;
    [SerializeField]
    public List<GameObject> CheckPoint = new List<GameObject>();

    public float prelastDistance;
    public float lastDistance;

    private Material lastMat;
    int random;
    public int distance;

    
    public void CreatCheckpoint()
    {
        CheckPoint.Add(Instantiate(exampleCP, new Vector3(CheckPoint[CheckPoint.Count - 1].transform.position.x, CheckPoint[CheckPoint.Count - 1].transform.position.y, CheckPoint[CheckPoint.Count - 1].transform.position.z + distance), CheckPoint[CheckPoint.Count-1].transform.rotation));
        do 
        {
            CheckPoint[CheckPoint.Count - 1].GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
        }
        while (lastMat.color == CheckPoint[CheckPoint.Count - 1].GetComponent<MeshRenderer>().material.color);
        CheckPoint[CheckPoint.Count - 1].GetComponent<MeshRenderer>().material.color = CheckPoint[CheckPoint.Count - 1].GetComponent<MeshRenderer>().material.color;
        lastMat = CheckPoint[CheckPoint.Count - 1].GetComponent<MeshRenderer>().material;
        prelastDistance = lastDistance;
        lastDistance = CheckPoint[CheckPoint.Count-1].transform.position.z;
        random = Random.Range(0, materials.Length);
        modSpawner.EatSpawn(lastMat, materials[random]);
        modSpawner.GemsSpawner();
    }

    void Start()
    {
        modSpawner = GetComponent<ModifySpawner>();
        CheckPoint.Add(exampleCP);
        exampleCP.GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
        lastMat = exampleCP.GetComponent<MeshRenderer>().material;
        prelastDistance = lastDistance;
        lastDistance = transform.position.z;
    }
}
