using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // 1
    public Vector3 camOffset = new Vector3(0f, 0f, -.01f);

    // 2
    private Transform target;

    private void Start()
    {
        // 3
        target = GameObject.Find("Player").transform;
    }

    // 4
    void LateUpdate()
    {
        // 5
        this.transform.position = target.TransformPoint(camOffset);

        // 6
        this.transform.LookAt(target);
    }
}
