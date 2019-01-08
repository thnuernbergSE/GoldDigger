using UnityEngine;

public class FoodItems
{
  public Sprite GetSprite { get; }

  public int Energy { get; }

  public string Name { get; }

  public FoodItems(string name)
  {
    GetSprite = Resources.Load<Sprite>("Sprites/Food/" + name);

    Name = name.ToString();

    var food = Resources.Load<GameObject>("Prefabs/Food/" + name);

    int energy = food.GetComponent<Food>().Energy;
    Energy = energy;

    if (GetSprite == null)
    {
      throw new System.Exception("Sprite can't be null: " + name + "- InventoryItems.cs");
    }
  }
}
