using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            transform.GetComponent<PlayerInfo>().TakeDamage(other.GetComponent<Enemy>().Damage);
        }
    }
}
