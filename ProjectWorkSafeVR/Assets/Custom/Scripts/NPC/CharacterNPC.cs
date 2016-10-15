using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterNPC : MonoBehaviour
{
    [SerializeField]
    private int life;

    [SerializeField]
    private List<SphereDamageArea.DamageType> notTakeDamage;

    [SerializeField]
    private ActionNPC[] routine;

    [SerializeField]
    private float invunarableTime = 0.3f;

    private bool invunarable = false;
    public bool IsInvunarable
    {
        get{ return invunarable; }
    }

    private NavMeshAgent m_agent;

    void Start ()
    {
        m_agent = GetComponentInChildren<NavMeshAgent>();
        StartCoroutine("StartRoutine");
	}

    private IEnumerator StartRoutine()
    {
        int currentAction = 0;
        while (true)
        {
            yield return routine[currentAction].StartAction(this);

            currentAction++;
            if (currentAction == routine.Length)
                currentAction = 0;
        }
    }

    public void MoveTo(Vector3 point, System.Action onCompletePath)
    {
        m_agent.SetDestination(point);
        StopCoroutine("CheckFinish");
        StartCoroutine("CheckFinish", onCompletePath);

    }
    private IEnumerator CheckFinish(System.Action onCompletePath)
    {
        while (m_agent.pathStatus != NavMeshPathStatus.PathComplete || m_agent.remainingDistance > m_agent.stoppingDistance)
        {
            yield return null;
        }

        onCompletePath();
    }

    public void TryDamage(SphereDamageArea damage)
    {
        if (invunarable)
            return;

        if (notTakeDamage.Contains(damage.Damage_Type))
            return;

        life = Mathf.Max(0, life - damage.Damage);

        StartCoroutine("StartInvunarable");
    }
    private IEnumerator StartInvunarable()
    {
        invunarable = true;
        yield return new WaitForSeconds(invunarableTime);
        invunarable = false;
    }
}
