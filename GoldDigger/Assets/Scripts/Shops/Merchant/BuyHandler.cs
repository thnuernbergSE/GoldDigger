using TMPro;
using UnityEngine;

public class BuyHandler : MonoBehaviour
{
  [SerializeField] GameObject food;

  int cost;

  public int Cost => cost;

  Sprite sprite;

  Sprite GetSprite => sprite;

  public GameObject Food => food;

  // Use this for initialization
  void Start()
  {

    cost = food.GetComponent<Food>().Cost;

    sprite = food.GetComponent<Food>().GetSprite;

    transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = Cost.ToString();

    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = food.GetComponent<Food>().Name;
  }
}
