using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonHandler : MonoBehaviour
{
  readonly bool bought = false;

  GameObject pickaxe;

  GameObject player;

  int upgradeCost;

  GameObject upgradeItems;

  public List<KeyValuePair<InventoryItems, int>> UpgradeItemsList { get; } =
    new List<KeyValuePair<InventoryItems, int>>();

  // Use this for initialization
  void Start()
  {
    player = GameObject.Find("Player");

    upgradeCost = transform.parent.gameObject.GetComponent<UpgradeHandlerPickaxe>().UpgradeCost;

    pickaxe = transform.parent.gameObject.GetComponent<UpgradeHandlerPickaxe>().Pickaxe;

    upgradeItems = transform.parent.GetChild(5).gameObject;

    for (var i = 0; i < upgradeItems.transform.childCount; i++)
    {
      if (upgradeItems.transform.GetChild(i).GetComponent<UpgradeItem>().Item == null)
      {
        continue;
      }

      var inventoryItem = new InventoryItems(upgradeItems.transform.GetChild(i).GetComponent<UpgradeItem>().Item
        .GetComponent<OreItems>().ItemName);

      var itemAmount = upgradeItems.transform.GetChild(i).GetComponent<UpgradeItem>().Amount;

      UpgradeItemsList.Add(new KeyValuePair<InventoryItems, int>(inventoryItem, itemAmount));
    }
  }

  // Update is called once per frame
  void Update()
  {
    var inventory = player.GetComponent<Inventory>();
    var allItem = !(player.GetComponent<Player>().Money <= upgradeCost || bought);

    foreach (var keyValuePair in UpgradeItemsList)
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

    foreach (var keyValuePair in UpgradeItemsList)
    {
      inventory.Remove(keyValuePair.Key, keyValuePair.Value);
    }

    player.GetComponent<Player>().RemoveMoney(upgradeCost);
    player.GetComponent<Player>().Pickaxe = pickaxe;
  }
}