using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    [Header("Referencias")]
    public Transform head;
    public GameObject segmentPrefab;

    [Header("Configuración")]
    public float distanceBetween = 0.5f;
    public int initialSize = 5;
    public float smoothSpeed = 10f;

    private List<Transform> segments = new List<Transform>();
    private List<Vector3> positionsHistory = new List<Vector3>();

    void Start()
    {
        positionsHistory.Add(head.position);

        for (int i = 0; i < initialSize; i++)
        {
            AddSegment();
        }
    }

    void Update()
    {
        RecordHeadPosition();
        MoveSegments();
    }

    void RecordHeadPosition()
    {
        if (Vector3.Distance(positionsHistory[0], head.position) > distanceBetween)
        {
            positionsHistory.Insert(0, head.position);
        }
    }

    void MoveSegments()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            int index = Mathf.Min(i * 3, positionsHistory.Count - 1);

            Vector3 targetPos = positionsHistory[index];

            segments[i].position = Vector3.Lerp(
                segments[i].position,
                targetPos,
                Time.deltaTime * smoothSpeed
            );
        }
    }

    public void AddSegment()
    {
        GameObject segment = Instantiate(segmentPrefab, head.position, Quaternion.identity);
        segments.Add(segment.transform);
    }
}