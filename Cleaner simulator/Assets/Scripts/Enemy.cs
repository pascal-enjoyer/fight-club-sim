using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float radius = 20;
    private float distance;
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance >= radius)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().SetBool("StayBool", true);
            GetComponent<Animator>().SetBool("RunBool", false);
        }
        if (distance < radius & distance > 5)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().destination = player.position;

            GetComponent<Animator>().SetBool("StayBool", false);
            GetComponent<Animator>().SetBool("RunBool", true);
            GetComponent<Animator>().SetBool("WalkBool", false);
        }
        if (distance <= 5 & distance > 2)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().destination = player.position;

            GetComponent<Animator>().SetBool("RunBool", false);
            GetComponent<Animator>().SetBool("WalkBool", true);
            GetComponent<Animator>().SetBool("HitBool", false);
        }
        if (distance <= 2)
        {
            transform.LookAt(player);
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().SetBool("HitBool", true);
            GetComponent<Animator>().SetBool("WalkBool", false);
        }
    }
}