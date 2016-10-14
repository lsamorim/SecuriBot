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

	private MeshRenderer m_renderer;
    private UnityEngine.EventSystems.EventTrigger m_eventTrigger;

	void Start()
	{
		m_renderer = GetComponentInChildren<MeshRenderer>();
        m_renderer.material.SetFloat("_Outline", 0);
        m_renderer.material.SetColor("_OutlineColor", onEnterOutlineColor);
	}
	
	public virtual void OnGazeEnter()
	{
        m_renderer.material.SetFloat("_Outline", onEnterOutlineWidth);
	}
    public virtual void OnGazeExit()
    {
        m_renderer.material.SetFloat("_Outline", 0);
    }
}
