using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterNPC : MonoBehaviour
{
    [SerializeField]
    private int life;
    private int currentLife;

    [SerializeField]
    private List<SphereDamageArea.DamageType> notTakeDamage;

    [SerializeField]
    private ActionData[] routine;

    [SerializeField]
    private float invunarableTime = 0.3f;

    private bool invunarable = false;
    public bool IsInvunarable
    {
        get{ return invunarable; }
    }

    public Transform lifeBar;

    private NavMeshAgent m_agent;

    private Animator m_animator;
    private VrPlayer player;

    void Start ()
    {
        player = FindObjectOfType<VrPlayer>();
        m_agent = GetComponentInChildren<NavMeshAgent>();
        m_animator = GetComponentInChildren<Animator>();
        StartCoroutine("StartRoutine");

        currentLife = life;
	}

    private IEnumerator StartRoutine()
    {
        int currentAction = 0;
        while (true)
        {
            yield return new WaitForSeconds(routine[currentAction].timeBefore);

            if(routine[currentAction].action)
                yield return routine[currentAction].action.StartAction(this);

            yield return new WaitForSeconds(routine[currentAction].timeAfter);

            currentAction++;
            if (currentAction == routine.Length)
                currentAction = 0;
        }
    }

    public void OnGazeEnter()
    {
        player.OnGazeNPCEnter(this);
    }

    public void MoveTo(Vector3 point, System.Action onCompletePath)
    {
        m_animator.SetBool("walking", true);
        m_agent.SetDestination(point);
        StopCoroutine("CheckFinish");
        StartCoroutine("CheckFinish", onCompletePath);

    }
    private IEnumerator CheckFinish(System.Action onCompletePath)
    {
        yield return new WaitForSeconds(1);

        while (m_agent.pathPending)
        {
            yield return null;
        }

        while(m_agent.remainingDistance > m_agent.stoppingDistance)
        {
            yield return null;
        }

        //print(gameObject.name + "shb");
        m_animator.SetBool("walking", false);
        onCompletePath();
    }

    public void TryDamage(SphereDamageArea damage)
    {
        if (invunarable)
            return;

        if (notTakeDamage.Contains(damage.Damage_Type))
            return;

        currentLife = Mathf.Max(0, currentLife - damage.Damage);

        lifeBar.transform.localScale = lifeBar.transform.localScale.WithX((currentLife *1f)/ life);

        m_animator.SetInteger("damage_code", 1);
        m_animator.SetTrigger("damage");

        StartCoroutine("StartInvunarable");
    }
    private IEnumerator StartInvunarable()
    {
        invunarable = true;
        yield return new WaitForSeconds(invunarableTime);
        invunarable = false;
    }
}

[System.Serializable]
public class ActionData
{
    public float timeBefore;
    public ActionNPC action;
    public float timeAfter;
}
