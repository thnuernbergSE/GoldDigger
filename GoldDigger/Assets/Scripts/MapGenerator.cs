using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapGenerator : MonoBehaviour
{

  public GameObject GrassBlock;
  public GameObject DirtBlock;
  public GameObject StoneBlock;
  public GameObject CopperBlock;
  public GameObject CoalBlock;

  int worldHeight;

  // Use this for initialization
  void Start()
  {
    //for(int i = 0; i <= 50; i++)
    //{
    //    for (int j = 0; j <= 10; j++)
    //    {
    //        Instantiate(Resources.Load("Prefabs/Blocks/DirtBlock", typeof(GameObject)), new Vector2(i, -j), Quaternion.identity);
    //    }
    //}
    createGrassLayer(50, 10);
    createStoneLayer1(50, 500);
    
  }

  // Update is called once per frame
  void Update()
  {

  }

  void createStoneLayer1(int width, int height)
  {
    for (int i = 0; i < height; i++)
    {
      
      for (int j = 0; j < width; j++)
      {
        GameObject active = null;

        float rand = Random.Range(0f, 100f);
        if (rand <= 4)
        {
          active = CoalBlock;
        }
        else if (rand <= 7)
        {
          active = CopperBlock;
        }

        if (active == null)
        {
          active = StoneBlock;
        }
        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity);
      }
    }
  }

  void createGrassLayer(int width, int height)
  {

    for (int i = 0; i < height; i++)
    {
      GameObject active;
      for (int j = 0; j < width; j++)
      {
        int rand = Random.Range(0, 100);
        if (rand >= 5)
        {
          active = DirtBlock;
        }
        else
        {
          active = StoneBlock;
        }
        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity);
      }
    }
    worldHeight += height;
  }
}
