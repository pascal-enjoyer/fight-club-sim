using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;



public class WeaponSwitch : MonoBehaviour
{
    public Transform Hand;
    public int itemId;
    public int currentSlot;
    public Transform bigSlotOnBody;
    public Transform leftSmallSlotOnBody;
    public Transform rightSmallSlotOnBody;
    public List<GameObject> weaponPrefabs = new List<GameObject>();
    
    
    [SerializeField] public UnityEvent OnWeaponSwitched;
    public void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var inventory = transform.GetComponent<Inventory>();
            itemId = inventory.GetSlotId(0);
            currentSlot = 0;
            OnWeaponSwitched.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var inventory = transform.GetComponent<Inventory>();
            itemId = inventory.GetSlotId(1);
            currentSlot = 1;
            OnWeaponSwitched.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var inventory = transform.GetComponent<Inventory>();
            itemId = inventory.GetSlotId(2);
            currentSlot = 2;
            OnWeaponSwitched.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            var inventory = transform.GetComponent<Inventory>();
            itemId = inventory.GetSlotId(3);
            currentSlot = 3;
            OnWeaponSwitched.Invoke();
        }

        SelectWeapon();
    }
    public void SelectWeapon()
    {
        var inventory = transform.GetComponent<Inventory>();
        itemId = inventory.GetSlotId(currentSlot);
        if (itemId == 0)
        {
            foreach (Transform weapon in Hand)
            {
                weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform weapon in Hand)
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
        for (int i = 0; i < inventory.size; i++)
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
        // если предмет в инвентаре и не в руке, то проверить 
    }
}
