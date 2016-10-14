using UnityEngine;
using System.Collections;

public class BasicItem : MonoBehaviour
{
	[SerializeField]
	private bool storable;
	[SerializeField]
	private Color onEnterOutlineColor = Color.white;
    [SerializeField]
    private float onEnterOutlineWidth = 0.008f;
	[SerializeField, Multiline]
	private string utilityDescription; 

    [SerializeField]
    private int identifier;

    private VrPlayer player;
	private MeshRenderer m_renderer;
    private Rigidbody m_rigidbody;

    public bool IsStorable
    {
        get{ return storable; }
    }

	void Start()
	{
        player = FindObjectOfType<VrPlayer>();

        m_renderer = GetComponentInChildren<MeshRenderer>();
        m_renderer.material.SetFloat("_Outline", 0);
        m_renderer.material.SetColor("_OutlineColor", onEnterOutlineColor);

        m_rigidbody = GetComponentInChildren<Rigidbody>();
	}
	
	public virtual void OnGazeEnter()
	{
        player.OnGazeItemEnter(this);
        m_renderer.material.SetFloat("_Outline", onEnterOutlineWidth);
	}
    public virtual void OnGazeExit()
    {
        player.OnGazeItemEnter(this);
        m_renderer.material.SetFloat("_Outline", 0);
    }
    public virtual void OnGazeDown()
    {
        player.OnGazeItemDown(this);
    }
    public virtual void OnGazeClick()
    {
        player.OnGazeItemClick(this);
    }

    public virtual void AttachTo(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        m_rigidbody.isKinematic = true;
    }
    public virtual void Release()
    {
        transform.SetParent(null);
        m_rigidbody.isKinematic = false;
    }
}
