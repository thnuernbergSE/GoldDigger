using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems
{
  public const string AluminumItem = "Aluminium";
  public const string CoalItem = "Coal";
  public const string CobaltItem = "Cobalt";
  public const string CobbleItem = "Cobble";


  public Sprite GetSprite { get; }

  public string ItemName { get; }

  public int ItemWeight { get; }


  public InventoryItems(string name, int weight)
  {
    GetSprite = Resources.Load<Sprite>("Sprites/Items/" + name);
    ItemName = name;
    ItemWeight = weight;

    if (GetSprite == null)
    {
      throw new NullReferenceException("Sprite can't be null - InventoryItems.cs");
    }
  }
}
