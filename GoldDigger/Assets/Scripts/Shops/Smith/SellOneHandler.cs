using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellOneHandler : MonoBehaviour
{

  [SerializeField] GameObject sellOneText;

  string itemName;

  Inventory inventory;
  GameObject item;

  InventoryItems inventoryItem;

  Button button;

  Player player;

  // Use this for initialization
  void Start () {
    inventory = GameObject.Find("Player").GetComponent<Inventory>();

    item = gameObject.transform.parent.gameObject.GetComponent<ShopItem>().GetItem;

    itemName = item.GetComponent<OreItems>().ItemName;

    button = GetComponent<Button>();

    player = GameObject.Find("Player").GetComponent<Player>();

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
  }

  void Update()
  {
    button.interactable = inventory.GetAmountOf(new InventoryItems(itemName)) != 0;

    sellOneText.GetComponent<TextMeshProUGUI>().text = item.GetComponent<OreItems>().ItemValue.ToString();
  }

  // Update is called once per frame
  public void OnClick()
  {
    var inventoryItem = new InventoryItems(itemName);

    if (inventory.Remove(inventoryItem, 1))
    {
      Debug.Log("Removed Items: " + 1);
      player.AddMoney(item.GetComponent<OreItems>().ItemValue);
    }
  }
}
