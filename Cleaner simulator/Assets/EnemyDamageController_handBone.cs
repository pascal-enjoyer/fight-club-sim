using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamageController_handBone : MonoBehaviour
{
    public Transform Enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInfo>() != null)
        {
            other.GetComponent<PlayerInfo>().currentHealth -= Enemy.GetComponent<Enemy>().Damage;
        }
    }
}
