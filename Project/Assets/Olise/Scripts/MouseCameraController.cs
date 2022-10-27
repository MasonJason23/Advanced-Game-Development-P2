using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 mousePos;
    public float mouseSensitivity;
    public Transform ParentTransform;
    void Start()
    {
        // Locks the cursor
        Cursor.lockState = CursorLockMode.Locked;
        // Confines the cursor
        Cursor.lockState = CursorLockMode.Confined;
        // Hides the cursor...
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        mousePos.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        mousePos.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.localRotation = Quaternion.Euler(-mousePos.y,mousePos.x, 0);
        ParentTransform.localRotation = Quaternion.Euler(ParentTransform.position.y,mousePos.x,
            0);
    }
}
