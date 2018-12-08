using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderHandler : MonoBehaviour {

  GameObject stoneUnderneath;
  bool isFalling = false;
  float maxFallDistance = 1f;
  float fallDistance;

  // Use this for initialization
  void Start ()
	{
	  getStoneUnderneath();
	}
	
	// Update is called once per frame
	void Update () {
	  if (isFalling)
	  {
	    float toFall = 0.1f;
	    if (fallDistance < maxFallDistance) { 
        transform.position -= new Vector3(0, toFall);
	      fallDistance += toFall;
      }
	    else
	    {
	      isFalling = false;
	      fallDistance = 0f;
	    }
    }
	  else
	  {
      getStoneUnderneath();
	  }
  }

  void getStoneUnderneath()
  {
    var col = Physics2D.OverlapCircle(transform.position + new Vector3(0, -1), 0.1f);
    if (col != null && col.gameObject.layer == 8)
      stoneUnderneath = col.gameObject;
    else
      isFalling = true;
  }
}
