using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class fighter : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject target;


    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();

    }

    
  

    private void OnCollisionEnter(Collision collision)
    {
       /* if (collision.gameObject.tag == "Player" && collision.transform.GetComponent<fighter>().isKiller == true && this.isKiller != true)*/
       if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }

}
