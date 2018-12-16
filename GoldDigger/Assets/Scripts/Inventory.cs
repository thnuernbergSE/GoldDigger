using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
  //InventorySlot inventorySlot;
  private bool isActive;

 

  private GameObject[] inventorySlots;
  private GameObject inventoryUI;

 

  [SerializeField]
  private ushort maxWeight = 10;

  private int currentWeight = 0;

  public ushort MaxWeight
  {
    get { return maxWeight; }
    set { maxWeight = value; }
  }

  List<KeyValuePair<InventoryItems, int>> inventory = new List<KeyValuePair<InventoryItems, int>>();

  public List<KeyValuePair<InventoryItems, int>> GetInventory
  {
    get { return inventory; }
  }

  public bool Add(InventoryItems item, int amountOf)
  {
    if (item == null)
    {
      throw new UnassignedReferenceException("OreItems equals null - Inventory.cs");
    }

    if (item.ItemWeight * amountOf + currentWeight <= maxWeight)
    {
      if (inventory.Count == 0)
      {
        inventory.Add(new KeyValuePair<InventoryItems, int>(item, amountOf));
      }
      else
      {
        for (int i = 0; i < inventory.Count; i++)
        {
          if (inventory[i].Key.ItemName == item.ItemName)
          {
            inventory[i] = new KeyValuePair<InventoryItems, int>(inventory[i].Key, inventory[i].Value + amountOf);
          }
          else
          {
            inventory.Add(new KeyValuePair<InventoryItems, int>(item, amountOf));
          }
        }
      }

      currentWeight += amountOf * item.ItemWeight;
      Debug.Log(currentWeight);

      Debug.Log(inventory);

      return true;
    }

    return false;
  }


  public bool Remove(InventoryItems item, int amountOf)
  {

    for (int i = 0; i < inventory.Count; i++)
    {
      if (inventory[i].Key.ItemName == item.ItemName)
      {
        if (inventory[i].Value < amountOf)
        {
          return false;
        }
        else if (inventory[i].Value == amountOf)
        {
          inventory.Remove(inventory[i]);

        }
        else
        {
          inventory[i] = new KeyValuePair<InventoryItems, int>(inventory[i].Key, inventory[i].Value - amountOf);
          currentWeight -= amountOf * item.ItemWeight;
        }
        return true;
      }
    }
    return false;
  }

  void Update()
  {
    //Debug.Log(inventory.Count);
    for (int i = 0; i < inventorySlots.Length; i++)
    {
      if (inventory.Count > i)
      {
        Debug.Log(i + "Items");
        if (inventory[i].Key != null)
        {

          inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].Key.GetSprite;
          inventorySlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].Value.ToString();

        }
      }

    }
  }

  void Start()
  {
    inventoryUI = GameObject.Find("SlotPanel");
    if (inventoryUI == null)
    {
      throw new MissingReferenceException("Missing Reference --- Inventory");
    }
    inventorySlots = new GameObject[inventoryUI.transform.childCount];
    for (int i = 0; i < inventorySlots.Length; i++)
    {
      inventorySlots[i] = inventoryUI.transform.GetChild(i).gameObject;
    }
    for (int i = 0; i < inventorySlots.Length; i++)
    {
      inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
      inventorySlots[i].transform.GetChild(1).GetComponent <TextMeshProUGUI>().text = null;
      
      
    }

  }




}
