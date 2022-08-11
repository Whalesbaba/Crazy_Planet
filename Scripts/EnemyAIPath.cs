using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIPath : MonoBehaviour
{
   public NavMeshAgent agent;
   public Transform player;
   public LayerMask whatIsGround, whatIsPlayer;
   private bool hasAniComp = false;
   public AudioManager aud;
   private AudioSource _audio;


   //Patrolling
   public Vector3 walkPoint;
   bool walkPointSet;
   public float walkPointRange;

   //Attacking
   public float timeBetweenAttacks;
   bool alreadyAttacked;

   //States
   public float sightRange, attackRange;
   public bool playerInSightRange, playerInAttackRange;


   void Start()
   {

      if (null != GetComponent<Animation>())
      {
         hasAniComp = true;
      }
      _audio = GetComponent<AudioSource>();

   }

   private void Awake()
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      agent = GetComponent<NavMeshAgent>();
   }
   private void Update()
   {
      playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
      playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

      if (!playerInSightRange && !playerInAttackRange) Patrolling();
      if (playerInSightRange && !playerInAttackRange && PlayerScript.playerIsAlive) ChasePlayer();
      if (playerInSightRange && playerInAttackRange && PlayerScript.playerIsAlive) AttackPlayer();
   }

   private void Patrolling()
   {
      if (!walkPointSet) SearchWalkPoint();

      if (walkPointSet)
         agent.SetDestination(walkPoint);

      Vector3 distanceToWaalkPoint = transform.position - walkPoint;
      GetComponent<Animation>().Play("move_forward");
      
      //Walkpoint reached

      if (distanceToWaalkPoint.magnitude < 1f)
         walkPointSet = false;
   }

   private void SearchWalkPoint()
   {
      //Calculate random point in range
      float randomz = Random.Range(-walkPointRange, walkPointRange);
      float randomx = Random.Range(-walkPointRange, walkPointRange);

      walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z);

      if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
         walkPointSet = true;

   }

   private void ChasePlayer()
   {
      agent.SetDestination(player.position);
      GetComponent<Animation>().CrossFade("move_forward_fast");
   }

   private void AttackPlayer()
   {
      
      //Make sure enemy doesn't move
      agent.SetDestination(transform.position);
      // aud.PlaySound("Slash");
      //_audio.Play();

      transform.LookAt(player);
      GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
      GetComponent<Animation>().CrossFadeQueued("idle_combat");

      if (!alreadyAttacked)
      {
         alreadyAttacked = true;
         Invoke(nameof(ResetAttack), timeBetweenAttacks);
      }
   }

   private void ResetAttack()
   {
      alreadyAttacked = false;
   }


}
