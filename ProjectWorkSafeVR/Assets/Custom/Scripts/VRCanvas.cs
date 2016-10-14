using UnityEngine;
using System.Collections;

public class VRCanvas : MonoBehaviour
{
	public Transform reference;
	private Vector3 offsetPosition;

	void Start()
	{
		offsetPosition = transform.position - reference.transform.position;
		Attach ();
	}

	void LateUpdate ()
	{
		//transform.position = reference.transform.position + offsetPosition;
	}

	public void Attach()
	{
		transform.position = reference.transform.position + offsetPosition;
		transform.SetParent (reference);
	}
	public void Detach()
	{
		transform.SetParent (null);
	}
}
