using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviour
{
    public TextMeshProUGUI collectingText;
    public Camera mainCamera;

    void Update()
    {
        RayCastInfo();
    }
    private void RayCastInfo()
    {

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<CollectableItem>() != null) 
            {
                ShowCollectingText(hit.transform.GetComponent<CollectableItem>().item.itemName);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    var inventory = transform.GetComponent<Inventory>();
                    if (inventory.AddItems(hit.transform.gameObject.GetComponent<CollectableItem>().item, hit.transform.gameObject.GetComponent<CollectableItem>().count))
                        Destroy(hit.transform.gameObject);
                }
            }
            else
            {
                HideCollectingText();
            }
            if (hit.transform.gameObject.GetComponent<Enemy>() != null)
            {

                Inventory inventory = transform.GetComponent<Inventory>();
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                WeaponSwitch currentWeapon = transform.GetComponent<WeaponSwitch>();
                if (Input.GetKeyDown(KeyCode.Mouse0) && inventory.GetItem(currentWeapon.currentSlot).Weapon)
                {
                    enemy.Currenthealth -= inventory.GetItem(currentWeapon.currentSlot).Damage;
                    if (enemy.Currenthealth <= 0 )
                    {
                        Destroy(enemy.gameObject);
                    }


                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red);
        
    }
    private void ShowCollectingText(string objName)
    {
        collectingText.text = $"Press E to collect {objName}";
    }
    private void HideCollectingText()
    {
        collectingText.text = "";
    }
    
}
