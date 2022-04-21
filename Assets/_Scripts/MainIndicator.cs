using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIndicator : MonoBehaviour
{
    public Transform[] followers;

    private Vector3 prevPos;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        prevPos = transform.position;
    }

    private void Update()
    {
        MoveFollowers();
    }

    private void MoveFollowers()
    {
        Vector3 diff = transform.position - prevPos;
        foreach (var follower in followers)
        {
            follower.transform.position += diff;
        }
        prevPos = transform.position;
    }
}
