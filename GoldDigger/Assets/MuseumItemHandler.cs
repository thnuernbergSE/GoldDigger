using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MuseumItemHandler : MonoBehaviour {

  [SerializeField] int itemID;

  Inventory inventory;

  TextMeshProUGUI text;

  InventoryItems inventoryItem;

  [SerializeField] float researchTimer = 5;

  float startingTime;

  public float StartingTime
  {
    set { startingTime = value; }
  }

  bool isResearching = false;
  bool finishedResearch = false;

  string itemName;

  public void ActivateTimer()
  {
    isResearching = true;
  }

  public int ItemID => itemID;

  // Use this for initialization
  void Start () {
    inventory = GameObject.Find("Player").GetComponent<Inventory>();

    text = transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>();

    text.text = "???";

    itemName = BoneNames.Bones[itemID];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    if(!finishedResearch)
    {
      if (inventory.GetItemByName(BoneNames.Bones[itemID] + "_" + itemID) != null)
      {
        transform.GetChild(2).GetComponent<Button>().interactable = true;
        inventoryItem = inventory.GetItemByName(BoneNames.Bones[itemID] + "_" + itemID);
      }
      else
      {
        transform.GetChild(2).GetComponent<Button>().interactable = false;
      }

      if (isResearching)
      {
        updateResearchTimer();

        if (researchTimer + startingTime <= Time.time)
        {
          finishResearch();
          isResearching = false;
        }
      }
    }
	}

  void updateResearchTimer()
  {
    text.text = Mathf.Max((int)(startingTime + researchTimer - Time.time), 0) + "s";
    
  }

  void finishResearch()
  {
    finishedResearch = true;

    transform.GetChild(2).GetComponent<Button>().interactable = true;

    transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Collect";

    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;

    transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventoryItem.ItemName.Split('_')[2];
  }

  public void CollectMoney()
  {
    int amount; 
    Int32.TryParse(inventoryItem.ItemName.Split('_')[2], out amount);

    GameObject.Find("Player").GetComponent<Player>().AddMoney(amount);
  }
}
