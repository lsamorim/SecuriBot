using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class BasicInteractableItem : MonoBehaviour
{
    [SerializeField]
    private string id;

	[SerializeField]
	private bool storable, canCarry;
	[SerializeField]
	private Color onEnterOutlineColor = Color.white;
    [SerializeField]
    private float onEnterOutlineWidth = 0.008f;
	[SerializeField, Multiline]
	private string utilityDescription; 

    private VrPlayer player;
	private MeshRenderer m_renderer;
    private Rigidbody m_rigidbody;
    private EventTrigger internal_eventTrigger;
    private EventTrigger m_eventTrigger
    {
        get
        {
            if (internal_eventTrigger)
                return internal_eventTrigger;

            internal_eventTrigger = gameObject.GetComponentInChildren<EventTrigger>();

            if (internal_eventTrigger)
                return internal_eventTrigger;

            internal_eventTrigger = gameObject.AddComponent<EventTrigger>();

            return internal_eventTrigger;
        }
    }

    public bool IsStorable
    {
        get{ return storable; }
    }
    public bool CanCarry
    {
        get{ return canCarry; }
    }

    private void AddListener(EventTrigger trigger, EventTriggerType type, System.Action callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry( );
        entry.eventID = type;
        entry.callback.AddListener( ( data ) => { callback.Invoke(); } );
        trigger.triggers.Add( entry );
    }

	void Start()
	{
        player = FindObjectOfType<VrPlayer>();

        m_renderer = GetComponentInChildren<MeshRenderer>();
        m_renderer.material.SetFloat("_Outline", 0);
        m_renderer.material.SetColor("_OutlineColor", onEnterOutlineColor);

        m_rigidbody = GetComponentInChildren<Rigidbody>();

        AddListener(m_eventTrigger, EventTriggerType.PointerEnter, OnGazeEnter);
        AddListener(m_eventTrigger, EventTriggerType.PointerExit, OnGazeExit);
        AddListener(m_eventTrigger, EventTriggerType.PointerDown, OnGazeDown);
        AddListener(m_eventTrigger, EventTriggerType.PointerClick, OnGazeClick);
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
