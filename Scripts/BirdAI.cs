using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
   public Transform goal;
   public float SpaceBetween = 0.2f;
   public float attackSpace = 10f;
   public bool follow = false;
   public Animator _anim;
   private int _animIDAttack;
   private bool _hasAnimator;

   public StarterAssets.ThirdPersonController playerController;
   // Start is called before the first frame update
   void Start()
   {
      _anim = GetComponent<Animator>();
      _animIDAttack = Animator.StringToHash("Attack");
      _hasAnimator = TryGetComponent(out _anim);
      playerController = GameObject.Find("PlayerVee").GetComponent<StarterAssets.ThirdPersonController>();
   }


   // Update is called once per frame
   void Update()
   {
      _hasAnimator = TryGetComponent(out _anim);
      if (Vector3.Distance(goal.position, transform.position) < attackSpace)
      {
         follow = true;
         
         if (follow == true)
         {

            playerController.animeState = 1;
               Vector3 direction = goal.position - transform.position;
               transform.Translate(direction * 1.5f * Time.deltaTime);

               
               
                transform.LookAt(goal);
              if (Vector3.Distance(goal.position, transform.position) <= SpaceBetween)
            {
               _anim.SetBool(_animIDAttack, true);
            }
            else
            {
               _anim.SetBool(_animIDAttack, false);
            }
         }
      }
      else
      {
         follow = false;
         //playerController.animeState = 0;

      }
   }

  
}
