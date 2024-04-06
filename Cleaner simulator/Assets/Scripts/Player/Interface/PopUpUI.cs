using TMPro;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    public string CollectingText = "Collect ";
    public TextMeshProUGUI PressEToCollect;
    
    public void UpdateCollectingText(bool isHitted)
    {
        switch (isHitted)
        {
            case true:
                ShowCollectingItemText();
                break;
            case false:
                HideCollectingItemText();
                break;
        }
    }

    private void ShowCollectingItemText()
    {
        GameObject hittedObjectd = GetComponent<RayCast>().GetHittedObject();
        string objName = hittedObjectd.GetComponent<CollectableItem>().item.itemName;
        PressEToCollect.text = CollectingText + objName;
    }

    private void HideCollectingItemText()
    {
        PressEToCollect.text = "";
    }

}