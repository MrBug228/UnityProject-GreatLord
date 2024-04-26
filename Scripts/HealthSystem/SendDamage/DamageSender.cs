using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    [SerializeField]
    private float damage;

    public virtual void SendDamage(GameObject obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if(damageReceiver == null)
        {
            Debug.Log("damageReceiver is null ");
            return;
        }
        damageReceiver.ReceiveDamage(damage);

    }
}
