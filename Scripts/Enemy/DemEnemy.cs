using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemEnemy : MonoBehaviour
{
   private Animator _anim;
   private float _vertical, _horizontal;
   private NavMeshAgent _navMeshAgent;
   private float _roamTime;

   public int animeState = 0;
   public float maxRoamDistance;

   public bool isRoaming = false;

   private void Start()
   {
      _anim = GetComponent<Animator>();
      _navMeshAgent = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {
      if (animeState == 0)
      {
         Roam();
      }
      else
      {
        
      }
      Animations();
   }

   void Roam()
   {
      if (animeState != 0) isRoaming = false; return;
      isRoaming = true;
      if (Time.time > _roamTime)
      {
         float a = Random.Range(0, 2);
         _roamTime = Time.time + 10;
         _navMeshAgent.SetDestination(new Vector3(transform.position.x + Random.Range(maxRoamDistance / 2, maxRoamDistance) * (a == 1 ? 1: -1), 0,
            transform.position.x + Random.Range(maxRoamDistance / 2, maxRoamDistance) * (a == 1 ? 1 : -1))) ;
      }
   }
   void Animations()
   {
      _vertical = Mathf.Lerp(_vertical, _navMeshAgent.remainingDistance > 0 ? 1 : 0, 2 * Time.deltaTime);
      _anim.SetFloat("Vertical", _vertical);
      _anim.SetFloat("Horizontal", _horizontal);
      _anim.SetInteger("State", animeState);
   }
   string GetCharacterState()
   {
      switch (animeState)
      {
         case 0:
            return "Peaceful";
            break;
         case 1:
            return "Combat";
            break;
      }
      return "Out of range";
   }
}
