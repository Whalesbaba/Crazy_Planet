using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
   public Transform goal;
   public float SpaceBetween = 1.5f;
   public float attackSpace = 10f;
   public bool attack = false;
   public Animator _anim;
   private int _animIDRun;
   private bool _hasAnimator;
   // Start is called before the first frame update
   void Start()
    {
      _anim = GetComponent<Animator>();
      _animIDRun = Animator.StringToHash("isRunning");
      _hasAnimator = TryGetComponent(out _anim);
   }

    // Update is called once per frame
    void Update()
    {
      _hasAnimator = TryGetComponent(out _anim);
      if (Vector3.Distance(goal.position, transform.position) < attackSpace)
      {
         attack = true;
         _anim.SetBool(_animIDRun, true);
         if (attack == true)
         {
            if (Vector3.Distance(goal.position, transform.position) >= SpaceBetween)
            {
               Vector3 direction = goal.position - transform.position;
               transform.Translate(direction * 1.5f * Time.deltaTime);
               transform.LookAt(goal);
            }
         }
      }
      else
      {
         attack = false;
         _anim.SetBool(_animIDRun, false);
      }
   }
}

