using System;
using UnityEngine;

public class DepthArrowHandler : MonoBehaviour
{
  const int ImageOffset = 25;
  const int ImageSize = 325;
  GameObject depthArrow;
  float depthArrowPosition;

  MapGenerator mapGenerator;
  PlayerControl playerControl;

  float playerPosPercentage;
  int worldHeight;

  // Use this for initialization
  void Start()
  {
    mapGenerator = GameObject.Find("MapGenerator").GetComponent<MapGenerator>();
    if (mapGenerator == null)
    {
      throw new NullReferenceException("No MapGenerator found! - DepthArrowHandler");
    }

    playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    if (playerControl == null)
    {
      throw new NullReferenceException("No PlayerControl found! - DepthArrowHandler");
    }

    worldHeight = mapGenerator.GetWorldHeight;
    depthArrow = GameObject.Find("DepthArrow");
    if (depthArrow == null)
    {
      throw new NullReferenceException("No DepthArrow found! - DepthArrowHandler");
    }
  }


  // Update is called once per frame
  void Update()
  {
    playerPosPercentage = Mathf.Max(-playerControl.GetYPosition, 0) * 100 / worldHeight;
    depthArrowPosition = -playerPosPercentage / 100 * ImageSize - ImageOffset;
    depthArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-9.7f, depthArrowPosition);
  }
}