using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    float speed = 4;
    float rotSpeed = 80;
    float rot = 0f;
    float gravity = 8;
    public Collider weaponCollider;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

     void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
     void Update()
    {

        Movement();
        GetInput();
    }
    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("Attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("Attacking") == false)
                {
                    if (weaponCollider)
                    {
                        weaponCollider.enabled = false;
                    }


                    anim.SetBool("Running", true);
                    anim.SetInteger("Condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("Running", false);
                anim.SetInteger("Condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
    void GetInput()
    {
        if(controller.isGrounded)
        {
            if(Input.GetMouseButton(0))
            {
                if (anim.GetBool("Running") == true)
                {
                    anim.SetBool("Running", false);
                    anim.SetInteger("Condition", 0);
                }
                if (anim.GetBool("Running") == false)
                {
                    Attacking();
                }
            }
        }
    }
    void Attacking()
    {
       
        StartCoroutine (AttackRoutine());
        
    }
    IEnumerator AttackRoutine()
    {
        anim.SetBool("Attacking", true);
        anim.SetInteger("Condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("Condition", 0);
        anim.SetBool("Attacking", false);
    }
}
