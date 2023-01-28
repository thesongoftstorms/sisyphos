using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollusionWithTimer : MonoBehaviour
{
  public int turningPoint;
  public int turningAreaSize;
  public int timeout;

  private CircleCollider2D collider;
  private Rigidbody2D body;
  private GameObject player;
  private PolygonCollider2D playerCollider;
  private Rigidbody2D playerBody;
  private bool collidesWithPlayer;
  private bool menuUntriggered;
  

  void Start() {
    collider = GetComponent<CircleCollider2D>();
    body = GetComponent<Rigidbody2D>();
    player = GameObject.FindWithTag("Player");
    playerCollider = player.GetComponent<PolygonCollider2D>();
    playerBody = player.GetComponent<Rigidbody2D>();
    menuUntriggered = true;
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

  private IEnumerator LoadMenu() {
    if (this.timeout == 0) {
        this.timeout = 5;
    }
    yield return new WaitForSeconds(this.timeout);
    SceneManager.LoadScene("Main Menu");
  }

  void Update() {
    this.body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    Physics2D.IgnoreCollision(playerCollider, collider, true);
    if (!collidesWithPlayer && !this.IsRightOfPlayer()) {
      collidesWithPlayer = true;
    }
    if (collidesWithPlayer && this.menuUntriggered) {
        StartCoroutine(LoadMenu());
        this.menuUntriggered = false;
    }   
  }
}
