using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakMain : MonoBehaviour
{
    SneakTails compSnakeTail;
    [SerializeField]
    EatingArea eatArea;
    [SerializeField]
    CheckPoints CheckP;
    [SerializeField]
    ModifySpawner modspawn;
    Rigidbody rgBody;


    [Range(1, 20)]
    public float speedForward;
    [Range(1, 20)]
    public float speedHorizontal;

    public int direction;
    public bool borderRight;
    public bool borderLeft;

    void Start()
    {
        compSnakeTail = GetComponent<SneakTails>();
        rgBody = GetComponent<Rigidbody>();
        for (int i = 0; i <= eatArea.countEat; i++) compSnakeTail.AddSegments();
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        ChangedColor();
    }
    public void Movement()
    {
        rgBody.velocity = new Vector3(direction * speedHorizontal,0, speedForward);
    } 
    public void RightClickButton()
    {
        if (!borderRight && !modspawn.fever)
            direction = 1;   
    }
    public void LeftClickButton()
    {
        if (!borderLeft && !modspawn.fever)
            direction = -1; 
    }
    public void ButtonUp()
    {
        direction = 0;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Border_2")
        { ButtonUp(); borderLeft = true; }
        if (collision.gameObject.name == "Border_1")
        { ButtonUp(); borderRight = true; }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Border_2") borderLeft = false;
        if (collision.gameObject.name == "Border_1") borderRight = false;
    }

    public void ChangedColor()
    {
        for (int j = 0; j < compSnakeTail.snakeSegments.Count; j++)
        {
            for (int i = 0; i < CheckP.CheckPoint.Count; i++)
            {
                if (compSnakeTail.snakeSegments[j].transform.position.z >= CheckP.CheckPoint[i].transform.position.z - 1 && compSnakeTail.snakeSegments[j].transform.position.z <= CheckP.CheckPoint[i].transform.position.z+1)
                {
                    compSnakeTail.snakeSegments[j].GetComponent<MeshRenderer>().material = CheckP.CheckPoint[i].GetComponent<MeshRenderer>().material;
                }
                if(compSnakeTail.SnakeHead.position.z >= CheckP.CheckPoint[i].transform.position.z - 1 && compSnakeTail.SnakeHead.position.z <= CheckP.CheckPoint[i].transform.position.z + 1)
                    compSnakeTail.SnakeHead.GetComponent<MeshRenderer>().material = CheckP.CheckPoint[i].GetComponent<MeshRenderer>().material;
            }
        }
        
    }
}
