using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakTails : MonoBehaviour
{
    public Transform SnakeHead;
    public float segmentDiameter;

    public List<Transform> snakeSegments = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        positions.Add(SnakeHead.position);
    }

    private void Update()
    {
        float distance = ((Vector3)SnakeHead.position - positions[0]).magnitude;

        if (distance > segmentDiameter)
        {
            Vector3 direction = ((Vector3)SnakeHead.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * segmentDiameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= segmentDiameter;
        }

        for (int i = 0; i < snakeSegments.Count; i++)
        {
            snakeSegments[i].position = Vector3.Lerp(positions[i + 1], positions[i], distance / segmentDiameter);
        }
    }

    public void AddSegments()
    {
        Transform segment = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity);
        snakeSegments.Add(segment);
        positions.Add(segment.position);
    }
}
