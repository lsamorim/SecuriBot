using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VRCanvas : MonoBehaviour
{
    [SerializeField]
    private Text itemInfoText;
    private string itemInfo;

    public void SetItemInfo(BasicInteractableItem item)
    {
        StopAllCoroutines();
        if (item == null)
            StartCoroutine("EmptyItemInfo");
        else
        {
            itemInfo = item.ItemName + "\n" + item.ItemDescription;
            StartCoroutine("FillItemInfo");
        }
    }

    private IEnumerator EmptyItemInfo()
    {
        int dotCount = 0;
        itemInfoText.text = string.Empty;
        while (true)
        {
            dotCount++;
            itemInfoText.text += ".";
            yield return new WaitForSeconds(1f);

            if (dotCount == 3)
            {
                dotCount = 0;
                itemInfoText.text = string.Empty;
            }
        }
    }

    private IEnumerator FillItemInfo()
    {
        itemInfoText.text = string.Empty;
        for (int i = 0, textCount = itemInfo.Length; i < textCount; i++)
        {
            itemInfoText.text += itemInfo.Substring(i, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
