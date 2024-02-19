using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSwitch : MonoBehaviour
{
    public Transform Hand;
    public int itemId;
    public int currentSlot;

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
            return;
        }
        foreach (Transform weapon in Hand) 
        {
            if (itemId == weapon.GetComponent<WeaponItem>().item.id)
            {
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
        }
    }
}
