using UnityEngine;
using System.Collections;

public class MoveActionNPC : ActionNPC
{
    [SerializeField]
    private Transform[] point;

    private CharacterNPC oldNPC;
    [SerializeField]
    private int currentPointIndex = 0;
    private bool reverse = false;

    #if UNITY_EDITOR

    [SerializeField]
    private bool drawGizmos;
    void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;
        for(int i = 0; i < point.Length; i++)
        {
            Gizmos.color = new Color(0, 0f, 1f, 0.8f);
            Gizmos.DrawSphere(point[i].position, 0.3f);

            Gizmos.color = Color.white;
            if (i > 0)
            {
                Gizmos.DrawLine(point[i - 1].position, point[i].position);
            }
        }
    }

    #endif

    public override IEnumerator StartAction(CharacterNPC npc)
    {
        if(oldNPC == npc)
        {
            if (reverse && currentPointIndex <= 0)
            {
                currentPointIndex = 0;
                reverse = false;
            }
            else if(!reverse && currentPointIndex >= point.Length - 1)
            {
                currentPointIndex = point.Length - 1;
                reverse = true;
            }
        }
        else
        {
            currentPointIndex = 0;
            reverse = false;
        }

        oldNPC = npc;

        yield return base.StartAction(npc);
    }

    protected override void DoLogic()
    {
        StartCoroutine("MoveNext");
    }
    private IEnumerator MoveNext()
    {
        if(reverse)
        {
            if (currentPointIndex < 0)
            {
                currentPointIndex = 0;
                base.finish = true;
                yield break;
            }
            npc.MoveTo(point[currentPointIndex].position, ()=>
                {
                    currentPointIndex--;
                    StartCoroutine("MoveNext");
                });
        }
        else
        {
            if (currentPointIndex >= point.Length)
            {
                currentPointIndex = point.Length - 1;
                base.finish = true;
                yield break;
            }
            npc.MoveTo(point[currentPointIndex].position, ()=>
                {
                    currentPointIndex++;
                    StartCoroutine("MoveNext");
                });
        }
        yield return null;
    }

}
