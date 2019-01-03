using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackHandler : MonoBehaviour
{

  [SerializeField] Sprite backpack;

  [SerializeField] Sprite backpackIcon;

  [SerializeField] int maxWeight;

  [SerializeField] int price;

  public int Price => price;

  public Sprite BackPack => backpack;

  public Sprite BackpackIcon => backpackIcon;

  public int MaxWeight => maxWeight;
}
