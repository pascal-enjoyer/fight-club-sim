using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    
    [SerializeField] private List<Image> iconsOfItems = new List<Image>();
    [SerializeField] private List<Text> counts = new List<Text>();

    public void UpdateUI(Inventory inventory)
    {
        for (int i = 0; i < inventory.GetSize() && i < iconsOfItems.Count; i++) 
        {
            iconsOfItems[i].color = new Color(1, 1, 1, 1);
            iconsOfItems[i].sprite = inventory.GetItem(i).icon;
            counts[i].text = inventory.GetCount(i) > 1 ? inventory.GetCount(i).ToString() : "";
            
        }
        for (int i = inventory.GetSize(); i < iconsOfItems.Count; i++)
        {
            iconsOfItems[i].color = new Color(1, 1, 1, 0);
            iconsOfItems[i].sprite = null;
            counts[i].text = "";
        }
    }
}
