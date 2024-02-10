using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Transform player;

    PlayerInfo playerInfo;

    void Start()
    {
        healthBar = GetComponent<Image>();
        playerInfo = player.GetComponent<PlayerInfo>();
    }

    void Update()
    {
        healthBar.fillAmount = playerInfo.currentHealth / playerInfo.maxHealth;
    }
}
