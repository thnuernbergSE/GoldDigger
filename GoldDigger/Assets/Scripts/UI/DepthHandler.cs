using System;
using TMPro;
using UnityEngine;

public class DepthHandler : MonoBehaviour
{
  PlayerControl playerControl;

  TextMeshProUGUI text;

  // Use this for initialization
  void Start()
  {
    text = GetComponent<TextMeshProUGUI>();

    playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();

    if (playerControl == null)
    {
      throw new NullReferenceException("Player not found! - DepthHandler");
    }
  }

  // Update is called once per frame
  void Update()
  {
    text.text = (int) playerControl.GetYPosition - 2 + " m";
  }
}