using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItems {


  public Sprite GetSprite { get; }

  public int Energy { get; }


  public FoodItems(string name,int energy = 0)
  {
    GetSprite = Resources.Load<Sprite>("Sprites/Food/" + name);
    Energy = energy;

    if (GetSprite == null)
    {
      throw new System.Exception("Sprite can't be null: " + name + "- InventoryItems.cs");
    }
  }
}
