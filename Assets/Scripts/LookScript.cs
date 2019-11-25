using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LookScript : NetworkBehaviour
{
    // Public:
    // Sensitivity of the mouse
    public float mouseSensitivity = 2.0f;
    // Minimum & Maximum Y axis (degrees)
    public float minimumY = -90f;
    public float maximumY = 90f;

    // Private:
    // Yaw of the camera (Rotation on Y)
    private float yaw = 0f;
    // Pitch of the camera (Rotation on X)
    private float pitch = 0f;
    // Main camera reference
    private GameObject mainCamera;

    void Start()
    {
        // Lock the mouse
        Cursor.lockState = CursorLockMode.Locked;
        // Make cursor invisible
        Cursor.visible = false;
        // Gets reference to the camera inside of this gameobject
        Camera cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            mainCamera = cam.gameObject;
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            // Update input for local player only
            HandleInput();
        }
    }

    // Remember to use LateUpdate for when you move the camera
    void LateUpdate()
    {
        // Check if this is the local player
        if (isLocalPlayer)
        {
            // Rotate the camera up or down using pitch
            mainCamera.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }
    }

    // Gets called upon GameObject being destroyed
    void OnDetroy()
    {
        // Release the cursor
        Cursor.lockState = CursorLockMode.None;
        // Make cursor visible
        Cursor.visible = true;
    }

    void HandleInput()
    {
        float pitch = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        yaw += Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw = Mathf.Clamp(yaw, minimumY, maximumY);
        transform.localEulerAngles = new Vector3(-yaw, pitch, 0);
    }
}
