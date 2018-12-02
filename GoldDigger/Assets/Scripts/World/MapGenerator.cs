using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  public GameObject AluminumBlock;
  public GameObject CoalBlock;
  public GameObject CobaltBlock;
  public GameObject CobbleBlock;
  public GameObject CopperBlock;
  public GameObject DiamondBlock;
  public GameObject DirtBlock;
  public GameObject GoldBlock;
  public GameObject GrassBlock;
  public GameObject IronBlock;
  public GameObject LithiumBlock;
  public GameObject PlatinumBlock;
  public GameObject RubyBlock;
  public GameObject SilverBlock;
  public GameObject StoneBlock;
  public GameObject TinBlock;
  public GameObject TitaniumBlock;

  public GameObject WorldBackground;

  int worldHeight;
  const int worldWidth = 50;

  public static int GetWorldWidth => worldWidth;

  float GetSpawnRate(GameObject block)
  {
    var blocksClass = block.GetComponent<Blocks>();
    
    return blocksClass.Spawnrate;
  }

  void setWorldBackground()
  {
    var background = Instantiate(WorldBackground, new Vector2(worldWidth / 2f - 0.5f, -worldHeight / 2f -  13f), Quaternion.identity);

    var spriteRenderer = background.GetComponent<SpriteRenderer>();

    spriteRenderer.size = new Vector2(worldWidth + 30, worldHeight + 30);
  }
  


  void createStoneLayer1(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      
      for (var j = 0; j < width; j++)
      {
        GameObject active = StoneBlock;

        float spawnRate = 0;

        var rand = Random.Range(0f, 100f);

        if (rand <= (spawnRate += GetSpawnRate(CoalBlock)))
        {
          active = CoalBlock;
        }
        else if (rand <= (spawnRate + GetSpawnRate(CopperBlock)))
        {
          active = CopperBlock;
        }

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }

    worldHeight += height;
  }

  void createDirtLayer(int width, int height)
  {

    for (var i = 0; i < height; i++)
    {
      
      for (var j = 0; j < width; j++)
      {
        var rand = Random.Range(0, 100);

        var active = rand >= GetSpawnRate(CobbleBlock) ? DirtBlock : CobbleBlock;

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }
    worldHeight += height;
  }

  void Start()
  {
    createGrassLayer(worldWidth, 1);
    createDirtLayer(worldWidth, 10);
    createStoneLayer1(worldWidth, 20);
    createIronTinLayer(worldWidth, 20);
    createSilverAluminumLayer(worldWidth, 20);
    createGoldLithiumLayer(worldWidth, 20);
    createRubyCobaltLayer(worldWidth, 20);
    createDiamondLayer(worldWidth, 20);
    createPlatinumLayer(worldWidth, 20);
    createTitaniumLayer(worldWidth, 20);
    setWorldBackground();
  }

  void createTitaniumLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      for (var j = 0; j < width; j++)
      {
        var random = Random.Range(0f, 100f);
        var active = StoneBlock;
        var spawnRate = 0f;

        if (random <= (spawnRate += GetSpawnRate(DiamondBlock)))
        {
          active = DiamondBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(TitaniumBlock)))
        {
          active = TitaniumBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(PlatinumBlock)))
        {
          active = PlatinumBlock;
        }

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }

    worldHeight += height;
  }

  void createPlatinumLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      for (var j = 0; j < width; j++)
      {
        var random = Random.Range(0f, 100f);
        var active = StoneBlock;
        var spawnRate = 0f;

        if (random <= (spawnRate += GetSpawnRate(DiamondBlock)))
        {
          active = DiamondBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(CobaltBlock)))
        {
          active = CobaltBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(PlatinumBlock)))
        {
          active = PlatinumBlock;
        }

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }

    worldHeight += height;
  }

  void createDiamondLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      for (var j = 0; j < width; j++)
      {
        var random = Random.Range(0f, 100f);
        var active = StoneBlock;
        var spawnRate = 0f;

        if (random <= (spawnRate += GetSpawnRate(RubyBlock)))
        {
          active = RubyBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(CobaltBlock)))
        {
          active = CobaltBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(DiamondBlock)))
        {
          active = DiamondBlock;
        }

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }

    worldHeight += height;
  }

  void createRubyCobaltLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      for (var j = 0; j < width; j++)
      {
        var random = Random.Range(0f, 100f);
        var active = StoneBlock;
        var spawnRate = 0f;

        if (random <= (spawnRate += GetSpawnRate(RubyBlock)))
        {
          active = RubyBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(CobaltBlock)))
        {
          active = CobaltBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(GoldBlock) / 2))
        {
          active = GoldBlock;
        }

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }

    worldHeight += height;
  }

  void createGrassLayer(int width, int height)
  {
    for (var i = 0; i < width; i++)
    {
      Instantiate(GrassBlock, new Vector2(i, 1), Quaternion.identity, GameObject.Find("World").transform);
    }
  }

  void createGoldLithiumLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      for (var j = 0; j < width; j++)
      {
        var random = Random.Range(0f, 100f);
        var active = StoneBlock;
        var spawnRate = 0f;

        if (random <= (spawnRate += GetSpawnRate(GoldBlock)))
        {
          active = GoldBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(LithiumBlock)))
        {
          active = LithiumBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(SilverBlock)))
        {
          active = SilverBlock;
        }

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }

    worldHeight += height;
  }

  void createSilverAluminumLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      for (var j = 0; j < width; j++)
      {
        var random = Random.Range(0f, 125f);
        var active = StoneBlock;
        var spawnRate = 0f;

        if (random <= (spawnRate += GetSpawnRate(SilverBlock)))
        {
          active = SilverBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(AluminumBlock)))
        {
          active = AluminumBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(CoalBlock) / 2))
        {
          active = CoalBlock;
        }

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }

    worldHeight += height;
  }

  void createIronTinLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      for (var j = 0; j < width; j++)
      {
        var random = Random.Range(0f, 100f);
        var active = StoneBlock;
        var spawnRate = 0f;

        if (random <= (spawnRate += GetSpawnRate(IronBlock)))
        {
          active = IronBlock;
        }
        else if (random <= (spawnRate += GetSpawnRate(TinBlock)))
        {
          active = TinBlock;
        } 
        else if (random <= (spawnRate += GetSpawnRate(CoalBlock) / 2))
        {
          active = CoalBlock;
        }

        Instantiate(active, new Vector2(j, -i - worldHeight), Quaternion.identity, GameObject.Find("World").transform);
      }
    }

    worldHeight += height;
  }
}
