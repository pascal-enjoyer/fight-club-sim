using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollecting : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    [SerializeField] private Item item;
    [SerializeField] private int count;
    [SerializeField] private GameObject hit;
    [SerializeField] private CollectableItem collectableItemComponent;
    [SerializeField] private UnityEvent OnItemCollected;


    public void CollectItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            hit = transform.GetComponent<RayCast>().GetHittedObject();

            
            collectableItemComponent = hit.transform.gameObject.GetComponent<CollectableItem>(); 
            item = collectableItemComponent.item;
            count = collectableItemComponent.count;

            if (inventory.AddItems(item, count))
            {
                Destroy(hit);
            }
        }
    }
}
