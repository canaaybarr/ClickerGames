using System;
using UnityEngine;


public class CODE_Joystick : MonoBehaviour
{/*
    // Paste those variables to EX_GameManager

    //-- Mechanic Variables
    public Joystick joystick;

    private Vector3 commandDirection;

    public GameObject player;

    private Rigidbody playerRb;

    public float playerSpeed = 20;
    //-- Mechanic Variables

    public void Start() // Paste the codes to EX_GameManager > Start
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    public void Update() // Paste the codes to EX_GameManager > Update > STATE.Play
    {
        if (Input.GetMouseButton(0))
        {
            var deltaPosition = joystick.Direction;

            commandDirection = deltaPosition.normalized;

            commandDirection = new Vector3(commandDirection.x, 0, commandDirection.y);

            var playerPos = player.transform.position;

            var playerlookatPosition = playerPos + commandDirection;

            Vector3 targetDir = playerlookatPosition - playerPos;

            float step = 7 * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(player.transform.forward, targetDir, step, 0.0f);

            player.transform.rotation = Quaternion.LookRotation(newDir);

            Vector3 targetVelocity = (player.transform.forward * playerSpeed);

            targetVelocity.y = playerRb.velocity.y;

            playerRb.velocity = targetVelocity;

            float magnitude = playerRb.velocity.magnitude;
        }

        if (Input.GetMouseButtonUp(0))
        {
            playerRb.velocity = Vector3.zero;
        }
    }
*/
}