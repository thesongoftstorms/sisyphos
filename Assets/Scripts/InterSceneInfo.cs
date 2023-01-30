using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterSceneInfo : MonoBehaviour {
    public static InterSceneInfo Instance;
    public int healthBar;

    private void Awake() {    
    // start of new code
    if (Instance != null) {
        Destroy(gameObject);
        return;
    }
    // end of new code
        Instance = this;
        healthBar = 100;
        DontDestroyOnLoad(gameObject);
    }
}
