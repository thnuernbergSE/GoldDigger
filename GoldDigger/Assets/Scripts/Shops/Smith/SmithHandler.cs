using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithHandler : MonoBehaviour
{

  bool isColliding = false;
  bool isShopOpen = false;

  GameObject smithUI;
	// Use this for initialization
	void Start ()
	{
    smithUI = GameObject.Find("SmithPopup");
	  if (smithUI == null)
	  {
      throw new NullReferenceException("SmithUI is null - SmithHandler.cs");
	  }

    smithUI.SetActive(false);

	  smithUI.transform.GetChild(0).gameObject.SetActive(false);
	  smithUI.transform.GetChild(1).gameObject.SetActive(false);
	  smithUI.transform.GetChild(2).gameObject.SetActive(true);
  }
	
	// Update is called once per frame
	void Update () {
	  if (isColliding)
	  {
	    if (Input.GetKeyDown(KeyCode.F))
      {
        isShopOpen = !smithUI.activeSelf;
        smithUI.SetActive(isShopOpen);

        if (isShopOpen)
        {
          smithUI.transform.GetChild(0).gameObject.SetActive(false);
          smithUI.transform.GetChild(1).gameObject.SetActive(false);
          smithUI.transform.GetChild(2).gameObject.SetActive(true);
        }
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

    smithUI.transform.GetChild(0).gameObject.SetActive(false);
    smithUI.transform.GetChild(1).gameObject.SetActive(false);
    smithUI.transform.GetChild(2).gameObject.SetActive(true);

    smithUI.SetActive(false);
  }

  
}
