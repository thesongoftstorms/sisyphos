using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
  public GameObject startMenu;
  public static bool isPaused;

  private bool isStarted;

  void Start() {
    this.PauseGame();
    isStarted = false;
  }

  void Update() {
    if(!isStarted) return;

    if(Input.GetKeyDown(KeyCode.Escape)) {
      if(isPaused) {
        ResumeGame();
        return;
      }

      PauseGame();
    }
  }

  public void StartGame() {
    isStarted = true;
    ResumeGame();
  }

  public void ResumeGame() {
    isPaused = false;
    this.startMenu.SetActive(false);
  }

  public void QuitGame() {
    Application.Quit();
  }

  private void PauseGame() {
    isPaused = true;
    this.startMenu.SetActive(true);
  }
}
