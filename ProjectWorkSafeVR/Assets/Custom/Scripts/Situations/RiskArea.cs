using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiskArea : MonoBehaviour
{
    [SerializeField]
    private bool solved;

    [SerializeField]
    private GameObject[] objects_onDanger;

    [SerializeField]
    private GameObject[] objects_onSafe;

    [SerializeField]
    private SphereDamageArea damageArea;

    public void Solved()
    {
        foreach(var danger in objects_onDanger)
            danger.SetActive(false);

        foreach(var safe in objects_onSafe)
            safe.SetActive(true);

        solved = true;
    }


}
