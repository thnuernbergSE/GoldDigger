using UnityEngine;

public class Ores : MonoBehaviour {

  [SerializeField]
  GameObject droppingItem;

  void OnDestroy()
  {
    Instantiate(droppingItem, gameObject.transform.position, Quaternion.identity);
  }
}
