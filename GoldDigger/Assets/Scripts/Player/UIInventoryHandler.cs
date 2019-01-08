using TMPro;
using UnityEngine;

public class UIInventoryHandler : MonoBehaviour
{

  GameObject player;

  TextMeshProUGUI text;

  Inventory inventory;

  // Use this for initialization
  void Start()
  {
    player = GameObject.Find("Player");

    text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

    inventory = player.GetComponent<Inventory>();
  }

  // Update is called once per frame
  void Update()
  {
    text.text = inventory.CurrentWeight + " | " + inventory.MaxWeight;
  }
}
