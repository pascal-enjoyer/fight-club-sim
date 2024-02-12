using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    
    [SerializeField] private List<Sprite> icons = new List<Sprite>();

    public void UpdateUI(Inventory inventory)
    {
        for (int i = 0; i < inventory.GetSize() && i < icons.Count; i++) 
        {
            icons[i] = inventory.GetItem(i).icon;
            
        }
    }
}
