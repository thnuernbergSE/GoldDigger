using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandlerBackpack : MonoBehaviour
{

  [SerializeField] GameObject backpack;

  public GameObject Backpack => backpack;

	// Use this for initialization
	void Start ()
	{
	  var backpackHandler = backpack.GetComponent<BackpackHandler>();

    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Maximal Weight " + backpackHandler.MaxWeight;

	  transform.GetChild(4).GetComponent<Image>().sprite = backpackHandler.BackpackIcon;

	  transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = backpackHandler.Price.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
