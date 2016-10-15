using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PointToMainCamera : MonoBehaviour {
    public Canvas canvas;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        canvas.transform.LookAt(Camera.main.transform);
	}
}
