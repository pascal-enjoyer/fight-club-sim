using TMPro;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    public string CollectingText = "Collect ";
    public TextMeshProUGUI PressEToCollect;
    
    public void ShowCollectingItemText()
    {
        GameObject hittedObjectd = transform.GetComponent<RayCast>().GetHittedObject();
        string objName = hittedObjectd.GetComponent<CollectableItem>().item.itemName;
        PressEToCollect.text = CollectingText + objName;
    }

    public void HideCollectingItemText()
    {
        PressEToCollect.text = "";
    }

}