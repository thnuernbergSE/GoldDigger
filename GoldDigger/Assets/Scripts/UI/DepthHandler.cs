using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine.UI;
using UnityEngine;

public class DepthHandler : MonoBehaviour
{

  Text text;
  PlayerControl playerControl;

	// Use this for initialization
	void Start ()
	{
	  text = GetComponent<Text>();
	  playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
	  if (playerControl == null)
	  {
	    throw new NullReferenceException("Player not found! - DepthHandler");
	  }
	}
	
	// Update is called once per frame
	void Update ()
	{
	  text.text = ((int)playerControl.GetYPosition - 2).ToString() + " m";
	}
}
