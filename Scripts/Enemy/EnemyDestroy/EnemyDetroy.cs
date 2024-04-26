using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetroy : MonoBehaviour
{

    private ObjectPool objectPool;

    private  GameObject enemyGameObject;
    private void Awake()
    {
        this.enemyGameObject = this.transform.root.gameObject;
        this.objectPool = GameObject.Find("Enemy_Pool").GetComponent<ObjectPool>();

    }

    public void DestroyEnemy()
    {
       
        this.objectPool.Release(this.enemyGameObject);
        
    }


}
