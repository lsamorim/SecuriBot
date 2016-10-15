﻿using UnityEngine;
using System.Collections;

public class VrPlayer : MonoBehaviour
{
    [SerializeField]
    private VRCanvas vrCanvas;

    [SerializeField]
    private Transform bag, hand;

    [SerializeField]
    private float holdTimeToStorableItem = 1.5f;

    private BasicInteractableItem currentHandleItem;
    public BasicInteractableItem CurrentHandleItem
    {
        get
        {
            return currentHandleItem;
        }
    }

    private bool tryRelease = false;

	void Start()
	{
        vrCanvas.SetItemInfo(null);
	}

    void Update()
    {
        if (Input.GetButtonDown("bt-a"))
        {
            if(currentHandleItem)
            {
                tryRelease = true;
            }
        }
        if(Input.GetButtonUp("bt-a") && tryRelease)
        {
            tryRelease = false;
            StopCoroutine("TryReleaseCurrentItem");
            StartCoroutine("TryReleaseCurrentItem");
        }
    }

    public void OnGazeItemEnter(BasicInteractableItem item)
    {
        ResetGazeActions();
        vrCanvas.SetItemInfo(item);
    }
    public void OnGazeItemExit(BasicInteractableItem item)
    {
        vrCanvas.SetItemInfo(null);
    }
    public void OnGazeItemDown(BasicInteractableItem item)
    {
        ResetGazeActions();

        if (item.IsStorable)
            StartCoroutine("TryStoreItem", item);
    }
    public void OnGazeItemClick(BasicInteractableItem item)
    {
        if (currentHandleItem || !item.CanCarry)
            return;
        ResetGazeActions();

        currentHandleItem = item;
        currentHandleItem.AttachTo(hand);
    }

    public void OnGazeNPCEnter(CharacterNPC npc)
    {
        //vrCanvas.SetItemInfo(item);
    }

    public GameObject notification;
    public void Notification(string message)
    {
        notification.SetActive(true);
        StartCoroutine("DeactiveNotification");
    }
    private IEnumerator DeactiveNotification()
    {
        yield return new WaitForSeconds(5);
        notification.SetActive(false);
    }

    private void ResetGazeActions()
    {
        StopCoroutine("TryStoreItem");
        StopCoroutine("TryReleaseCurrentItem");
        tryRelease = false;
    }

    public void UseHandleItem()
    {
        currentHandleItem.AttachTo(bag);
        currentHandleItem = null;
        vrCanvas.SetItemInfo(null);
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
    private IEnumerator TryStoreItem(BasicInteractableItem item)
    {
        yield return new WaitForSeconds(holdTimeToStorableItem);
        item.AttachTo(bag);
    }
}
