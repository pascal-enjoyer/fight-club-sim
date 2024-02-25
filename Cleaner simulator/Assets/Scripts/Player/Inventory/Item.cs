using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int id;
    public Sprite icon;
    public string itemName;
    public int maxCountInInventory;
    public bool HealItem = false;
    [Range(0, 100f)] public int HPtoHeal = 0;
    public bool Weapon = false;
    [Range(0, 100f)] public int Damage = 0;
    public bool Throwable = false;
    public bool bigItem = false;
    public bool smallItem = false;

}
