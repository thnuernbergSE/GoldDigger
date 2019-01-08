using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour
{
  MuseumItemHandler museumItem;

  Inventory inventory;

  int itemID;

  // Use this for initialization
  void Start()
  {
    museumItem = transform.parent.GetComponent<MuseumItemHandler>();

    inventory = GameObject.Find("Player").GetComponent<Inventory>();

    itemID = transform.parent.GetComponent<MuseumItemHandler>().ItemID;
  }

  public void OnClick()
  {
    if (transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Collect")
    {
      transform.parent.GetComponent<MuseumItemHandler>().CollectMoney();

      transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Collected";

      GetComponent<Button>().interactable = false;
    }

    else
    {
      inventory.Remove(inventory.GetItemByName(BoneNames.Bones[itemID] + "_" + itemID));

      transform.parent.GetComponent<MuseumItemHandler>().StartingTime = Time.time;

      museumItem.ActivateTimer();
    }
  }
}
