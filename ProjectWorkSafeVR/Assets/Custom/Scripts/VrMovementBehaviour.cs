using UnityEngine;
using System.Collections;

public class VrMovementBehaviour : MonoBehaviour
{
	[SerializeField]
    private Transform vrCamera;
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private AudioSource footAudioSource;

    [SerializeField]
    private AudioClip footstepSound_default;

    private Rigidbody m_rigidbody;

    void Start()
	{
        m_rigidbody = GetComponent<Rigidbody> ();

	}

	void Update()
	{
//		if(Input.GetButtonDown("bt-y"))
//			Jump ("bt-y");
//		if(Input.GetButtonDown("bt-b"))
//			Jump ("bt-b");
//		if(Input.GetButtonDown("bt-x"))
//			Jump ("bt-x");
		
//		if(Input.GetButtonDown("Jump"))
//			rigidbody.AddForce (Vector3.up * 10, ForceMode.Impulse);
	}
	void Jump(string inputName)
	{
		GetComponent<Rigidbody>().AddForce (Vector3.up * 10, ForceMode.Impulse);
	}

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (v == 0 && h == 0)
        {
            if (footAudioSource.isPlaying)
            {
                footAudioSource.Pause();
            }
        }
        else
        {
            if (!footAudioSource.isPlaying)
            {
                footAudioSource.clip = footstepSound_default;
                footAudioSource.Play();
            }
        }


        m_rigidbody.MovePosition(
			transform.position +
			vrCamera.transform.right.WithY(0) * h * movementSpeed * Time.fixedDeltaTime
			+ vrCamera.transform.forward.WithY(0) * v * movementSpeed * Time.fixedDeltaTime
		);
	}
}
