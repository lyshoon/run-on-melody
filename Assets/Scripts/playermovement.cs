using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float landDistance = 2.5f;
    public float positionOffset = 1f;
    private int currentLane = 1;
    private int sidePosition = 0;

    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentLane = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            currentLane = 1;  
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentLane = 2;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            currentLane = 3;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && sidePosition > -1)
        {
            sidePosition--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && sidePosition < 1)
        {
            sidePosition++;
        }

        float lanePositionX = (currentLane - 1.5f) * landDistance;
        float sideOffset = sidePosition * positionOffset;
        Vector3 targetPosition = new Vector3(lanePositionX + sideOffset, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }
}
