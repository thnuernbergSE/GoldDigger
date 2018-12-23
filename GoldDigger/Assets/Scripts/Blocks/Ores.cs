using System.Xml.Serialization;
using UnityEngine;

public class Ores : MonoBehaviour {

  [SerializeField] GameObject droppingItem;

  public void SpawnOre()
  {
    Instantiate(droppingItem, gameObject.transform.position, Quaternion.identity);
  }
}
