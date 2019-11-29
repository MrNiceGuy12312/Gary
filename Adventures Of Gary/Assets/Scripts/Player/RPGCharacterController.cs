using UnityEngine;

public class RPGCharacterController : MonoBehaviour
{
    public bool walkByDefault = true;
    public float gravity = 20.0f;

    // movement variables
    public float jumpSpeed = 8.0f;
    public float runSpeed = 10.0f;
    public float walkSpeed = 4.0f;
    public float turnSpeed = 250.0f;
    public float moveBackwardMultiplier = 0.75f;

    // internal variables
    private float speedMultiplier = 0.0f;
    private bool isGrounded = false;
    private Vector3 moveDirection = Vector3.zero;
    private bool isWalking = false;
    private bool jumping = false;
    private bool mouseSideButton = false;

    // character and animation controllers
    private CharacterController controller;
    private Animator animController;

    public Collider weaponCollider;
    private int attackStateHash;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animController = GetComponent<Animator>();

        attackStateHash = Animator.StringToHash("UpperTorso.Attack");
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = walkByDefault;

        if (Input.GetAxis("Run") != 0)
        {
            isWalking = !walkByDefault;
        }

        if (isGrounded)
        {
            // check right mouse button for direction control
            if (Input.GetMouseButton(1))
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            }
            else
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            }

            // auto-move toggle button check
            if (Input.GetButtonDown("Toggle Move"))
            {
                mouseSideButton = !mouseSideButton;
            }

            // check for auto-move interrupt
            if (mouseSideButton && (Input.GetAxis("Vertical") != 0 || Input.GetButton("Jump")) ||
                (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
            {
                mouseSideButton = false;
            }

            // check for mouse movement
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1) || mouseSideButton)
            {
                moveDirection.z = 1;
            }

            // check for strafing
            if (!(Input.GetMouseButton(1) && Input.GetAxis("Horizontal") != 0))
            {
                moveDirection.x -= Input.GetAxis("Strafing");
            }

            // check for diagonal movement
            if (((Input.GetMouseButton(1) && Input.GetAxis("Horizontal") != 0) ||
                Input.GetAxis("Strafing") != 0) && Input.GetAxis("Vertical") != 0)
            {
                moveDirection *= 0.7f;
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                speedMultiplier = moveBackwardMultiplier;
            }
            else
            {
                speedMultiplier = 1f;
            }

            // set speed
            moveDirection *= isWalking ? walkSpeed * speedMultiplier : runSpeed * speedMultiplier;

            // check for jumping
            if (Input.GetButton("Jump"))
            {
                jumping = true;
                moveDirection.y = jumpSpeed;
            }

            // update animation state
            if (animController)
            {
                animController.SetFloat("Direction", moveDirection.x);
                animController.SetFloat("Speed", moveDirection.z);
            }

            moveDirection = transform.TransformDirection(moveDirection);
        } // end if (isGrounded)

        // use camera turning with right mouse button
        if (Input.GetMouseButton(1))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        }

        // apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // check for grounding
        isGrounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
        jumping = isGrounded ? false : jumping;
        animController.SetBool("Jumping", jumping);

        AnimatorStateInfo currentStateInfo = animController.GetCurrentAnimatorStateInfo(1);
        if (currentStateInfo.fullPathHash != attackStateHash)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animController.SetBool("IsAttacking", true);
            }
            else
            {
                animController.SetBool("IsAttacking", false);
                if (weaponCollider)
                {
                    weaponCollider.enabled = false;
                }
            }
        }
        else if (weaponCollider)
        {
            weaponCollider.enabled = true;
        }
    }
}
