using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandling : MonoBehaviour
{
  public int turningPoint;
  public int turningAreaSize;

  private CircleCollider2D collider;
  private Rigidbody2D body;
  private GameObject player;
  private PolygonCollider2D playerCollider;
  private Rigidbody2D playerBody;
  private bool collidesWithPlayer;

  void Start() {
    collider = GetComponent<CircleCollider2D>();
    body = GetComponent<Rigidbody2D>();
    player = GameObject.FindWithTag("Player");
    playerCollider = player.GetComponent<PolygonCollider2D>();
    playerBody = player.GetComponent<Rigidbody2D>();
  }

  private bool IsBehindTurningPoint() {
    return this.body.position.x > turningPoint;
  }

  private bool IsOutOfTurningArea() {
    return this.body.position.x < turningPoint - turningAreaSize;
  }

  private bool IsRightOfPlayer() {
    return playerBody.position.x < this.body.position.x - this.collider.radius / 2 - 1;
  }

  void Update() {
    if (collidesWithPlayer && this.IsBehindTurningPoint()) {
      collidesWithPlayer = false;
    }
    if (!collidesWithPlayer && this.IsOutOfTurningArea() && this.IsRightOfPlayer()) {
      collidesWithPlayer = true;
    }

    Physics2D.IgnoreCollision(playerCollider, collider, !collidesWithPlayer);
  }
}
