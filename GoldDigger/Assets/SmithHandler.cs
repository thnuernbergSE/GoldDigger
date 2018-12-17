using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithHandler : MonoBehaviour
{

  bool isColliding = false;
  bool isShopOpen;

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
	}
	
	// Update is called once per frame
	void Update () {
	  if (isColliding)
	  {
	    if (Input.GetKeyDown(KeyCode.F))
	    {
	      isShopOpen = !isShopOpen;
	      smithUI.SetActive(isShopOpen);
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
    smithUI.SetActive(false);
  }

  
}
