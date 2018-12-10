using System;
using UnityEngine;
using UnityEngine.UI;

public class DepthHandler : MonoBehaviour
{
  PlayerControl playerControl;

  Text text;

  // Use this for initialization
  void Start()
  {
    text = GetComponent<Text>();
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