using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Audio.Google;
using UnityEngine.Experimental.U2D;
using UnityEngine.UI;

public class BuyButtonHandler : MonoBehaviour
{

  List<KeyValuePair<InventoryItems, int>> upgradeItemsList = new List<KeyValuePair<InventoryItems, int>>();

  public List<KeyValuePair<InventoryItems, int>> UpgradeItemsList => upgradeItemsList;

  GameObject player;

  GameObject upgradeItems;

  GameObject pickaxe;

  int upgradeCost;

  bool bought = false;

  // Use this for initialization
  void Start ()
  {
    player = GameObject.Find("Player");

    upgradeCost = transform.parent.gameObject.GetComponent<UpgradeHandler>().UpgradeCost;

    pickaxe = transform.parent.gameObject.GetComponent<UpgradeHandler>().Pickaxe;

    upgradeItems = transform.parent.GetChild(5).gameObject;
    for (var i = 0; i < upgradeItems.transform.childCount; i++)
    {

      if (upgradeItems.transform.GetChild(i).GetComponent<UpgradeItem>().Item == null)
      {
        continue;
      }

      var inventoryItem = new InventoryItems(upgradeItems.transform.GetChild(i).GetComponent<UpgradeItem>().Item.GetComponent<OreItems>().ItemName);

      var itemAmount = upgradeItems.transform.GetChild(i).GetComponent<UpgradeItem>().Amount;

      upgradeItemsList.Add(new KeyValuePair<InventoryItems, int>(inventoryItem, itemAmount));
    }

    foreach (var keyValuePair in upgradeItemsList)
    {
      Debug.Log(keyValuePair.Key.ItemName + " " + keyValuePair.Value);
    }

  }
	
	// Update is called once per frame
	void Update ()
	{
	  var inventory = player.GetComponent<Inventory>();
	  var allItem = !(player.GetComponent<Player>().Money <= upgradeCost || bought);

	  foreach (var keyValuePair in upgradeItemsList)
	  {
	    if (!inventory.Contains(keyValuePair.Key, keyValuePair.Value))
	    {
	      allItem = false;
	    }
    }

	  gameObject.GetComponent<Button>().interactable = allItem;
  }

  public void OnClick()
  {
    var inventory = player.GetComponent<Inventory>();

    foreach (var keyValuePair in upgradeItemsList)
    {
      inventory.Remove(keyValuePair.Key, keyValuePair.Value);

      player.GetComponent<Player>().Pickaxe = pickaxe;
    }
  }
}
