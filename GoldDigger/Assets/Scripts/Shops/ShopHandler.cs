using UnityEngine;

public class ShopHandler : MonoBehaviour {

  GameObject thinkingCloud;
  // Use this for initialization
  void Start()
  {
    thinkingCloud = GameObject.Find("thinkCloud");
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
