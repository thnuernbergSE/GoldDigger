using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreItems : MonoBehaviour
{
  [SerializeField] string itemName;
  [SerializeField] int itemValue;
  [SerializeField] int itemWeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

  void OnTriggerEnter2D(Collider2D col)
  {
    if (col.tag == "Player")
    {
      Debug.Log("test");
      //TODO: ADD ITEM TO PLAYER INVENTORY AND DESTROY
    }
  }
}
