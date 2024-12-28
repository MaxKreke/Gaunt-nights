using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignCamera : MonoBehaviour
{
    public float mouseSensitivity = 2;
    public float verticalRotation = 0;
    public float horizontalRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= inputY;
        horizontalRotation += inputX;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        transform.position = transform.parent.position + Vector3.up * 1;
    }
}
