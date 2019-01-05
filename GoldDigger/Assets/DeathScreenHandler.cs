using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenHandler : MonoBehaviour
{

  float loadingTimer = 5;

  bool isLoading = false;

  [SerializeField] GameObject gameOver;
  [SerializeField] GameObject loading;

  Animator loadingAnimator;

	// Use this for initialization
	void Start ()
	{
	  loading.SetActive(false);
	  loadingAnimator = loading.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	  if (Input.GetKeyDown(KeyCode.Mouse0))
	  {
      SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
	    isLoading = true;

	    gameOver.SetActive(false);
      loading.SetActive(true);
	    loadingAnimator.Play("Loading");
	  }

    Debug.Log("TEST");

	  //if (loadingTimer <= 0)
	  //{
	  //  SceneManager.UnloadSceneAsync(1);
	  //  SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
	  //}

	  //if (isLoading)
	  //{
	  //  loadingTimer -= Time.deltaTime;
	  //}

	  if (SceneManager.GetSceneByBuildIndex(0).isLoaded)
	  {
	    SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
    }
	}
}
