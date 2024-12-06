using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void Update()
    {
        Vector3 tragetPos = transform.position = player.position + offset;
        tragetPos.x = 0;
        transform.position = tragetPos;
    }
}
