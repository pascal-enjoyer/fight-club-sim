
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bleeding : MonoBehaviour
{

    public float bleedingTimeInSec = 5f;  // ����� ������������ � ��������
    public GameObject bloodPrefab;  // ������ ������� �����
    private bool isBleeding = false;
    private Coroutine bleedCoroutine;
    [SerializeField] private float maxBloodSize = 4f;

    public void StartBleed()
    {
        if (bleedCoroutine != null)
        {
            StopCoroutine(bleedCoroutine);  // ���������� ���������� ��������, ���� ��� ���� ��������
        }

        bleedCoroutine = StartCoroutine(BleedRoutine());
    }

    private IEnumerator BleedRoutine()
    {
        isBleeding = true;
        float currentBleedTime = 0f;

        while (currentBleedTime <= bleedingTimeInSec)
        {
            // ������� ������ ����� ������ 0.5 �������
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
            if (hit.collider.CompareTag("BloodPool")) // ������������, ��� � ���� ����� ���� ��� "BloodPool"
            {
                if (hit.collider.transform.localScale.z < maxBloodSize)
                    hit.collider.gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            }
            else
                Instantiate(bloodPrefab, hit.point, Quaternion.identity);
        }
    }
}
