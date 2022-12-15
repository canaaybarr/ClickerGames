using System.Collections;
using System.Collections.Generic;
using _Scripts._Managers;
//using UnityEditor.UIElements;
using UnityEngine;

public class PlayerRunnerController : MonoBehaviour
{
    public bool autoCentering, keepWalking;

    public float sensitivity, speed, rotateClamp, xPosClamp;
    
    private bool holding;

    private Vector3 pos1, pos2;

    private Rigidbody rb;

    private Animator anim;

    private bool initWalk;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (EX_GameManager.Instance.state == GameManager<EX_GameManager>.STATE.Play)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pos1 = EX_GameManager.Instance.GetMousePosition();

                initWalk = true;
                
                anim.SetBool("Running", true);
                
                holding = true;
            }

            if (Input.GetMouseButton(0) && holding)
            {
                pos2 = EX_GameManager.Instance.GetMousePosition();

                Vector3 delta = pos1 - pos2;

                pos1 = pos2;

                float yRot = transform.eulerAngles.y - delta.x * sensitivity * Time.deltaTime * 1000;

                transform.eulerAngles = new Vector3(transform.eulerAngles.x,EX_GameManager.Instance.ClampAngle(yRot, -rotateClamp, rotateClamp), transform.eulerAngles.z);

                print(rb.velocity);
                
                rb.velocity = transform.forward * speed;
            }
            else
            {
                if (autoCentering)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x,Mathf.LerpAngle(transform.eulerAngles.y, 0, 0.05f), transform.eulerAngles.z);
                }

                if (keepWalking && initWalk)
                {
                    rb.velocity = transform.forward * speed;
                }
            }
            
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xPosClamp, xPosClamp), transform.position.y, transform.position.z);

            if (Input.GetMouseButtonUp(0))
            {
                holding = false;

                if (!keepWalking)
                {
                    rb.velocity = Vector3.zero;

                    anim.SetBool("Running", false);
                }
            }
        }
    }
}
