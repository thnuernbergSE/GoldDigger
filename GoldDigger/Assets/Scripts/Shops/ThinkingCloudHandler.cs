using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingCloudHandler : MonoBehaviour
{

  GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");

	  if (player == null)
	  {
      throw new NullReferenceException("Player not found! - ThinkingCloudHandler.cs");
	  }
	}
	
	// Update is called once per frame
	void Update ()
	{
	  transform.localScale = player.transform.localScale.x < 0 ? new Vector3(-1,1) : new Vector3(1,1);
	}
}
