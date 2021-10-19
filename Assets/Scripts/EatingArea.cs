using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EatingArea : MonoBehaviour
{
    public int countGems;
    public int countEat;
    public bool isCheck;

    [SerializeField]
    private List<Transform> gems = new List<Transform>();

    [SerializeField]
    private SneakTails sneakTail;
    [SerializeField]
    CheckPoints chPoints;
    [SerializeField]
    ModifySpawner modSpawn;
    [SerializeField]
    GameObject gemsCounttext;

    [SerializeField]
    int countToFever;
    [SerializeField]
    private int lastgem;

    public void Update()
    {
        gemsCounttext.GetComponent<Text>().text = countGems.ToString();
        if (countToFever >= 3)
        {
            StartCoroutine(modSpawn.Fever());
            gems.RemoveRange(0, gems.Count);
            countToFever = 0;
        }
    }
    void Restart()
    {
        SceneManager.LoadScene("1", LoadSceneMode.Single);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Traps"))
        {
            Destroy(other.gameObject);
            if (!modSpawn.fever)
                Restart();
        }
        if (other.CompareTag("Gems"))
        { 
            countGems++;
            Transform trans = other.transform;
            gems.Add(trans);
            if (gems.Count >= 2)
            {
                int i = gems.Count-1;
                do
                {
                    if (gems[i].position.z == gems[i - 1].position.z + modSpawn.rangeToOther)
                        countToFever += 1;
                    else countToFever = 0;
                } while (i <= 0);
            }
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Eat"))
        {
            Destroy(other.gameObject);
            if (sneakTail.snakeSegments[sneakTail.snakeSegments.Count - 1].GetComponent<MeshRenderer>().material.color == other.GetComponent<MeshRenderer>().material.color || modSpawn.fever)
            {
                countEat++;
                sneakTail.AddSegments();
            }
            else
                Restart();
        }
        
        if (other.CompareTag("CheckPoint"))
            chPoints.CreatCheckpoint();
        
        if (other.CompareTag("Respawn"))
            Restart();

    }
}
