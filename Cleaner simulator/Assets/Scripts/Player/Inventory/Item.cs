using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int id;
    public Sprite icon;
    public string itemName;
    public int maxCountInInventory;

    public int healCount;
    public int damageCount;

    public ItemType type;
    public UnityEvent<Item> OnItemUsed;
    

}

public enum ItemType
{
    Mop,
    Heal,
    Throwable
}
