using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour
{
    public AudioSource a;
    public AudioClip c;
	// Use this for initialization
	IEnumerator Start () {
	yield return new WaitForSeconds(0.3f);
        a.PlayOneShot(c);
	    StartCoroutine("Start");
	}
	
	// Update is called once per frame
	void Update () {
	transform.Rotate(Vector3.up * 90 * Time.deltaTime);
	}
}
