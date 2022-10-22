using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {//przerwa 12:37-12:42
        Teleportation();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player") 
        {
            playerIsOverlapping = false;
        }
    }

    void Teleportation() 
    {
        if (playerIsOverlapping) 
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f) 
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciever.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }
}
