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

    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerCollider = player.GetComponent<PolygonCollider2D>();
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    private bool isBehindTurningPoint() {
        return this.body.position.x > turningPoint;
    }

    private bool isOutOfTurningArea() {
        return this.body.position.x < turningPoint - turningAreaSize;
    }

    private bool isRightOfPlayer() {
        return playerBody.position.x < this.body.position.x - this.collider.radius / 2 - 1;
    }

    void Update()
    {
        if (collidesWithPlayer && this.isBehindTurningPoint()) {
            collidesWithPlayer = false;
        }
        if (!collidesWithPlayer && this.isOutOfTurningArea() && this.isRightOfPlayer()) {
            collidesWithPlayer = true;
        }

        Physics2D.IgnoreCollision(playerCollider, collider, !collidesWithPlayer);
    }
}
