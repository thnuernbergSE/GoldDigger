using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonHandlerMerchant : MonoBehaviour
{
  List<InventoryItems> buyItemsList = new List<InventoryItems>();

  public List<InventoryItems> ItemsList => buyItemsList;

  GameObject player;

  GameObject food;

  int cost;

  void Start()
  {
    player = GameObject.Find("Player");

    cost = transform.parent.gameObject.GetComponent<BuyHandler>().Cost;

    food = transform.parent.gameObject.GetComponent<BuyHandler>().Food;
  }

  // Update is called once per frame
  void Update()
  {
    var allItem = (player.GetComponent<Player>().Money >= cost);

    gameObject.GetComponent<Button>().interactable = allItem;
  }

  public void OnClick()
  {
    var inventory = player.GetComponent<Inventory>();

    if (inventory.AddFood(food) && player.GetComponent<Player>().RemoveMoney(cost))
    {
      Debug.Log("Food_Added-BuyhandlerMerchant.cs");
    }
  }
}
