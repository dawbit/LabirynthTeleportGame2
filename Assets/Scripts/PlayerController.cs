using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 12f; // szybkość naszej postaci
    Vector3 velocity; // posłuży do wyliczenia prędkości w dół
    CharacterController characterController; // komponent CHaracter Controller

    public Transform groundCheck; // miejsce na nasz obiekt
    public LayerMask groundMask; // grupa obiektów, które będą warstwą używane za teren

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        RaycastHit hit; // zmienna w które zapisywana jest referencja do uderzonego obiektu
        if (Physics.Raycast(groundCheck.position, transform.TransformDirection(Vector3.down), out hit, 0.4f, groundMask)) 
        {
            string terrainType;
            terrainType = hit.collider.gameObject.tag; //sprawdzamy tag tego w co uderzyliśmy

            switch (terrainType) 
            {
                default:        // standardowa prędkość gdy chodzimy po dowolnym terenie
                    speed = 12;
                    break;
                case "Low":    //prędkość gdy chodzimy po terenie spowalniającym
                    speed = 3;
                    break;
                case "High":   // prędkość gdy chodzimy po terenie przyspieszającym
                    speed = 20;
                    break;
            }
        }
    }
}
