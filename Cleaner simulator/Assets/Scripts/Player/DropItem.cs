using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropItem : MonoBehaviour
{
    public int currentSlotId;
    public List<CollectableItem> ItemPrefabs = new List<CollectableItem>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DropItems();
        }
    }

    private void DropItems()
    {

        currentSlotId = transform.GetComponent<WeaponSwitch>().currentSlot;
        var inventory = transform.GetComponent<Inventory>();
        try
        {
            var item = inventory.GetItem(currentSlotId);
            foreach (CollectableItem t in ItemPrefabs)
            {
                if (t.item.id == item.id) {
                    Instantiate(t, transform.position += transform.forward * 2f + transform.up * 1f, Quaternion.Euler(transform.rotation.eulerAngles));
                }
            }
        }
        catch {
            
        }
        inventory.DeleteItems(currentSlotId, 1);

    }

}
