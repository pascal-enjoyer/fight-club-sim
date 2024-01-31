using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    
    public float radius = 20f;
    private float distance;
    void Update()
    {
        distance = Vector3.Distance(player.position, enemy.transform.position);
        if (distance >= radius)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            enemy.GetComponent<Animator>().SetBool("StayBool", true);
            enemy.GetComponent<Animator>().SetBool("RunBool", false);
        }
        if (distance < radius & distance > 5)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().destination = player.position;
            enemy.GetComponent<Animator>().SetBool("StayBool", false);
            enemy.GetComponent<Animator>().SetBool("RunBool", true);
            enemy.GetComponent<Animator>().SetBool("WalkBool", false);
        }
        if (distance <= 5 & distance > 1.5)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().destination = player.position;

            enemy.GetComponent<Animator>().SetBool("RunBool", false);
            enemy.GetComponent<Animator>().SetBool("WalkBool", true);
            enemy.GetComponent<Animator>().SetBool("HitBool", false);
        }
        if (distance <= 1.5)
        {
            // Передача координат X и Z в позицию игрока
            Vector3 playerPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(playerPosition);

            GetComponent<NavMeshAgent>().enabled = false;
            enemy.GetComponent<Animator>().SetBool("HitBool", true);
            enemy.GetComponent<Animator>().SetBool("WalkBool", false);
        }
    }
}