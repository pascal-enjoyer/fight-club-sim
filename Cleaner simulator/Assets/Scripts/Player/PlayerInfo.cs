using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    [Range(0f, 1000f)] public float maxHealth = 100f;

    [Range(0f, 1000f)] public float currentHealth = 100f;

    public bool TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                return false;
            }
            return true;
        }
        return false;
    }
}
