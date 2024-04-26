using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBulletByDistance : MonoBehaviour
{

    private ObjectPool objectPool;

    private Transform PlayerTransform;

    [SerializeField]
    private float destroyDistance = 30f;

    private void Awake()
    {
        this.PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        this.objectPool = GameObject.Find("Bullet_Pool").GetComponent<ObjectPool>();
    }
    private void Update()
    {

        float distance = Vector2.Distance(this.transform.parent.parent.parent.position, this.PlayerTransform.position);


        if (distance >= this.destroyDistance)
        {
             objectPool.Release(transform.parent.parent.parent.gameObject);
          
        }
    }


}
