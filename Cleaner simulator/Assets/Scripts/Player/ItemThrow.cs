using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{

    [Header("Objects")]
    public Transform cam;
    public Transform objectStartPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {

        readyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    public void Throw()
    {
        Inventory inventory = transform.GetComponent<Inventory>();
        if (inventory.GetSlotId(transform.GetComponent<WeaponSwitch>().GetCurrentSlotIndex()) != 0 && inventory.GetItem(transform.GetComponent<WeaponSwitch>().GetCurrentSlotIndex()).type == ItemType.Throwable)
        {
            var item = inventory.GetItem(transform.GetComponent<WeaponSwitch>().GetCurrentSlotIndex());
            foreach (CollectableItem t in transform.GetComponent<DropItem>().ItemPrefabs)
            {
                if (t.item.id == item.id)
                {
                    objectToThrow = t.gameObject;
                }
            }
            inventory.DeleteItems(transform.GetComponent<WeaponSwitch>().GetCurrentSlotIndex(), 1);

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

            totalThrows--;

            Invoke(nameof(ResetThrow), throwCooldown);
        }
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
