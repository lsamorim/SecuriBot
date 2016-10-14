using UnityEngine;
using System.Collections;

public class VrPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform bag, hand;

    [SerializeField]
    private float holdTimeToStorableItem = 1.5f;

    private BasicItem currentHandleItem;

    private bool tryRelease = false;

	void Start()
	{
	    
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(currentHandleItem)
            {
                tryRelease = true;
            }
        }
        if(Input.GetMouseButtonUp(0) && tryRelease)
        {
            tryRelease = false;
            StopCoroutine("TryReleaseCurrentItem");
            StartCoroutine("TryReleaseCurrentItem");
        }
    }

    public void OnGazeItemEnter(BasicItem item)
    {
        ResetGazeActions();

    }
    public void OnGazeItemDown(BasicItem item)
    {
        ResetGazeActions();

        if (item.IsStorable)
            StartCoroutine("TryStoreItem", item);
    }
    public void OnGazeItemClick(BasicItem item)
    {
        if (currentHandleItem)
            return;
        ResetGazeActions();

        currentHandleItem = item;
        currentHandleItem.AttachTo(hand);
    }

    private void ResetGazeActions()
    {
        StopCoroutine("TryStoreItem");
        StopCoroutine("TryReleaseCurrentItem");
        tryRelease = false;
    }

    private IEnumerator TryReleaseCurrentItem()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        if (currentHandleItem)
        {
            currentHandleItem.Release();
            currentHandleItem = null;
        }
    }
    private IEnumerator TryStoreItem(BasicItem item)
    {
        yield return new WaitForSeconds(holdTimeToStorableItem);
        item.AttachTo(bag);
    }
}
