using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public Item item;

    public WeaponItem(Item item)
    {
        this.item = item;
    }
}
