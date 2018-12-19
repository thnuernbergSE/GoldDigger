using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

  [SerializeField] GameObject pickaxe;

  int upgradeCost;

  public int UpgradeCost => upgradeCost;

  public GameObject Pickaxe => pickaxe;

	// Use this for initialization
	void Start ()
	{
	  upgradeCost = pickaxe.GetComponent<Pickaxe>().ItemCost;

	  transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = upgradeCost.ToString();

	  transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pickaxe.GetComponent<Pickaxe>().Name;
	}
	
	// Update is called once per frame
	void Update ()
	{
	  
	}
}
