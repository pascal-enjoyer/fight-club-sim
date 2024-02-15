using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Transform Hand;
    public void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon; 
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
        {
            if (selectedWeapon >= Hand.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = Hand.childCount - 1;
            }
            else selectedWeapon--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            var inventory = transform.GetComponent<Inventory>();
            if (inventory.IsEmptySlot(0))
                selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var inventory = transform.GetComponent<Inventory>();
            if (inventory.IsEmptySlot(1))
                selectedWeapon = 1;
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in Hand) 
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
