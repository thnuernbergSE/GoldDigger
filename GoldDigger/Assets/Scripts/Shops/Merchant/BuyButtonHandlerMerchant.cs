using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Audio.Google;
using UnityEngine.Experimental.U2D;
using UnityEngine.UI;

public class BuyButtonHandlerMerchant : MonoBehaviour
{

  List<InventoryItems> buyItemsList = new List<InventoryItems>();

  public List<InventoryItems> ItemsList => buyItemsList;

  MerchantContent content;

  GameObject player;

  GameObject buyItems;

  GameObject food;

  int cost;

  bool spaceAvailable;

  void Start()
  { 
    spaceAvailable = true;
    player = GameObject.Find("Player");
    cost = transform.parent.gameObject.GetComponent<BuyHandler>().Cost;
    food = transform.parent.gameObject.GetComponent<BuyHandler>().Food;

    for (int i = 0; i < content.Food.Length; i++)
    {
       buyItemsList[i] = content.Food[i].GetComponent<InventoryItems>();
    }

  }

  // Update is called once per frame
  void Update()
  {
    var inventory = player.GetComponent<Inventory>();
    var allItem = !(player.GetComponent<Player>().Money <= cost);

    gameObject.GetComponent<Button>().interactable = allItem;
  }

  public void OnClick()
  {
    var inventory = player.GetComponent<Inventory>();
    for (int i = 0; i < buyItemsList.Count; i++)
    {
      
      if (spaceAvailable)
      {
        spaceAvailable = inventory.AddFood(buyItemsList[i]);
      }
    }
  }
}
