using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject slash;
    public GameObject altSlash;
    public GameObject downSlash;
    public GameObject upSlash;

    public ContactFilter2D enemyContactFilter;

    public enum AttackType
    {
        Slash, AltSlash, DownSlash, Upslash
    }
    public void Play(AttackType attackType, ref List<Collider2D> colliders)
    {
        switch (attackType)
        {
            case AttackType.Slash:
                Physics2D.OverlapCollider(slash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                slash.GetComponent<AudioSource>().Play();
                break;
            case AttackType.AltSlash:
                Physics2D.OverlapCollider(altSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                altSlash.GetComponent<AudioSource>().Play();
                break;
            case AttackType.DownSlash:
                Physics2D.OverlapCollider(downSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                downSlash.GetComponent<AudioSource>().Play();
                break;
            case AttackType.Upslash:
                Physics2D.OverlapCollider(upSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                upSlash.GetComponent<AudioSource>().Play();
                break;
            default:
                break;

        }

    }
}
