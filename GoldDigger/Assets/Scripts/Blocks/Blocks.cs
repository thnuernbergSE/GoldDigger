﻿using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Blocks : MonoBehaviour
{
  [SerializeField] string blockName;

  [SerializeField] int hardness;

  [SerializeField] int health;

  [SerializeField] float spawnrate;

  [SerializeField] bool breakable = true;

  void breakBlock()
  {
    Destroy(gameObject);
    //TODO: play break Animation
  }

  public int Health
  {
    get { return health; }
    set { health = value; }
  }

  public int Hardness
  {
    get { return hardness; }
    set { hardness = value; }
  }

  public float Spawnrate => spawnrate;

  public void ReceiveDamage(int[] itemInfo)
  {

    if (itemInfo.Length != 2)
    {
      throw new System.Exception("ReceiveDamage not right amount of arguments");

    }

    //Debug.Log("Hardness: " + itemInfo[0] + ":" + hardness + " Health: " + itemInfo[1] + ":" + health);
    if (itemInfo[0] >= hardness && breakable)
    {
      health -= itemInfo[1];

      if (health <= 0)
      {
        var ores = gameObject.GetComponent<Ores>();
        if (ores != null)
        {
          ores.SpawnOre();
        }

        Destroy(gameObject);
      }
    }
  }
}