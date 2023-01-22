using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
  public float speed;
  private float move;
  private Rigidbody2D body;

  void Start() {
    body = GetComponent<Rigidbody2D>();
  }

  void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            body.velocity = new Vector2(body.velocity.x, 0);
            }
             }

  void Update() {
   

    move = Input.GetAxis("Horizontal");
    body.velocity = new Vector2(speed * move, body.velocity.y);
    }

    
     
}
