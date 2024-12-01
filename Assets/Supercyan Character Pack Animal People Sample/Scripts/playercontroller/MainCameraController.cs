using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [Header("Camera Controller")]
    public Transform target;
    public float gap = 10f;
    public float rotSpeed = 6f;

    [Header("Camera handling")]
    public float minVerAngle = -14f;
    public float maxVerAngle = 45f;
    public Vector2 framingBalance;

    private float rotX;
    private float rotY;
    public bool invertX;
    public bool invertY;

    private float invertXValue;
    private float invertYValue;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        invertXValue = invertX ? -1 : 1;
        invertYValue = invertY ? -1 : 1;

        rotX += Input.GetAxis("Mouse Y") * invertYValue * rotSpeed;
        rotX = Mathf.Clamp(rotX, minVerAngle, maxVerAngle); 
        rotY += Input.GetAxis("Mouse X") * invertXValue * rotSpeed;

        Quaternion targetRotation = Quaternion.Euler(rotX, rotY, 0);

        Vector3 focusPos = target.position + new Vector3(framingBalance.x, framingBalance.y);
        transform.position = focusPos - targetRotation * new Vector3(0, 0, gap);
        transform.rotation = targetRotation;
        
    }

    public Quaternion flatRotation => Quaternion.Euler(0, rotY, 0);
}
