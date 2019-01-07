using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
  int currentWeight;

  GameObject[] inventorySlots;
  GameObject[] foodSlots;

  GameObject inventoryUI;

  GameObject inventory;

  const int foodSpace = 4;

  [SerializeField] GameObject acitveBackpack;

  public GameObject ActiveBackpack
  {
    set { acitveBackpack = value; }
  }
  bool isActive;


  [SerializeField] int maxWeight;

  public int MaxWeight
  {
    get { return maxWeight; }
    set { maxWeight = value; }
  }

  public int CurrentWeight => currentWeight;

  public List<KeyValuePair<InventoryItems, int>> GetInventory { get; } = new List<KeyValuePair<InventoryItems, int>>();

  public List<FoodItems> GetFoodInventory { get; } = new List<FoodItems>();

  public bool Add(InventoryItems item, int amountOf)
  {
    var itemAdded = false;

    if (item == null)
    {
      throw new UnassignedReferenceException("OreItems equals null - Inventory.cs");
    }

    if (item.ItemWeight * amountOf + currentWeight <= maxWeight)
    {
      if (GetInventory.Count == 0)
      {
        GetInventory.Add(new KeyValuePair<InventoryItems, int>(item, amountOf));
        Debug.Log("Inventory Count == 0");
      }
      else
      {
        for (var i = 0; i < GetInventory.Count; i++)
        {
          if (GetInventory[i].Key.ItemName == item.ItemName)
          {
            GetInventory[i] =
              new KeyValuePair<InventoryItems, int>(GetInventory[i].Key, GetInventory[i].Value + amountOf);
            itemAdded = true;
          }
        }

        if (!itemAdded)
        {
          GetInventory.Add(new KeyValuePair<InventoryItems, int>(item, amountOf));
        }
      }

      currentWeight += amountOf * item.ItemWeight;
      Debug.Log(currentWeight);
      return true;
    }

    return false;
  }

  public bool Remove(InventoryItems item)
  {
    for (var i = 0; i < GetInventory.Count; i++)
    {
      if (GetInventory[i].Key.ItemName == item.ItemName)
      {
        currentWeight -= GetInventory[i].Key.ItemWeight * GetInventory[i].Value;
        GetInventory.Remove(GetInventory[i]);

        return true;
      }
    }

    return false;
  }

  public bool Remove(InventoryItems item, int amountOf)
  {
    for (var i = 0; i < GetInventory.Count; i++)
    {
      if (GetInventory[i].Key.ItemName == item.ItemName)
      {
        if (GetInventory[i].Value < amountOf)
        {
          return false;
        }

        if (GetInventory[i].Value == amountOf)
        {
          currentWeight -= GetInventory[i].Key.ItemWeight * amountOf;
          GetInventory.Remove(GetInventory[i]);
        }
        else
        {
          currentWeight -= GetInventory[i].Key.ItemWeight * amountOf;
          GetInventory[i] =
            new KeyValuePair<InventoryItems, int>(GetInventory[i].Key, GetInventory[i].Value - amountOf);
        }

        return true;
      }
    }

    return false;
  }

  public InventoryItems GetItemByName(string name)
  {
    foreach (var inventoryItem in GetInventory)
    {
      if (inventoryItem.Key.ItemName.Contains(name))
      {
        return inventoryItem.Key;
      }
    }

    return null;
  }

  public bool Contains(InventoryItems item, int amountOf)
  {
    foreach (var inventoryItem in GetInventory)
    {
      if (inventoryItem.Key.ItemName == item.ItemName)
      {
        if (inventoryItem.Value >= amountOf)
        {
          return true;
        }
      }
    }

    return false;
  }

  public int GetAmountOf(InventoryItems item)
  {
    foreach (var inventoryItem in GetInventory)
    {
      if (inventoryItem.Key.ItemName == item.ItemName)
      {
        return inventoryItem.Value;
      }
    }

    return 0;
  }

  void Update()
  {
    updateSlotPanel();
    updateFoodPanel();
  }

  void updateSlotPanel()
  {
    inventory.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentWeight.ToString();
    inventory.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = maxWeight.ToString();


    inventory.GetComponent<Image>().sprite =
      acitveBackpack.GetComponent<BackpackHandler>().BackPack;
    maxWeight = acitveBackpack.GetComponent<BackpackHandler>().MaxWeight;

    for (var i = 0; i < inventorySlots.Length; i++)
    {
      inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
      inventorySlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
      if (GetInventory.Count > i)
      {
        if (GetInventory[i].Key != null)
        {
          inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = GetInventory[i].Key.GetSprite;
          inventorySlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
            GetInventory[i].Value.ToString();
        }
      }
    }
  }

  void updateFoodPanel()
  {
    for (var i = 0; i < foodSlots.Length; i++)
    {
      foodSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;

      if (GetFoodInventory.Count > i)
      {
        if (GetFoodInventory[i] != null)
        {
          foodSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = GetFoodInventory[i].GetSprite;
        }
      }
    }
  }


  void Start()
  {
    startSlotPanel();
    FoodPanelStart();
  }

  void startSlotPanel()
  {
    maxWeight = acitveBackpack.GetComponent<BackpackHandler>().MaxWeight;

    inventory = GameObject.Find("Inventory");

    inventory.GetComponent<Image>().sprite = acitveBackpack.GetComponent<BackpackHandler>().BackPack;

    inventoryUI = GameObject.Find("SlotPanel");
    if (inventoryUI == null)
    {
      throw new MissingReferenceException("Missing Reference --- Inventory");
    }

    inventorySlots = new GameObject[inventoryUI.transform.childCount];
    for (var i = 0; i < inventorySlots.Length; i++)
    {
      inventorySlots[i] = inventoryUI.transform.GetChild(i).gameObject;
    }
  }

  void FoodPanelStart()
  {
    inventoryUI = GameObject.Find("FoodPanel");
    if (inventoryUI == null)
    {
      throw new MissingReferenceException("Missing Reference --- Inventory");
    }

    foodSlots = new GameObject[inventoryUI.transform.childCount];
    for (var i = 0; i < foodSlots.Length; i++)
    {
      foodSlots[i] = inventoryUI.transform.GetChild(i).gameObject;
    }
  }

  public bool AddFood(GameObject item)
  {
    var foodItems = new FoodItems(item.name);
    if (foodItems == null)
    {
      throw new UnassignedReferenceException("FoodItem equals null - Inventory.cs");
    }

    if (GetFoodInventory.Count < foodSpace)
    {
      GetFoodInventory.Add(foodItems);
      Debug.Log(item.name);
      return true;
    }

    return false;
  }

  public bool RemoveFood(FoodItems item)
  {
    for (var i = 0; i < GetFoodInventory.Count; i++)
    {
      
      if (GetFoodInventory[i].Name == item.Name)
      {
        GetFoodInventory.Remove(GetFoodInventory[i]);
        return true;
      }
    }

    return false;
  }
}