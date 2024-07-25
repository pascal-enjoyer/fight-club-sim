using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private GameObject character;

    private LayerMask whatIsGround;
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("ты наступил в кровь");
    }
}
