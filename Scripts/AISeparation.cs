﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISeparation : MonoBehaviour {

    GameObject[] AI;
    public float SpaceBetween = 1.5f;

	void Start () {
        AI = GameObject.FindGameObjectsWithTag("Enemy");
	}

	void Update () {

      if (DealDamageComponent.enemyDeath == true)
      {
         Destroy(this);
      }
		foreach(GameObject go in AI)
        {
         if (AI != null)
         {
            if (go != gameObject)
            {
               float distance = Vector3.Distance(go.transform.position, this.transform.position);
               if (distance <= SpaceBetween)
               {
                  Vector3 direction = transform.position - go.transform.position;
                  transform.Translate(direction * Time.deltaTime);
               }
            }
         }
        }
	}
}
