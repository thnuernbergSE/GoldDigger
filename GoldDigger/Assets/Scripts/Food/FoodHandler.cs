using UnityEngine;
using UnityEngine.UI;

public class FoodHandler : MonoBehaviour
{
  GameObject player;

  bool canUse;

  void Start ()
  {
    player = GameObject.Find("Player"); 
  }

	void Update ()
  {
   if (gameObject.GetComponent<Image>().sprite != null)
    {
      canUse = true;
    }

    gameObject.GetComponent<Button>().interactable = canUse;
  }

  public void OnClick()
  {
    bool done = true;

    var inventory = player.GetComponent<Inventory>();

    foreach (var item in inventory.GetFoodInventory)
    {
      if (item.GetSprite == gameObject.GetComponent<Image>().sprite && done)
      {
        player.GetComponent<Player>().AddStammina(item.Energy);

        inventory.RemoveFood(item);

        done = false;
      }
    }
  }
}
