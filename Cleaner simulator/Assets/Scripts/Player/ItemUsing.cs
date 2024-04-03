using UnityEngine;
using UnityEngine.Events;

public class ItemUsing : MonoBehaviour
{
    [SerializeField] private int currentSlotId;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private UnityEvent OnHealItemUsed;
    // ������� ������ � ������������ ��� ������� �������� (� ������� �������� ���� ������������)
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
            Item item = inventory.GetItem(currentSlotId);
            playerInfo.TakeHeal(item.HPtoHeal);
        }
    }
}
