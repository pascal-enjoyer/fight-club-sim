using UnityEngine;

public class TypesOfItemUsage : MonoBehaviour
{
    public void UseHealItem(Item item)
    {
        PlayerInfo playerInfo = GetComponent<PlayerInfo>();
        playerInfo.TakeHeal(item.healCount);
    }

    
    public void UseThrowableItem(Item item)
    {
        GetComponent<ItemThrow>().Throw();
    }
}
