using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddHealth : MonoBehaviour {
    private Rigidbody2D body;
    private bool maxHealthReached;
    public int turningPoint;
    public int turningAreaSize;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    private bool isBehindTurningPoint() {
        return this.body.position.x > turningPoint;
    }

    void Update() {
        if (this.isBehindTurningPoint() && !this.maxHealthReached) {
            if (InterSceneInfo.Instance.healthBar != null) {
                InterSceneInfo.Instance.healthBar += 10;
                this.maxHealthReached = true;
            }
        }
    }

}
