using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollusionWithTimer : MonoBehaviour
{
  public int turningPoint;
  public int turningAreaSize;
  public int footOfHill;
  public int menuTreshold; 

  private CircleCollider2D collider;
  private Rigidbody2D body;
  private GameObject player;
  private PolygonCollider2D playerCollider;
  private Rigidbody2D playerBody;
  private bool collidesWithPlayer;
  private int collisionCounter; 
  private bool menuUntriggered;
  

  void Start() {
    collider = GetComponent<CircleCollider2D>();
    body = GetComponent<Rigidbody2D>();
    player = GameObject.FindWithTag("Player");
    playerCollider = player.GetComponent<PolygonCollider2D>();
    playerBody = player.GetComponent<Rigidbody2D>();
    menuTreshold = Random.Range(1, 5);
    collisionCounter = 0;
    menuUntriggered = true;
  }

  private bool IsBehindTurningPoint() {
    bool isBehind = this.body.position.x > turningPoint;
    if (isBehind) {
      this.collisionCounter += 1;
      if (this.collisionCounter > this.menuTreshold) {
        this.collisionCounter = 0;
      } 
    };
    return isBehind;
  }

  private bool returnedAfterRoll() {
    return this.body.position.x < footOfHill;
  }

  private bool IsOutOfTurningArea() {
    return this.body.position.x < turningPoint - turningAreaSize;
  }

  private bool IsRightOfPlayer() {
    return playerBody.position.x < this.body.position.x - this.collider.radius / 2 - 1;
  }

  private bool maxCollidesReached() {
    return this.collisionCounter >= this.menuTreshold;
  }
  private IEnumerator LoadMenu() {
    yield return new WaitForSeconds(5);
    SceneManager.LoadScene("Main Menu");
  }

  void Update() {
    if (collidesWithPlayer && this.IsBehindTurningPoint()) {
      collidesWithPlayer = false;
    }
    if (!collidesWithPlayer && this.IsOutOfTurningArea() && this.IsRightOfPlayer()) {
      collidesWithPlayer = true;
    }
    if (this.returnedAfterRoll() && this.maxCollidesReached() && menuUntriggered) {
      this.body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
      StartCoroutine(LoadMenu());
      this.menuUntriggered = false;
    };


    Physics2D.IgnoreCollision(playerCollider, collider, !collidesWithPlayer);
        
    }
}
