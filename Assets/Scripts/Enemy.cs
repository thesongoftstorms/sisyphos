t using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public bool causeDamage;
    public int timeout = 5;
    private bool maxDamageDealt = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private IEnumerator LoadMenu() {
      if (this.timeout == 0) {
          this.timeout = 5;
      }
      transform.position = new Vector3(0, 1000, 1000);
      yield return new WaitForSeconds(this.timeout);
      SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if(health <=0)
        {
            if (InterSceneInfo.Instance != null && this.causeDamage && !this.maxDamageDealt) {
                InterSceneInfo.Instance.healthBar -= 10;
                this.maxDamageDealt = true;
            }
            if (!this.causeDamage) {
                StartCoroutine(LoadMenu());
            }
            Vector3 hidden = new Vector3(0, 1000, 1000);
            if (transform.position != hidden) {
                transform.position = hidden;
            }
        }
    }
}
