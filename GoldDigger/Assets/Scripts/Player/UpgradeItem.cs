using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
  [SerializeField] GameObject item;
  [SerializeField] int amount;

  public GameObject Item => item;

  public int Amount => amount;

  // Use this for initialization
  void Start()
  {
    if (item == null)
    {
      gameObject.SetActive(false);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (item != null)
    {
      gameObject.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;

      transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = amount.ToString();
    }
  }
}
