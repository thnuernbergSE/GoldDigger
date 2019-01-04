using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonBackpack : MonoBehaviour
{
  bool alreadyBought = false;
  GameObject player;
  UpgradeHandlerBackpack upgradeHandler;

  void Start()
  {
    player = GameObject.Find("Player");
    upgradeHandler = transform.parent.gameObject.GetComponent<UpgradeHandlerBackpack>();
  }

  void Update()
  {
    GetComponent<Button>().interactable = player.GetComponent<Player>().Money >=
                                          upgradeHandler.Backpack.GetComponent<BackpackHandler>().Price && !alreadyBought;
  }

	// Use this for initialization
  public void OnClick()
  {
    player.GetComponent<Player>().RemoveMoney(upgradeHandler.Backpack.GetComponent<BackpackHandler>().Price);
    player.GetComponent<Inventory>().ActiveBackpack = upgradeHandler.Backpack;

    alreadyBought = true;
  }
}
