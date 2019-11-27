using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWanderer : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float walkTurnSpeed = 5.0f;
    public float runSpeed = 5.0f;
    public float runTurnSpeed = 5.0f;
    public float maxHeadingChange = 5.0f;

    public Transform Target { get; set; }
    public float waitToCharge = 5.0f;
    public float attackRadius = 1.5f;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Vector3 targetRotation;

    private Animator animController;
    private int runStateHash;
    private int AttackStateHash;

    private bool isDead = false;
    private float heading;

    // Start is called before the first frame update
    void Start()
    {
   heading = Random.Range(0, 360);
        navAgent.transform.eulerAngles = new Vector3(0, heading, 0);

    }
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animController = GetComponent<animator>();
     
      

        runStateHash = Animator.StringToHash("Base Layer Run");
        AttackStateHash = Animator.StringToHash("Base Layer Attack");
    }
    public void spotEnemy(Transform target)
    {
        Target = target;
       
    }

    private void NewHeading()
    {
        float floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        float ceiling = Mathf.Clamp(heading - maxHeadingChange, 0, 360);

        heading = Random.Range(floor, ceiling);
        targetRotation = new Vector3(0, heading, 0);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Target)
        {
            Vector3 targetDirection = Target.position;
            targetDirection.y = transform.position.y;
            targetDirection -= transform.position;
            Quaternion desiredRotation = Quaternion.LookRotation(targetDirection);

            if (waitToCharge > 0)
            {
                waitToCharge -= Time.deltaTime;
            }

            else
            {
                
                navAgent.stoppingDistance = attackRadius;
                navAgent.angularSpeed = runTurnSpeed;
                navAgent.speed = runSpeed;
                navAgent.setDestination(Target.position);
            }
        }
        else
        {
            navAgent.stoppingDistance = 0;
            navAgent.angularSpeed = walkTurnSpeed;
            navAgent.Speed = walkSpeed;

            NewHeading();

            navAgent.transform.eulerAngles = Vector3.Slerp(navAgent.transform.eulerAngles, targetRotation, walkTurnSpeed * Time.deltaTime);
            navAgent.setDestination(navAgent.transform.position + navAgent.transform.foward * walkSpeed);
        }
    }
}
