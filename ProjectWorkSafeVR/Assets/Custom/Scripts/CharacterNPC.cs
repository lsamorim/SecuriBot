using UnityEngine;
using System.Collections;

public class CharacterNPC : MonoBehaviour
{
    private int life;

    public Transform pointA, pointB;
    public NavMeshAgent agent;

    IEnumerator Start ()
    {
        agent.SetDestination(pointA.position);
        yield return new WaitForSeconds(5);
        agent.SetDestination(pointB.position);
        yield return new WaitForSeconds(5);
        agent.SetDestination(pointA.position);
        yield return new WaitForSeconds(5);
        agent.SetDestination(pointB.position);
	}
	
	void Update ()
    {
	
	}
}
