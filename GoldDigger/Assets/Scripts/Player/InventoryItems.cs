using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems
{
  string itemName;
  Sprite sprite;
  int itemWeight;


  public Sprite GetSprite => sprite;
  public string ItemName => itemName;
  public int ItemWeight => itemWeight;
  

  public InventoryItems(string name, int weight)
  {
    sprite = Resources.Load<Sprite>("Sprites/Items/" + name);
    itemName = name;
    itemWeight = weight;

    if (sprite == null)
    {
      throw new NullReferenceException("Sprite can't be null - InventoryItems.cs");
    }
  }

	
	// Update is called once per frame
	void Update () {
		
	}
}
