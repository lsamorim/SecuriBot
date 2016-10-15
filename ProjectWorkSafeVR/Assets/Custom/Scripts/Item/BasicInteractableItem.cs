using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class BasicInteractableItem : MonoBehaviour
{
    [SerializeField]
    private string id;
    public string Id
    {
        get{ return id; }
    }

	[SerializeField]
	private bool storable, canCarry;
	[SerializeField]
	private Color onGazeEnterColor = Color.white;
    [SerializeField]
    private string itemName;
    [SerializeField, Multiline]
    private string itemDescription;

    protected VrPlayer player;
    protected MeshRenderer m_renderer;
    private Rigidbody m_rigidbody;
    private Collider m_collider;
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

    public string ItemName
    {
        get { return itemName; }
    }

    public virtual string ItemDescription
    {
        get { return itemDescription; }
    }

    private void AddListener(EventTrigger trigger, EventTriggerType type, System.Action callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry( );
        entry.eventID = type;
        entry.callback.AddListener( ( data ) => { callback.Invoke(); } );
        trigger.triggers.Add( entry );
    }

    protected virtual void Start()
	{
        player = FindObjectOfType<VrPlayer>();

        m_renderer = GetComponentInChildren<MeshRenderer>();

        m_rigidbody = GetComponentInChildren<Rigidbody>();
        m_collider = GetComponentInChildren<Collider>();
        AddListener(m_eventTrigger, EventTriggerType.PointerEnter, OnGazeEnter);
        AddListener(m_eventTrigger, EventTriggerType.PointerExit, OnGazeExit);
        AddListener(m_eventTrigger, EventTriggerType.PointerDown, OnGazeDown);
        AddListener(m_eventTrigger, EventTriggerType.PointerClick, OnGazeClick);
	}
	
	public virtual void OnGazeEnter()
	{
        player.OnGazeItemEnter(this);
        //StartCoroutine("OnGazeEnterEffect");
        ItemToColor(onGazeEnterColor);
	}
    public virtual void OnGazeExit()
    {
        player.OnGazeItemExit(this);

//        StopCoroutine("OnGazeEnterEffect");
//        iTween.Stop(m_renderer.gameObject);
        ItemToColor(Color.white);
    }
    public virtual void OnGazeDown()
    {
        player.OnGazeItemDown(this);
    }
    public virtual void OnGazeClick()
    {
        player.OnGazeItemClick(this);
    }

    public void ItemToColor(Color color)
    {
        m_renderer.material.color = color;
    }

//    protected virtual IEnumerator OnGazeEnterEffect()
//    {
//        iTween.Stop(m_renderer.gameObject);
//        while (true)
//        {
//            iTween.ColorTo(m_renderer.gameObject, onGazeEnterColor, 0.5f);
//            yield return new WaitForSeconds(0.55f);
//            iTween.ColorTo(m_renderer.gameObject, Color.white, 0.5f);
//            yield return new WaitForSeconds(0.55f);
//        }
//    }

    public virtual void AttachTo(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        m_rigidbody.isKinematic = true;
        m_collider.enabled = false;
    }
    public virtual void Release()
    {
        transform.SetParent(null);
        m_rigidbody.isKinematic = false;
        m_collider.enabled = true;
    }
}
