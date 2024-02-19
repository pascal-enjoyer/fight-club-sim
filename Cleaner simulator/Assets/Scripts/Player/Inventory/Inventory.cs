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

    public InventorySlot()
    {
        item = null;
        cnt = 0;
    }
    public InventorySlot(Item item, int cnt)
    {
        this.item = item;
        this.cnt = cnt;
    }
}
public class Inventory : MonoBehaviour
{
    [SerializeField] public InventorySlot[] items;
    [SerializeField] public int size = 4;
    [SerializeField] public UnityEvent OnInventoryChanged;

    private void Start()
    {
        items = new InventorySlot[size];
    }
    public bool AddItems(Item item, int cnt)
    {
        int currentSlotId = transform.GetComponent<WeaponSwitch>().currentSlot;
        if (items[currentSlotId] != null && items[currentSlotId].cnt != 0) {
            if (items[currentSlotId].item.id == item.id)
            {
                if (items[currentSlotId].cnt + cnt <= item.maxCountInInventory) {
                    items[currentSlotId].cnt += cnt;
                    OnInventoryChanged.Invoke();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else //тут можно будет добавить чтоб айтем брался в другой слот если выбранный занят
            {
                return false;
            }
        }
        else
        {
            items[currentSlotId] = new InventorySlot(item, cnt);
            OnInventoryChanged.Invoke();
            return true;
        }
    }

    public bool DeleteItems(int i, int cnt)
    {
        if (items[i] != null && items[i].cnt != 0)
        {
            items[i].cnt -= cnt;
            if (items[i].cnt <= 0)
            {
                items[i] = null;
            }
            OnInventoryChanged.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetSlotId(int i)
    {
        if (items[i] != null && items[i].cnt != 0)
        {
            return items[i].item.id;
        }
        return 0;
    }

    public int GetCount(int i)
    {
        if (items[i] != null && items[i].cnt != 0)
            return items[i].cnt;
        return 0;
    }

    public Item GetItem(int i)
    {
        if (items[i] != null && items[i].cnt != 0)
            return items[i].item;
        return null;
    }

    public int GetSize()
    {
        int k = 0;
        for (int i = 0; i < size;i++)
        {
            if (items[i] != null && items[i].cnt != 0)
            {
                k++;
            }
        }
        return k;
    }
    


}
