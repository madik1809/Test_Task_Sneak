using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ModifySpawner : MonoBehaviour
{
    CheckPoints checkPoints;
    [SerializeField]
    EatingArea eatArea;
    [SerializeField]
    SneakMain sneakMain;

    [SerializeField]
    List<Material> mat = new List<Material>();
    [SerializeField]
    public List<GameObject> Gems = new List<GameObject>();
    [SerializeField]
    public List<GameObject> Traps = new List<GameObject>();

    [SerializeField]
    GameObject exampleEat;
    [SerializeField]
    GameObject exampleGem;
    [SerializeField]
    GameObject exampleTrap;

    GameObject lastRightEat;
    GameObject lastLeftEat;

    GameObject lastGem;
    GameObject lastTrap;

    [SerializeField]
    private int rangeToEat;
    [SerializeField]
    public int rangeToOther;

    public bool fever;
    void Start()
    {
        checkPoints = GetComponent<CheckPoints>();
    }  

    public void EatSpawn(Material edible, Material inedible)
    {
        checkPoints = GetComponent<CheckPoints>();
        if (mat.Count > 0)
        {
            mat.RemoveAt(0);
            mat.RemoveAt(0);
        }

        mat.Add(edible);
        mat.Add(inedible);

        while (inedible.color == edible.color)
        {
            inedible = checkPoints.materials[Random.Range(0, checkPoints.materials.Length)];
        }

        for (int i = 1; i <= 4; i++)
        {
            lastLeftEat = Instantiate(exampleEat, checkPoints.CheckPoint[checkPoints.CheckPoint.Count-1].transform.position, Quaternion.identity);
            lastLeftEat.transform.position = new Vector3(6.5f, 0.5f, (lastLeftEat.transform.position.z + rangeToEat * i) + Random.Range(-3,3));

            lastLeftEat.GetComponent<MeshRenderer>().material = mat[Random.Range(0, mat.Count)];

            lastRightEat = Instantiate(exampleEat, checkPoints.CheckPoint[checkPoints.CheckPoint.Count-1].transform.position,Quaternion.identity);
            lastRightEat.transform.position = new Vector3(10, 0.5f, (lastRightEat.transform.position.z + rangeToEat * i) + Random.Range(-3, 3));

            if (lastLeftEat.GetComponent<MeshRenderer>().material.color == edible.color)
                lastRightEat.GetComponent<MeshRenderer>().material = inedible;
            else
                lastRightEat.GetComponent<MeshRenderer>().material = edible;
        }
    }
    public void GemsSpawner()
    {
        
        int countGem;
        Vector3 posGem;
        Vector3 posTrap;

        posGem = new Vector3(8.2f, 1, lastLeftEat.transform.position.z + rangeToEat);
        posTrap = new Vector3(10, 1, posGem.z);

        Gems.Add(Instantiate(exampleGem, posGem, Quaternion.identity));
        Traps.Add(Instantiate(exampleTrap, posTrap, Quaternion.identity));

        posTrap = new Vector3(6.5f, 1, posGem.z);

        Traps.Add(Instantiate(exampleTrap, posTrap, Quaternion.identity));
        
        lastGem = Gems[Gems.Count - 1];
        lastTrap = Traps[Traps.Count - 1];
        for (int i = 1; i <= 4; i++)
        {
            countGem = Random.Range(1, 100);
            if (countGem % 2 == 0)
            {
                posGem = new Vector3(8.2f, 1, lastGem.transform.position.z + rangeToOther);
                Gems.Add(Instantiate(exampleGem, posGem, Quaternion.identity));
                lastGem = Gems[Gems.Count - 1];

                posTrap = new Vector3(10, 1, posGem.z);
                Traps.Add(Instantiate(exampleTrap, posTrap, Quaternion.identity));
                lastTrap = Traps[Traps.Count - 1];

                posTrap = new Vector3(6.5f, 1, posGem.z);
                Traps.Add(Instantiate(exampleTrap, posTrap, Quaternion.identity));
                lastTrap = Traps[Traps.Count - 1];
            }
            else
            {
                posGem = new Vector3(10, 1, lastGem.transform.position.z + rangeToOther);
                Gems.Add(Instantiate(exampleGem, posGem, Quaternion.identity));
                lastGem = Gems[Gems.Count - 1];
                posGem = new Vector3(6.5f, 1, lastGem.transform.position.z);
                Gems.Add(Instantiate(exampleGem, posGem, Quaternion.identity));
                lastGem = Gems[Gems.Count - 1];

                posTrap = new Vector3(8.2f, 1, posGem.z);
                Traps.Add(Instantiate(exampleTrap, posTrap, Quaternion.identity));
                lastTrap = Traps[Traps.Count - 1];
            }
        
        }

    }
    public IEnumerator Fever()
    {
        if (!fever)
        {
            fever = true;
            sneakMain.speedForward *= 3;
            sneakMain.transform.position = new Vector3(8, sneakMain.transform.position.y, sneakMain.transform.position.z);
            yield return new WaitForSeconds(5);
            eatArea.countGems = 0;
            sneakMain.speedForward /= 3;
            fever = false;
            StopCoroutine(Fever());
        }
    }
}
