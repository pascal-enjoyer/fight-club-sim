using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RayCast : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private UnityEvent OnRayHitCollectableItem;
    [SerializeField] private UnityEvent OnRayHitEnemy;

    [SerializeField] private Ray ray;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        CheckRayCast();
    }

    private void CheckRayCast()
    {
        ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<CollectableItem>() != null) 
            {
                OnRayHitCollectableItem.Invoke();
            }
            if (hit.transform.gameObject.GetComponent<EnemyInfo>() != null)
            {
                OnRayHitEnemy.Invoke();
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red);

    }/*

    private void ShowCollectingText(string objName)
    {
    }

    private void HideCollectingText()
    {
        CollectingText.text = "";
    }*/
}


            /*
                ShowCollectingText(hit.transform.GetComponent<CollectableItem>().item.itemName);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (inventory.AddItems(hit.transform.gameObject.GetComponent<CollectableItem>().item, hit.transform.gameObject.GetComponent<CollectableItem>().count))
                        Destroy(hit.transform.gameObject);
                }*/