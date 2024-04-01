using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;



public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private Transform handWithWeapons;

    [SerializeField] private int itemId;
    [SerializeField] private int currentSlot;

/*    public Transform bigSlotOnBody;
    public Transform leftSmallSlotOnBody;
    public Transform rightSmallSlotOnBody;*/

    [SerializeField] private bool slotIsEmpty;

    public List<GameObject> weaponPrefabs = new List<GameObject>();
    
    [SerializeField] public UnityEvent OnWeaponSwitched;

    [SerializeField] private Inventory inventory;

    public void Start()
    {
        inventory = transform.GetComponent<Inventory>();
        SelectWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnInventoryKeysDown(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnInventoryKeysDown(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnInventoryKeysDown(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnInventoryKeysDown(3);
        }
    }

    private void OnInventoryKeysDown(int keyCode)
    {
        itemId = inventory.GetSlotId(keyCode);
        currentSlot = keyCode;
        OnWeaponSwitched.Invoke();
        SelectWeapon();
    }

    public void SelectWeapon()
    {
        itemId = inventory.GetSlotId(currentSlot);
        if (itemId == 0)
        {
            foreach (Transform weapon in handWithWeapons)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        else
        {
            foreach (Transform weapon in handWithWeapons)
            {
                if (itemId == weapon.GetComponent<WeaponItem>().item.id)
                {
                    weapon.gameObject.SetActive(true);

                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }

        // если предмет в инвентаре и не в руке, то проверить 
    }
/*
    public void PlaceUnactiveWeaponsOnBody()
    {
        for (int i = 0; i < inventory.GetSize(); i++)
        {
            Item itemInInventory = inventory.GetItem(i);
            if (itemInInventory != null)
            {
                if (itemInInventory.id != itemId && bigSlotOnBody.childCount == 0 && itemInInventory.bigItem)
                {
                    Instantiate(weaponPrefabs.Find(x => x.GetComponent<WeaponItem>().item.id == inventory.GetSlotId(i)).gameObject, bigSlotOnBody);
                }
                else if (itemInInventory.id == itemId && bigSlotOnBody.childCount != 0 && bigSlotOnBody.GetChild(0).GetComponent<WeaponItem>().item.id == itemInInventory.id)
                {
                    Destroy(bigSlotOnBody.GetChild(0).gameObject);
                }
                if (itemInInventory.id != itemId && leftSmallSlotOnBody.childCount == 0 && itemInInventory.smallItem)
                {
                    if (!(rightSmallSlotOnBody.childCount != 0 && rightSmallSlotOnBody.GetChild(0).GetComponent<WeaponItem>().item.id == itemInInventory.id))
                    {
                        Instantiate(weaponPrefabs.Find(x => x.GetComponent<WeaponItem>().item.id == inventory.GetSlotId(i)).gameObject, leftSmallSlotOnBody);
                    }
                }
                else if (itemInInventory.id == itemId && leftSmallSlotOnBody.childCount != 0 && leftSmallSlotOnBody.GetChild(0).GetComponent<WeaponItem>().item.id == itemInInventory.id)
                {
                    Destroy(leftSmallSlotOnBody.GetChild(0).gameObject);
                }

                if (itemInInventory.id != itemId && rightSmallSlotOnBody.childCount == 0 && itemInInventory.smallItem)
                {
                    if (!(leftSmallSlotOnBody.childCount != 0 && leftSmallSlotOnBody.GetChild(0).GetComponent<WeaponItem>().item.id == itemInInventory.id))
                    {
                        Instantiate(weaponPrefabs.Find(x => x.GetComponent<WeaponItem>().item.id == inventory.GetSlotId(i)).gameObject, rightSmallSlotOnBody);
                    }
                }
                else if (itemInInventory.id == itemId && rightSmallSlotOnBody.childCount != 0 && rightSmallSlotOnBody.GetChild(0).GetComponent<WeaponItem>().item.id == itemInInventory.id)
                {
                    Destroy(rightSmallSlotOnBody.GetChild(0).gameObject);
                }
            }
        }
    }*/

    public int GetCurrentSlotIndex()
    {
        return currentSlot;
    }
}
