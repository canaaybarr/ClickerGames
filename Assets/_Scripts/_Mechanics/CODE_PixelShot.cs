using System;
using UnityEngine;


public class CODE_PixelShot : MonoBehaviour
{/*
    // Paste those variables to EX_GameManager

    //-- Mechanic Variables
    public GameObject player;

    private Rigidbody playerRb;

    public LineRenderer l1;

    public LineRenderer l2;

    public float speedModifier;

    private Camera cam;

    public LayerMask inputLayer;

    private Vector3 mouseStartPosition, playerStartPosition;
    //-- Mechanic Variables

    void Start() // Paste the codes to EX_GameManager > Start
    {
        playerRb = player.GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update() // Paste the codes to EX_GameManager > Update > STATE.Play
    {
        if (Input.GetMouseButtonDown(0))
        {

            playerRb.isKinematic = true;

            l1.enabled = true;
            l2.enabled = true;

            Vector3 pPos = player.transform.position;
            l1.SetPosition(0, pPos);
            l2.SetPosition(1, pPos);

            mouseStartPosition = CastRayFromCameraToMousePosition();
            playerStartPosition = player.transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            l1.SetPosition(1, player.transform.position);
            l2.SetPosition(0, player.transform.position);

            Vector3 deltaMousePosition = CastRayFromCameraToMousePosition() - mouseStartPosition;
            deltaMousePosition.y = 0;

            Vector3 targetPos = playerStartPosition + deltaMousePosition;


            if (Vector3.Distance(targetPos, playerStartPosition) > 2)
            {
                Vector3 newPos = (targetPos - playerStartPosition).normalized;
                targetPos = playerStartPosition + (newPos * 2);
            }

            targetPos.x = Mathf.Clamp(targetPos.x, playerStartPosition.x - 4, playerStartPosition.x + 4);

            player.transform.position = targetPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            l1.enabled = false;
            l2.enabled = false;

            playerRb.isKinematic = false;
            playerRb.velocity = Vector3.zero;
            Vector3 forcePlayerStartPos = playerStartPosition;
            playerRb.AddForce(
                (forcePlayerStartPos - player.transform.position).normalized *
                Vector3.Distance(player.transform.position, playerStartPosition) * speedModifier,
                ForceMode.VelocityChange);
        }
    }
    
    //----------------------------------------------------------------------------------------------------
    
    // Paste those methods to EX_GameManager
    
    //-- Mechanic Methods
    public Vector3 CastRayFromCameraToMousePosition() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, inputLayer))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    
    public Vector3 CastRayFromPlayerToDirection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, direction, out hit, 1000))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    //-- Mechanic Methods
    */
}
