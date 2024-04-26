using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    private HealthController healthController;
    private void Awake()
    {
        this.healthController = this.transform.root.GetComponentInChildren<HealthController>();
    }
    public virtual void ReceiveDamage(float damage)
    {
        if(this.healthController == null)
        {
            Debug.Log("ReceiveDamage khong tim thay healthController ");
            return;
        }
        this.healthController.TakeDamage(damage);
    }
}
