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
                ShowCollectingText(hit.transform.GetComponent<Item>().itemName); // тут ошибка
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    var inventory = transform.GetComponent<Inventory>();
                    if (inventory.AddItems(hit.transform.gameObject.GetComponent<CollectableItem>().GetComponent<Item>(), hit.transform.gameObject.GetComponent<CollectableItem>().amount))
                        Destroy(hit.transform.gameObject);
                    else
                    {
                    }
                }
            }
            else
            {
                HideCollectingText();
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
