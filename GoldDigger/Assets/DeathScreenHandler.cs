﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenHandler : MonoBehaviour
{
  bool isLoading;

  [SerializeField] GameObject loading;
  [SerializeField] GameObject gameOver;

  Animator loadingAnimator;

  float loadingTimer = 5;

  void Start()
  {
    loading.SetActive(false);
    loadingAnimator = loading.GetComponent<Animator>();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
      isLoading = true;

      gameOver.SetActive(false);
      loading.SetActive(true);
      loadingAnimator.Play("Loading");
    }

    if (SceneManager.GetSceneByBuildIndex(0).isLoaded)
    {
      SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }
  }
}