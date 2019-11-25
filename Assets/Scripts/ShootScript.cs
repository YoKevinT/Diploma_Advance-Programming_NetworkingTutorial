using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootScript : NetworkBehaviour
{
    // Public:
    // Amount of bullets that can be fired per second
    public float fireRate = 1f;
    // Range that the bullet can travel
    public float range = 100f;
    // LayerMask of which layer to hit
    public LayerMask mask;

    // Private:
    // Timer for the fireRate
    private float fireFactor = 0f;
    private float fireInterval;

    // Reference to the camera child
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if(isLocalPlayer)
        {
            HandleInput();
        }
    }

    [Command]
    void Cmd_PlayerShot(string _id)
    {
        Debug.Log("Player" + _id + "has been shoot!");
    }

    [Client]
    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range, mask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Cmd_PlayerShot(hit.collider.name);
            }
        }
    }

    void HandleInput()
    {
        fireFactor = fireFactor + Time.deltaTime;
        fireInterval = 1 / fireRate;

        if(fireFactor >= fireInterval)
        {
            if (Input.GetMouseButton(1))
            {
                Shoot();
            }
        }
    }
}
