using TMPro;
using UnityEngine;

public class UpgradeHandlerPickaxe : MonoBehaviour
{

  [SerializeField] GameObject pickaxe;

  int upgradeCost;

  public int UpgradeCost => upgradeCost;

  public GameObject Pickaxe => pickaxe;

  public static int caseSwitchCursor = 0;

  // Use this for initialization
  void Start()
  {
    upgradeCost = pickaxe.GetComponent<Pickaxe>().ItemCost;

    transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = upgradeCost.ToString();

    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pickaxe.GetComponent<Pickaxe>().Name;

    caseSwitchCursor++;
  }
}
