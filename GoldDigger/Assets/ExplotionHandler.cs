using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionHandler : MonoBehaviour {
  int radius = 3;
	// Use this for initialization
	void Start () {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
    foreach (Collider2D nearbyObject in colliders)
    {
      if (nearbyObject == null || nearbyObject.gameObject.tag != "Blocks")
      {
        continue;
      }
      Destroy(nearbyObject.gameObject);
    }
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
