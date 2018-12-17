using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellAllHandler : MonoBehaviour
{
  [SerializeField] GameObject sellAllText;

  string itemName;

  Inventory inventory;

  GameObject item;

  Button button;

	// Use this for initialization
	void Start ()
	{
	  inventory = GameObject.Find("Player").GetComponent<Inventory>();

	  item = gameObject.transform.parent.gameObject.GetComponent<ShopItem>().GetItem;

	  button = GetComponent<Button>();

    if (inventory == null)
	  {
      throw new NullReferenceException("inventory is null - SellAllHandler.cs");
	  }
	  if (item == null)
	  {
	    throw new NullReferenceException("item is null - SellAllHandler.cs");
	  }
	  if (button == null)
	  {
	    throw new NullReferenceException("button is null - SellAllHandler.cs");
	  }

	  itemName = item.GetComponent<OreItems>().ItemName;
	}
	
	// Update is called once per frame
	void Update () {
	  button.interactable = inventory.GetAmountOf(new InventoryItems(itemName)) != 0;

	  sellAllText.GetComponent<TextMeshProUGUI>().text =
	    (inventory.GetAmountOf(new InventoryItems(itemName)) *
	    item.GetComponent<OreItems>().ItemValue).ToString();
	}

  public void OnClick()
  {
    var inventoryItem = new InventoryItems(itemName, item.GetComponent<OreItems>().ItemWeight);
    int amountOf = inventory.GetAmountOf(inventoryItem);
    if (inventory.Remove(inventoryItem))
    {
      Debug.Log("Removed Items: " + amountOf);
      //TODO: ADD MONEY TO PLAYER
    }
  }
}
