using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMoneyHandler : MonoBehaviour
{


  Player player;
  TextMeshProUGUI text;

	// Use this for initialization
	void Start ()
	{
	  player = GameObject.Find("Player").GetComponent<Player>();
	  text = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	  text.text = player.Money.ToString();
	}
}
