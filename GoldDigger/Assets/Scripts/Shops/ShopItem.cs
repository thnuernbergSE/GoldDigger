using System;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
  [SerializeField] GameObject item;

  TextMeshProUGUI itemNameText;

  GameObject player;

  public GameObject GetItem => item;

  // Use this for initialization
  void Start()
  {
    itemNameText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

    player = GameObject.Find("Player");

    if (itemNameText == null)
    {
      throw new NullReferenceException("itemNameText is null - ShopItem.cs");
    }

    if (player == null)
    {
      throw new NullReferenceException("player is null - ShopItem.cs");
    }
  }

  // Update is called once per frame
  void Update()
  {
    var itemName = item.GetComponent<OreItems>().ItemName;

    itemNameText.text = itemName + " x" + player.GetComponent<Inventory>().GetAmountOf(new InventoryItems(itemName));
  }
}
