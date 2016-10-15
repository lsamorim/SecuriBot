using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class SphereDamageArea : MonoBehaviour
{
    public enum DamageType
    {
        DEFAULT, 
    }
    [SerializeField]
    private DamageType m_type;
    public DamageType Damage_Type
    {
        get
        {
            return m_type;
        }
    }

    [SerializeField]
    private int damage;
    public int Damage
    {
        get{ return damage; }
    }

    [SerializeField]
    private float delayToDamage;

    private SphereCollider m_collider;
    private List<CharacterNPC> tryingDamage = new List<CharacterNPC>();

    public LayerMask layer;
    private Collider[] others = new Collider[10];

    void Start()
    {
        m_collider = GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        int collisionCount = Physics.OverlapSphereNonAlloc(m_collider.transform.position, m_collider.radius, others, layer);
        if (collisionCount > 0)
        {
            for(int i = 0; i < collisionCount; i++)
            {
                var npc = others[i].gameObject.GetComponent<CharacterNPC>();
                if (npc)
                {
                    if (delayToDamage == 0)
                    {
                        npc.TryDamage(this);
                    }
                    else
                    {
                        if(!npc.IsInvunarable && !tryingDamage.Contains(npc))
                        {
                            tryingDamage.Add(npc);
                            StartCoroutine("TryDamage", npc);
                        }
                    }
                }
            }
        }
    }

    private IEnumerator TryDamage(CharacterNPC verifyNpc)
    {
        yield return new WaitForSeconds(delayToDamage);

        int collisionCount = Physics.OverlapSphereNonAlloc(m_collider.transform.position, m_collider.radius, others, layer);
        if (collisionCount > 0)
        {
            for(int i = 0; i < collisionCount; i++)
            {
                var post_npc = others[i].gameObject.GetComponent<CharacterNPC>();
                if (post_npc)
                {
                    if (post_npc == verifyNpc)
                    {
                        verifyNpc.TryDamage(this);
                        break;
                    }
                }
            }
        }

        tryingDamage.Remove(verifyNpc);
    }
}
