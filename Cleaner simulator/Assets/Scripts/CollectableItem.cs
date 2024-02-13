using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(BoxCollider))]
public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] public int amount = 1;


}
