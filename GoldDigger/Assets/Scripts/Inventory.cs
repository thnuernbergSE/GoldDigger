using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  //InventorySlot inventorySlot;
  private bool isAktive;

  [SerializeField]
  private GameObject[] inventorySlots;

  

  [SerializeField]
  private ushort maxWeight = 5;

  private ushort currentWeight;

  public ushort MaxWeight
  {
    get { return maxWeight; }
    set { maxWeight = value; }
  }

  List<KeyValuePair<GameObject, int>> inventory = new List<KeyValuePair<GameObject, int>>();

  public List<KeyValuePair<GameObject, int>> GetInventory
  {
    get { return inventory; }
  }

  public bool Add(GameObject item, int amountOf)
  {

    OreItems oreItems = item.GetComponent<OreItems>();

    if (oreItems == null)
    {
      throw new UnassignedReferenceException("OreItems equals null - Inventory.cs");
    }

    if (oreItems.ItemWeight + currentWeight <= maxWeight)
    {
      for (int i = 0; i < inventory.Count; i++)
      {

        if (inventory[i].Key.GetComponent<OreItems>().ItemName == oreItems.ItemName)
        {
          
          inventory[i] = new KeyValuePair<GameObject, int>(inventory[i].Key, inventory[i].Value + amountOf);
          //inventorySlot.Add(amountOf);//item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
          currentWeight += oreItems.ItemWeight;
          inventory.Add(new KeyValuePair<GameObject, int>(item, amountOf));
          //inventorySlot.Add(item);
          
        }
      }
      return true;
    }
    return false;
  }
  public bool Remove(GameObject item, int amountOf)
  {
    OreItems oreItems = item.GetComponent<OreItems>();

    for (int i = 0; i < inventory.Count; i++)
    {
      if (inventory[i].Key.GetComponent<OreItems>().ItemName == oreItems.ItemName)
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
          inventory[i] = new KeyValuePair<GameObject, int>(inventory[i].Key, inventory[i].Value - amountOf);
        }
        return true;
      }
    }
    return false;
  }

  void Update()
  {
    for (int i = 0; i < inventorySlots.Length; i++)
    {
      if (inventory[i].Key != null)
      {
        inventorySlots[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = inventory[i].Key.GetComponent<SpriteRenderer>().sprite;
      }
    }
  }




}