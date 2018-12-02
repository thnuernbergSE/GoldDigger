using System.Xml.Serialization;
using UnityEngine;

public class Ores : MonoBehaviour {

  [SerializeField]
  GameObject droppingItem;

  Blocks block;

  void Start()
  {
    block = gameObject.GetComponent<Blocks>();
  }

  void Update()
  {
    if (block.GetHealth < 0)
    {
      Instantiate(droppingItem, gameObject.transform.position, Quaternion.identity);
    }
  }
}
