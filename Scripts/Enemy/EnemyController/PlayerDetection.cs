using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public bool playerDitected { get; private set; }

    public Transform playerTransform { get; private set; }

    public Vector2 directionToPlayer { get; private set; }

    [SerializeField]
    private float playerAwarenessDistance;


    private void Awake()
    {
        this.playerTransform = FindObjectOfType<PlayerMovement>().transform;
       
    }
    private void Update()
    {
        this.AwareByDistance();
    }

    void AwareByDistance()
    {
        Vector2 EnemyToPlayerVector = (this.playerTransform.position - this.transform.position);
        this.directionToPlayer = EnemyToPlayerVector.normalized;
       
        
        if(EnemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            playerDitected = true;
        }
        else
        {
            playerDitected = false;
        }
    }
}
