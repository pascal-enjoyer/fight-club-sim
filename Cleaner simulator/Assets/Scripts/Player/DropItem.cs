using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropItem : MonoBehaviour
{
    public int currentSlotId;
    public float dropDistance = 2f;
    public float dropHeight = 1f;
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
        if (inventory.GetSlotId(currentSlotId) != 0)
        {
            var item = inventory.GetItem(currentSlotId);
            foreach (CollectableItem t in ItemPrefabs)
            {
                if (t.item.id == item.id)
                {
                    Instantiate(t, transform.position + transform.forward * dropDistance + new Vector3(0, dropHeight, 0), Quaternion.Euler(transform.rotation.eulerAngles));
                }
            }
            inventory.DeleteItems(currentSlotId, 1);
        }

    }

}
