using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  [SerializeField] private ushort maxWeight = 5;

  private int currentWeight;

  public ushort MaxWeight
  {
    get { return maxWeight; }
    set { maxWeight = value; }
  }

  List<KeyValuePair<string, int>> inventory = new List<KeyValuePair<string, int>>();



  public bool Add(GameObject item, int amountOf)
  {

    OreItems oreItems = item.GetComponent<OreItems>();

    if (oreItems == null)
    {
      throw new UnassignedReferenceException("OreItems equals null - Inventory.cs");
    }

    if (oreItems.ItemWeight * amountOf + currentWeight <= maxWeight)
    {
      for (int i = 0; i < inventory.Count; i++)
      {

        if (inventory[i].Key == oreItems.ItemName)
        {
          inventory[i] = new KeyValuePair<string, int>(inventory[i].Key, inventory[i].Value + amountOf);
        }
        else
        {
          currentWeight += oreItems.ItemWeight;
          inventory.Add(new KeyValuePair<string, int>(oreItems.ItemName, amountOf));
        }
      }

      currentWeight += amountOf * oreItems.ItemWeight;
      Debug.Log(currentWeight);

      return true;
    }

    return false;
  }

  public bool Remove(GameObject item, int amountOf)
  {
    OreItems oreItems = item.GetComponent<OreItems>();

    for (int i = 0; i < inventory.Count; i++)
    {
      if (inventory[i].Key == oreItems.ItemName)
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
          inventory[i] = new KeyValuePair<string, int>(inventory[i].Key, inventory[i].Value - amountOf);
        }
        currentWeight -= amountOf * oreItems.ItemWeight;
        Debug.Log(currentWeight);
        return true;
      }
    }

    return false;
  }



}