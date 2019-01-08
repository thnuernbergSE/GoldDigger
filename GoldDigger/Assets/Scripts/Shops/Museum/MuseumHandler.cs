using System;
using UnityEngine;

public class MuseumHandler : MonoBehaviour
{
  GameObject museumUI;

  bool isShopOpen = false;
  bool isColliding = false;

  // Use this for initialization
  void Start()
  {
    museumUI = GameObject.Find("MuseumPopup");

    if (museumUI == null)
    {
      throw new NullReferenceException("MuseumUI is null - MuseumHandler.cs");
    }

    museumUI.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (isColliding)
    {
      if (Input.GetKeyDown(KeyCode.F))
      {
        isShopOpen = !museumUI.activeSelf;
        museumUI.SetActive(isShopOpen);
      }
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    isColliding = true;
  }

  void OnTriggerExit2D(Collider2D other)
  {
    isColliding = false;
  }
}
