using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

  Player player;

  [SerializeField]
  Sprite[] lifeSprite;

  GameObject stats;

  // Use this for initialization
  void Start ()
  {
    stats = GameObject.Find("Stats");
    player = GameObject.Find("Player").GetComponent<Player>();
  }
	
	// Update is called once per frame
	void Update ()
  {
    HandleHealth();
    HandleStamina();
	}

  private void HandleHealth()
  {
    for (int i = 0; i <= player.MaxHealth; i++)
    {
      if (player.CurrentHealth == i)
      {
        stats.transform.GetChild(0).GetComponent<Image>().sprite= lifeSprite[i];
      }
    }
  }

  private void HandleStamina()
  {
    for (int i = 0; i < player.MaxStamina; i++)
    {
      if (player.CurrentStamina == i)
      {
        stats.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = (float)i/player.MaxStamina;
      }
    }
  }
}
