using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatroller : MonoBehaviour, AIBrain
{
    
    public GameObject waypointContainer;
    public float minDistance = 3.0f;
    public float attackRadius = 3.0f;

    public Transform Target { get; set; }
    public Collider weaponCollider;

    public bool bodyDecays = true;
    public float bodyDecayDelay = 3.0f;

    private NavMeshAgent navAgent;
    private Transform[] waypoints;
    private int currentWaypoint = 0;

    private Animator animController;
    private int runStateHash;
    private int attackStateHash;

    // private HealthBar healthBar;
    private bool isDead = false;

     public Transform player;
    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        runStateHash = Animator.StringToHash("Base Layer.Run");
        attackStateHash = Animator.StringToHash("Base Layer.Attack");
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform[] potentialWPs = waypointContainer.GetComponentsInChildren<Transform>();
        waypoints = new Transform[potentialWPs.Length - 1];

        for (int i = 1; i < potentialWPs.Length; ++i)
        {
            waypoints[i - 1] = potentialWPs[i];
        }

        if (weaponCollider)
        {
            weaponCollider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Target)
        {
            navAgent.stoppingDistance = attackRadius;
            navAgent.SetDestination(Target.position);

            // update animator controller
        }
        else
        {
            navAgent.stoppingDistance = 0;
            if ((transform.position - waypoints[currentWaypoint].position).magnitude < minDistance)
            {
                currentWaypoint += 1;
                if (currentWaypoint == waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            navAgent.SetDestination(waypoints[currentWaypoint].position);
        }
        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                        Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.5f);
            }
        }
    }

    public void SpotEnemy(Transform target)
    {
        Target = target;

 
    }

    public void LostEnemy()
    {
        Target = null;
    }
}
