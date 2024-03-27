using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUsing : MonoBehaviour
{
    [SerializeField] private int currentSlotId;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerInfo playerInfo;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseHeal();
        }
    }
    void UseHeal()
    {
        inventory = transform.GetComponent<Inventory>();
        playerInfo = transform.GetComponent<PlayerInfo>();
        currentSlotId = transform.GetComponent<WeaponSwitch>().GetCurrentSlotIndex();
        if (inventory.GetSlotId(currentSlotId) != 0 && inventory.GetItem(currentSlotId).HealItem)
        {

            var item = inventory.GetItem(currentSlotId);
            if (playerInfo.currentHealth < playerInfo.maxHealth)
            {
                playerInfo.currentHealth += item.HPtoHeal;
                if (playerInfo.currentHealth > playerInfo.maxHealth)
                    playerInfo.currentHealth = playerInfo.maxHealth;
                inventory.DeleteItems(currentSlotId, 1);
            }

        }
    }
}
