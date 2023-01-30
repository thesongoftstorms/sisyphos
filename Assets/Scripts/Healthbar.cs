using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Healthbar : MonoBehaviour {
  public Image healthBarImage;  

  public void UpdateHealthBar() {
    float health = 100f;
    if (InterSceneInfo.Instance.healthBar != null) {
      health = InterSceneInfo.Instance.healthBar;
    }
    healthBarImage.fillAmount = Mathf.Clamp(health / 100, 0, 1f);
  }

  void Update() {
    this.UpdateHealthBar();
  }
}
