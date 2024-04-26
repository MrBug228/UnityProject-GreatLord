using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSpawn : MonoBehaviour
{
    private GameObject player;
    private Transform objectTransform;
    [SerializeField]
    private Vector3 newPosition;

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        if(this.player == null)
        {
            Debug.Log("khong tim thay player");
        }
        this.objectTransform = this.transform.root;
    }

   
    private void Update()
    {
        objectTransform.position = this.player.transform.position + this.newPosition;
    }
}
