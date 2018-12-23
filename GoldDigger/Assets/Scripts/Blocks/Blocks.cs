﻿using UnityEngine;

public class Blocks : MonoBehaviour
{
  [SerializeField] string blockName;

  [SerializeField] int hardness;

  [SerializeField] int health;

  [SerializeField] float spawnrate;

  [SerializeField] bool breakable = true;

  Sprite[] breakingSprites = new Sprite[3];

  GameObject breakObject;

  float fraction;

  void Start()
  {
    breakingSprites[0] = Resources.Load<Sprite>("Sprites/Blocks/Breaking/Break1");
    breakingSprites[1] = Resources.Load<Sprite>("Sprites/Blocks/Breaking/Break2");
    breakingSprites[2] = Resources.Load<Sprite>("Sprites/Blocks/Breaking/Break3");

    breakObject = transform.GetChild(0).gameObject;

    fraction = (float)health / breakingSprites.Length;
  }

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

    if (itemInfo[0] >= hardness && breakable)
    {
      health -= itemInfo[1];

      

      if (health < fraction * 3)
      {
        breakObject.GetComponent<SpriteRenderer>().sprite = breakingSprites[0];
      }
      if (health < fraction * 2)
      {
        breakObject.GetComponent<SpriteRenderer>().sprite = breakingSprites[1];
      }
      if (health < fraction * 1)
      {
        breakObject.GetComponent<SpriteRenderer>().sprite = breakingSprites[2];
      }

      if (health <= 0)
      {
        if(GetComponent<Ores>() != null || GetComponent<BoneHandler>() != null)
        {
          gameObject.SendMessage("SpawnOre");   
        }
        

        Destroy(gameObject);
      }
    }
  }
}