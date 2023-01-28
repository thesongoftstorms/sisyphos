using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    public Transform attackPos;
    public LayerMask enemies;
    public float attackrange;
    public int damage;
    public int timeout;

    private IEnumerator LoadMenu() {
      if (this.timeout == 0) {
          this.timeout = 5;
      }
      yield return new WaitForSeconds(this.timeout);
      SceneManager.LoadScene("Main Menu");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackrange); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackrange, enemies);
            for(int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Enemy>().health -= damage;
            }
            StartCoroutine(LoadMenu());
         }
     } 
}
