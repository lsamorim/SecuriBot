using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiskArea : MonoBehaviour
{
    [SerializeField]
    private bool solved;

    [SerializeField]
    private GameObject object_onDanger;

    [SerializeField]
    private GameObject object_onSafe;

    [SerializeField]
    private SphereDamageArea damageArea;

    void Start()
    {
	    
	}
	
	void LateUpdate()
    {
        
        //Physics.OverlapSphereNonAlloc(damageArea, damageArea.radius, out others);
	}
}
