using UnityEngine;
using System.Collections;

public class VrPlayer : MonoBehaviour
{
	public Transform vrCamera;
	public Vector3 positionOffset;

	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		vrCamera.position = transform.position + positionOffset;
		Vector3 theRotation = transform.localEulerAngles;
		theRotation.y = vrCamera.eulerAngles.y;
		transform.localEulerAngles = theRotation;
	}
}
