using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
   public float speed;
   private float _waitTime;
   public float startWaitTime;

   public Transform[] moveSpot;
   private int randomSpot;
   public float minX;
   public float maxX;
   public float minZ;
   public float maxZ;

    // Start is called before the first frame update
    void Start()
    {
      _waitTime = startWaitTime;
      randomSpot = Random.Range(0, moveSpot.Length);
      //moveSpot.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
    }

    // Update is called once per frame
    void Update()
    {
      transform.position = Vector3.MoveTowards(transform.position, moveSpot[randomSpot].position, speed * Time.deltaTime);

      transform.forward = transform.position;

      if (Vector3.Distance(transform.position, moveSpot[randomSpot].position) < 0.2f)
      {
         if (_waitTime <= 0)
         {
            randomSpot = Random.Range(0, moveSpot.Length);
            _waitTime = startWaitTime;
         }
         else
         {
            _waitTime -= Time.deltaTime;
         }
      }
    }
}
