using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneHandler : MonoBehaviour
{
  static int boneCounter = 0;

  [SerializeField] int id;

  [SerializeField] int price = 10;
  [SerializeField] GameObject bone;

  public void SpawnOre()
  {
    var spawnedBone = Instantiate(bone, gameObject.transform.position, Quaternion.identity);

    var boneItem = spawnedBone.GetComponent<OreItems>();

    boneItem.ItemName = BoneNames.Bones[id] + "_" + id + "_" + price;
  }

  // Use this for initialization
  void Start () {
    id = boneCounter++;

    price *= (int) -transform.position.y;
	}
}
