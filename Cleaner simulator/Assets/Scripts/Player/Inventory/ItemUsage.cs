using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ItemUsage : MonoBehaviour
{
    [Header("Main Item Usage Settings")]

    [SerializeField] private KeyCode itemUseKey = KeyCode.Mouse0;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Item item;
    [SerializeField] private int currentSlotIndex;

    [Header("Mop item usage")]


    [SerializeField] private float mopUsageCooldown;
    [SerializeField] private bool readyToUseMop = true;
    [SerializeField] private float mopUsageDistance;

    [SerializeField] private float mopPushForce;

    [Header("Heal Item Settings")]

    [SerializeField] private PlayerInfo playerInfo;

    [Header("Throwable Item Settings")]

    public Transform cam;
    public Transform objectStartPoint;
    [SerializeField] private GameObject objectToThrow;

    [Header("Throw settings")]

    public float throwCooldown;
    public float throwForce;
    public float throwUpwardForce;
    [SerializeField] bool readyToThrow = true;

    //распознать какой айтем € юзаю, по куррент слот индексу и проверке нажати€ клавиши!!

    private void Update()
    {
        if (Input.GetKeyDown(itemUseKey))
        {
            inventory = GetComponent<Inventory>();
            playerInfo = GetComponent<PlayerInfo>();
            currentSlotIndex = GetComponent<WeaponSwitch>().GetCurrentSlotIndex();
            item = inventory.GetItem(currentSlotIndex);
            if (item != null)
            {
                switch (item.type)
                {
                    case ItemType.Heal:
                        UseHealItem();
                        break;
                    case ItemType.Mop:
                        UseMopItem();
                        break;
                    case ItemType.Throwable:
                        UseThrowItem();
                        break;
                }
            }
        }
    }

    public void UseHealItem()
    {
        if (playerInfo.TakeHeal(item.healCount))
        {
            inventory.DeleteItems(currentSlotIndex, 1);
        }
        else
        {
            Debug.Log("’п полон, нечего хилить");
        }
    }

    public void UseThrowItem()
    {
        if (inventory.GetSlotId(GetComponent<WeaponSwitch>().GetCurrentSlotIndex()) != 0 && item.type == ItemType.Throwable && readyToThrow)
        {
            foreach (CollectableItem t in GetComponent<DropItem>().ItemPrefabs)
            {
                if (t.item.id == item.id)
                {
                    objectToThrow = t.gameObject;
                }
            }
            inventory.DeleteItems(GetComponent<WeaponSwitch>().GetCurrentSlotIndex(), 1);
            readyToThrow = false;
            GameObject projectile = Instantiate(objectToThrow, objectStartPoint.position, cam.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            Vector3 forceDirection = cam.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
            {
                forceDirection = (hit.point - objectStartPoint.position).normalized;
            }
            Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
            projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
            Invoke(nameof(ResetThrow), throwCooldown);
        }
    }

    public void UseMopItem()
    {
        if (inventory.GetSlotId(GetComponent<WeaponSwitch>().GetCurrentSlotIndex()) != 0 && item.type == ItemType.Mop)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, mopUsageDistance))
            {
                if (hit.collider.GetComponent<Blood>())
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            Invoke(nameof(ResetMop), mopUsageCooldown);
        }
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }


    private void ResetMop()
    {
        readyToUseMop = true;
    }
}
