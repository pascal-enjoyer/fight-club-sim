using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int cnt;
    public InventorySlot(Item item, int cnt)
    {
        this.item = item;
        this.cnt = cnt;
    }
}
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>();
    [SerializeField] private int size = 4;
    [SerializeField] public UnityEvent OnInventoryChanged;


    public bool AddItems(Item item, int cnt = 1)
    {
        foreach (InventorySlot slot in items)
        {
            if (slot.item.id == item.id)
            {
                if (item.maxCountInInventory >= cnt + slot.cnt)
                {
                    slot.cnt += cnt;
                    OnInventoryChanged.Invoke();
                    return true;
                }
            }
        }

        if (size <= items.Count)
        {
            return false;
        }
        InventorySlot newSlot = new InventorySlot(item, cnt);
        
        items.Add(newSlot);
        OnInventoryChanged.Invoke();
        return true;
        
    }
    public bool DeleteItems(int currentSlotId, int cnt = 1)
    {
        if (items.Count > currentSlotId && items[currentSlotId].cnt >= cnt)
        {
            items[currentSlotId].cnt -= cnt;

            OnInventoryChanged.Invoke();
            if (items[currentSlotId].cnt == 0)
            {
                items.RemoveAt(currentSlotId);
                items.RemoveAll(s => s == null);
                OnInventoryChanged.Invoke();
            }
            return true;
        }
        else
        {
            return false;
        }
    } //персонажи для того чтобы опхилиться будут есть мыло
    public Item GetItem(int i) {
        return i < items.Count ? items[i].item : null;
    }
    public int GetCount (int i)
    {
        return i < items.Count ? items[i].cnt : 0;
    }
    public int GetSize()
    {
        return items.Count;
    }
    public int GetSlotId(int i)
    {
        if (i < items.Count && items[i] != null)
        {
            return items[i].item.id;
        }
        else return 0;
    }
    
}
