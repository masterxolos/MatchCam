using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorFollower : MonoBehaviour
{
    public Transform indictator;
    public bool x = true, y = true, z = true;

    private Vector3 pos;

    private void Update()
    {
        if(indictator != null)
        {
            if (x)
                pos.x = indictator.position.x;
            else
                pos.x = transform.position.x;

            if (y)
                pos.y = indictator.position.y;
            else
                pos.y = transform.position.y;

            if (z)
                pos.z = indictator.position.z;
            else
                pos.z = transform.position.z;

            transform.position = pos;
        }
    }

}
