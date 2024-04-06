using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RayCast : MonoBehaviour
{
    public Camera MainCamera;
    [SerializeField] private UnityEvent<bool> OnRayHitCollectableItem;
    [SerializeField] private UnityEvent OnRayHitEnemy;
    [SerializeField] private RaycastHit hit;
    private GameObject hitGameObject;
    [SerializeField] private Ray ray;

    private void Start()
    {
    }

    void Update()
    {
        CheckRayCast();
    }

    private void CheckRayCast()
    {
        ray = MainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit))
        {
            hitGameObject = hit.transform.gameObject;
            if (hitGameObject.GetComponent<CollectableItem>() != null)
            {
                OnRayHitCollectableItem.Invoke(true);
            }
            else
                OnRayHitCollectableItem.Invoke(false);
            if (hitGameObject.GetComponent<EnemyInfo>() != null)
            {
                OnRayHitEnemy.Invoke();
            }
            
        }
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red);

    }

    public GameObject GetHittedObject()
    {
        return hitGameObject;
    }
}

// луч идет, когда попал на 