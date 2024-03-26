using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float currentDamage;
    [SerializeField] private float maxDamage;

    [SerializeField] private bool isDead;

    public void TakeDamage(float receivingDamage)
    {
        if (currentHealth - receivingDamage > 0)
        {
            currentHealth -= receivingDamage;
        }
        else
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        isDead = true;

    }
    


}
