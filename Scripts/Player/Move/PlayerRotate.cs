using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    [SerializeField]
    private float rotationSpeed = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        this.rb = this.transform.root.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        this.RotateWithMovementDirection();
    }
    private void  RotateWithMovementDirection()
    {
        Vector2 velocity = rb.velocity;
        if (velocity == Vector2.zero)
        {
            return;
        }
        this.moveDirection = velocity.normalized;
        Quaternion targetRotation = Quaternion.LookRotation(rb.transform.forward, moveDirection);
        Quaternion rotation = Quaternion.RotateTowards(rb.transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        this.rb.MoveRotation(rotation);
      
    }

}
