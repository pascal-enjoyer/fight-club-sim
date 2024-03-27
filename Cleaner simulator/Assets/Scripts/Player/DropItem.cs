using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropItem : MonoBehaviour
{
    [SerializeField] private int currentSlotId;
    [SerializeField] private float dropDistance = 2f;
    [SerializeField] private float dropHeight = 1f;

    [SerializeField] public List<CollectableItem> ItemPrefabs = new List<CollectableItem>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DropItems();
        }
    }

    private void DropItems()
    {

        currentSlotId = transform.GetComponent<WeaponSwitch>().GetCurrentSlotIndex();

        Inventory inventory = transform.GetComponent<Inventory>();

        if (inventory.GetSlotId(currentSlotId) != 0)
        {
            Item item = inventory.GetItem(currentSlotId);

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
