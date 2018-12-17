using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour {

  GameObject thinkingCloud;
  // Use this for initialization
  void Start()
  {
    thinkingCloud = GameObject.Find("thinkCloud");
    thinkingCloud.SetActive(false);
  }

  // Update is called once per frame

  void OnTriggerEnter2D(Collider2D other)
  {
    thinkingCloud.SetActive(true);
  }

  void OnTriggerExit2D(Collider2D other)
  {
    thinkingCloud.SetActive(false);
  }
}
