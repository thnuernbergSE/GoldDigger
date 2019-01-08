using System;
using UnityEngine;

public class MerchantHandler : MonoBehaviour
{
  bool isColliding = false;

  bool isShopOpen;

  GameObject merchantUI;

  void Start()
  {
    merchantUI = GameObject.Find("MerchantPopup");

    if (merchantUI == null)
    {
      throw new NullReferenceException("MerchanthUI is null - MerchantHandler.cs");
    }

    merchantUI.SetActive(false);
    merchantUI.transform.GetChild(0).gameObject.SetActive(false);

  }

  // Update is called once per frame
  void Update()
  {
    if (isColliding)
    {
      if (Input.GetKeyDown(KeyCode.F))
      {
        isShopOpen = !merchantUI.activeSelf;

        merchantUI.SetActive(isShopOpen);
        merchantUI.transform.GetChild(0).gameObject.SetActive(isShopOpen);
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
    isShopOpen = false;

    merchantUI.transform.GetChild(0).gameObject.SetActive(false);

    merchantUI.SetActive(false);
  }
}
