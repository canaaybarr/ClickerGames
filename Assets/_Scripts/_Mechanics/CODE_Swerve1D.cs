using System;
using UnityEngine;


public class CODE_Swerve1D : MonoBehaviour
{  /*
    // Paste those variables to EX_GameManager
    
    //-- Mechanic Variables
    public float clampValue, speedSideways;
    
    private bool holding;

    private Vector3 pos1, pos2;

    public GameObject player;

    private Rigidbody playerRb;
    //-- Mechanic Variables
    
    
    void Start() // Paste the codes to EX_GameManager > Start
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    
    void Update() // Paste the codes to EX_GameManager > Update > STATE.Play
    {
        if (Input.GetMouseButtonDown(0))
        {
            pos1 = GetMousePosition();

            holding = true;
        }

        if (Input.GetMouseButton(0) && holding) //set players velocity on X axis and clamp value
        {
            pos2 = GetMousePosition();

            Vector3 delta = pos1 - pos2;

            pos1 = pos2;

            playerRb.velocity =
                new Vector3(Mathf.Lerp(playerRb.velocity.x, -delta.x * speedSideways, 100f * Time.deltaTime),
                    playerRb.velocity.y, playerRb.velocity.z);

            player.transform.position =
                new Vector3(Mathf.Clamp(playerRb.transform.position.x, -clampValue, clampValue),
                    playerRb.transform.position.y, playerRb.transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            holding = false;

            playerRb.velocity = Vector3.zero;
        }
    }
    
    */
}
