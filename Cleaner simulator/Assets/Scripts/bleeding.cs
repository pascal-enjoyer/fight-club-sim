
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bleeding : MonoBehaviour
{

    public float bleedingTimeInSec = 5f;  // Время кровотечения в секундах
    public GameObject bloodPrefab;  // Префаб объекта крови
    private bool isBleeding = false;
    private Coroutine bleedCoroutine;
    [SerializeField] private float maxBloodSize = 4f;

    public void StartBleed()
    {
        if (bleedCoroutine != null)
        {
            StopCoroutine(bleedCoroutine);  // Остановить предыдущую корутину, если она была запущена
        }

        bleedCoroutine = StartCoroutine(BleedRoutine());
    }

    private IEnumerator BleedRoutine()
    {
        isBleeding = true;
        float currentBleedTime = 0f;

        while (currentBleedTime <= bleedingTimeInSec)
        {
            // Создаем объект крови каждые 0.5 секунды
            if (currentBleedTime % 0.5f < Time.deltaTime)
            {
                SpawnBlood();
            }

            currentBleedTime += Time.deltaTime;
            yield return null;
        }

        isBleeding = false;
    }

    private void SpawnBlood()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.CompareTag("BloodPool")) // предполагаем, что у лужи крови есть тег "BloodPool"
            {
                if (hit.collider.transform.localScale.z < maxBloodSize)
                    hit.collider.gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            }
            else
                Instantiate(bloodPrefab, hit.point, Quaternion.identity);
        }
    }
}
