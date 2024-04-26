using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
   
    private ObjectPool objectPool;

    GameObject bulletObj;
    DamageSender damageSender;
    void Awake()
    {
        this.bulletObj = this.transform.root.gameObject;
        this.damageSender = this.bulletObj.GetComponentInChildren<DamageSender>();
        this.objectPool = GameObject.Find("Bullet_Pool").GetComponent<ObjectPool>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (damageSender == null)
            {
                Debug.Log("Enemy damage receiver  is null");
                return;
            }

            damageSender.SendDamage(collision.gameObject);
        }

        objectPool.Release(this.bulletObj);
    }
}

