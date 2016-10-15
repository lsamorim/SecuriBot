using UnityEngine;
using System.Collections;

public class CharacterNPC : MonoBehaviour
{
    [SerializeField]
    private int life;
    [SerializeField]
    private ActionNPC[] routine;

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
}
