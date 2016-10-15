using UnityEngine;
using System.Collections;

public class ProblemItem : BasicInteractableItem
{
    [Header("PROBLEM ITEM"), SerializeField]
    private Color onHandlingSolutionItemColor;

    public RiskArea riskArea;

    protected override void Start ()
    {
        base.Start();
	}

    public override void OnGazeEnter()
    {
        base.OnGazeEnter();
        if (player.CurrentHandleItem && player.CurrentHandleItem.Id.ToLower() == Id.ToLower())
        {
            ItemToColor(onHandlingSolutionItemColor);
            player.CurrentHandleItem.ItemToColor(onHandlingSolutionItemColor);
        }
    }

    public override void OnGazeExit()
    {
        base.OnGazeExit();

        if(player.CurrentHandleItem && player.CurrentHandleItem.Id.ToLower() == Id.ToLower())
        {
            player.CurrentHandleItem.ItemToColor(Color.white);
        }
    }

    public override void OnGazeClick()
    {
        base.OnGazeClick();
        if(player.CurrentHandleItem && player.CurrentHandleItem.Id.ToLower() == Id.ToLower())
        {
            riskArea.Solved();
            player.UseHandleItem();
        }
    }
}
