using UnityEngine;
using System.Collections;

public abstract class ActionNPC : MonoBehaviour
{
    protected CharacterNPC npc;
    protected bool finish = false;

    public virtual IEnumerator StartAction(CharacterNPC npc)
    {
        this.npc = npc;
        finish = false;
        DoLogic();

        while (!finish)
        {
            yield return null;
        }
    }

    protected abstract void DoLogic();
}
