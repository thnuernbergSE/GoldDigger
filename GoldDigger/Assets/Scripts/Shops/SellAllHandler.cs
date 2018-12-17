using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellAllHandler : MonoBehaviour
{

  Inventory inventory;
  GameObject item;


	// Use this for initialization
	void Start ()
	{
	  inventory = GameObject.Find("Player").GetComponent<Inventory>();

	  item = gameObject.transform.parent.gameObject.GetComponent<ShopItem>().GetItem;


	  if (inventory == null)
	  {
      throw new NullReferenceException("inventory is null - SellAllHandler.cs");
	  }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void OnClick()
  {
    var inventoryItem = new InventoryItems(InventoryItems.CobbleItem, item.GetComponent<OreItems>().ItemWeight);
    int amountOf = inventory.GetAmountOf(inventoryItem);
    if (inventory.Remove(inventoryItem))
    {
      Debug.Log("Removed Items: " + amountOf);
      //TODO: ADD MONEY TO PLAYER
    }
  }
}
