using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour
{

    [Range(0f, 1000f)] public float MaxHealth = 100f;

    [Range(0f, 1000f)] public float CurrentHealth = 100f;

    [SerializeField] private UnityEvent OnHealthEqualsZero;
    
    public void TakeDamage(float receivingDamage)
    {
        if (CurrentHealth - receivingDamage > 0)
        {
            CurrentHealth -= receivingDamage;
        }
        else
        {
            CurrentHealth = 0;
        }
    }

    public void TakeHeal(float receivingHeal)
    {
        if (CurrentHealth +  receivingHeal > MaxHealth) 
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += receivingHeal;
        }
    }

    public int GetPlayerHpInPercents()
    {
        return (int)MaxHealth / (int)CurrentHealth;
    }
}
