using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController: MonoBehaviour
{
  public int turningPoint;
  public int turningAreaSize;
  public int footOfHill;
  public int menuTreshold; 
  public Transform attackPos;
  public LayerMask enemies;
  public float attackrange;

  private CircleCollider2D collider;
  private Rigidbody2D body;
  private GameObject stone;
  private PolygonCollider2D playerCollider;
  private Animator anim;
  private Rigidbody2D playerBody;
  private bool collidesWithPlayer;
  
  void Start() {
    stone = GameObject.FindWithTag("Stone");
    collider = stone.GetComponent<CircleCollider2D>();
    body = stone.GetComponent<Rigidbody2D>();
    playerCollider = GetComponent<PolygonCollider2D>();
    playerBody = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    attackrange = 1;
  }
  private void OnDrawGizmosSelected()
  {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(attackPos.position, attackrange); 
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
  
  private bool HasContact() {
    Collider2D[] collisionCandidates = Physics2D.OverlapCircleAll(attackPos.position, attackrange, enemies);
    return collisionCandidates.Length > 0;
  }

  private bool IsMoving() {
    return Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.RightArrow);
  }

  void Update() {
    if (collidesWithPlayer && this.IsBehindTurningPoint()) {
      collidesWithPlayer = false;
    }
    if (!collidesWithPlayer && this.IsOutOfTurningArea() && this.IsRightOfPlayer()) {
      collidesWithPlayer = true;
    }
    if (this.HasContact() && this.IsMoving()) {
      this.anim.Play("Player_push");
    }
    else if (this.IsMoving()) {
      this.anim.Play("Player_walk");   
    }
    else {
      this.anim.Play("Player_idle");
    }

    Physics2D.IgnoreCollision(playerCollider, collider, !collidesWithPlayer);   
    }
}
