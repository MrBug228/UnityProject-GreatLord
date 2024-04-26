using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private float currentHealth;

    public UnityEvent Ondied;
    // Start is called before the first frame update
    void Start()
    {
        this.ReBorn();
    }

    public void ReBorn()
    {
        this.currentHealth = this.maxHealth;
    }


    private void ChangeHealth(float amount)
    {
        this.currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            this.Ondied.Invoke();
        }
    }


    public virtual void TakeDamage(float amount)// nhan damage
    {
        this.ChangeHealth(-amount);
    }


    public virtual void Heal(float amount)// hoi mau
    {
        this.ChangeHealth(amount);
    }

}
