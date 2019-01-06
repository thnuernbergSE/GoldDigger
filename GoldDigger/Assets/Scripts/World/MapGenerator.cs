using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  const int worldWidth = 50;
  public GameObject AluminumBlock;

  public GameObject BoulderEnemy;
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

  [SerializeField] int BoneAmount = 20;
  [SerializeField] int maxTNT = 20;

  [SerializeField] GameObject TNTBlock;

  [SerializeField] GameObject BoneBlock;

  [SerializeField] GameObject normalBug;

  [SerializeField] GameObject extremeBug;

  [SerializeField] int blockHealthDirtLayer = 0;
  [SerializeField] int blockHealthStoneLayer = 0;
  [SerializeField] int blockHealthIronTinLayer = 0;
  [SerializeField] int blockHealthSilverAluminumLayer = 0;
  [SerializeField] int blockHealthGoldLithiumLayer = 0;
  [SerializeField] int blockHealthRubyCobaltLayer = 0;
  [SerializeField] int blockHealthDiamondLayer = 0;
  [SerializeField] int blockHealthPlatinumLayer = 0;
  [SerializeField] int blockHealthTitaniumLayer = 0;
  [SerializeField] int maxDungeons = 5;

  public static int GetWorldWidth => worldWidth;
  public int GetWorldHeight { get; set; }

  float GetSpawnRate(GameObject block)
  {
    var blocksClass = block.GetComponent<Blocks>();

    return blocksClass.Spawnrate;
  }

  void setWorldBackground()
  {
    var background = Instantiate(WorldBackground, new Vector2(worldWidth / 2f - 0.5f, -GetWorldHeight / 2f - 13f),
      Quaternion.identity);

    var spriteRenderer = background.GetComponent<SpriteRenderer>();

    spriteRenderer.size = new Vector2(worldWidth + 30, GetWorldHeight + 30);
  }


  void createStoneLayer1(int width, int height)
  {
    for (var i = 0; i < height; i++)
    for (var j = 0; j < width; j++)
    {

      var active = StoneBlock;

      float spawnRate = 0;

      var rand = Random.Range(0f, 100f);

      if (rand <= (spawnRate += GetSpawnRate(CoalBlock)))
      {
        active = CoalBlock;
      }
      else if (rand <= spawnRate + GetSpawnRate(CopperBlock))
      {
        active = CopperBlock;
      }
      else if (rand <= spawnRate + GetSpawnRate(BoulderEnemy))
      {
        active = BoulderEnemy;
      }

      var spawnedBlock = Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
      spawnedBlock.GetComponent<Blocks>().Hardness = 2;
      spawnedBlock.GetComponent<Blocks>().Health = blockHealthStoneLayer;
    }

    GetWorldHeight += height;
  }

  void createDirtLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
    {
      for (var j = 0; j < width; j++)
      {    
        var rand = Random.Range(0, 100);

        var active = rand >= GetSpawnRate(CobbleBlock) ? DirtBlock : CobbleBlock;

        var spawnedBlock = Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
        spawnedBlock.GetComponent<Blocks>().Hardness = 1;
        spawnedBlock.GetComponent<Blocks>().Health = blockHealthDirtLayer;
      }
    }
    
    GetWorldHeight += height;
  }

  void spawnBones(int width)
  {
    for(var i = 0; i < BoneAmount; i++)
    {
      var randomXPos = Random.Range(0, width);
      var randomYPos = Random.Range(-GetWorldHeight + 1, -1);

      var col = Physics2D.OverlapCircle(new Vector2(randomXPos, randomYPos), 0.1f);

      while (col == null || col.tag !=  "Blocks")
      {
        randomXPos = Random.Range(0, width);
        randomYPos = Random.Range(-GetWorldHeight + 1, -1);

        col = Physics2D.OverlapCircle(new Vector2(randomXPos, randomYPos), 0.1f);
      }

      Destroy(col.gameObject);

      Instantiate(BoneBlock, new Vector2(randomXPos, randomYPos), Quaternion.identity, GameObject.Find("World").transform);
    }
  }

  int dungeonWidth = worldWidth;
  int dungeonHeight = 0;
  int deathLimit = 3;
  int birthLimit = 3;
  float numberOfSteps = 3;
  float chanceToStartAlive = 0.5f;

  public bool[,] initialiseMap(bool[,] map)
  {
    for (int x = 0; x < dungeonWidth; x++)
    {
      for (int y = 0; y < dungeonHeight; y++)
      {
        if (Random.Range(0f, 1f) < chanceToStartAlive)
        {
          map[x,y] = true;
        }
      }
    }
    return map;
  }

  public int countAliveNeighbours(bool[,] map, int x, int y)
  {
    int count = 0;
    for (int i = -1; i < 2; i++)
    {
      for (int j = -1; j < 2; j++)
      {
        int neighbour_x = x + i;
        int neighbour_y = y + j;

        if (i == 0 && j == 0)
        {
        }
        else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= map.GetLength(0) || neighbour_y >= map.GetLength(1))
        {
          count = count + 1;
        }
        else if (map[neighbour_x, neighbour_y])
        {
          count = count + 1;
        }
      }
    }

    return count;
  }

  public bool[,] doSimulationStep(bool[,] oldMap)
  {
    bool[,] newMap = new bool[dungeonWidth, dungeonHeight];
    for (int x = 0; x < oldMap.GetLength(0); x++)
    {
      for (int y = 0; y < oldMap.GetLength(1); y++)
      {
        int nbs = countAliveNeighbours(oldMap, x, y);

        if (oldMap[x, y])
        {
          if (nbs < deathLimit)
          {
            newMap[x, y] = false;
          }
          else
          {
            newMap[x, y] = true;
          }
        }
        else
        {
          if (nbs > birthLimit)
          {
            newMap[x, y] = true;
          }
          else
          {
            newMap[x, y] = false;
          }
        }
      }
    }
    return newMap;
  }

  public bool[,] generateMap()
  {
    var cellmap = new bool[dungeonWidth, dungeonHeight];

    cellmap = initialiseMap(cellmap);

    for (var i = 0; i < numberOfSteps; i++)
    {
      cellmap = doSimulationStep(cellmap);
    }

    return cellmap;
  }

  void createDungeons()
  {
    int dungeonOffset = 20;

    dungeonHeight = GetWorldHeight;

    var cellmap = new bool[dungeonWidth, dungeonHeight - dungeonOffset];

    cellmap = generateMap();

    for (var cellmapX = 0; cellmapX < cellmap.GetLength(0); cellmapX++)
    {
      for (var cellmapY = 0; cellmapY < cellmap.GetLength(1); cellmapY++)
      {
        if (cellmap[cellmapX, cellmapY])
        {
          continue;
        }

        var col = Physics2D.OverlapCircle(new Vector2(cellmapX, -(cellmapY + dungeonOffset)), 0.1f);
        if (col == null || col.tag != "Blocks")
        {
          continue;
        }

        if (Random.Range(0, 20) == 0)
        {
          if (Random.Range(0, 2) == 0)
          {
            Instantiate(normalBug, new Vector2(cellmapX, -(cellmapY + dungeonOffset)), Quaternion.identity, GameObject.Find("World").transform);
          }
          else
          {
            Instantiate(extremeBug, new Vector2(cellmapX, -(cellmapY + dungeonOffset)), Quaternion.identity, GameObject.Find("World").transform);
          }
        }

        Destroy(col.gameObject);
      }
    }
  }

  void spawnTNTBlocks()
  {
    for (var i = 0; i < maxTNT; i++)
    {
      var randomX = Random.Range(0, worldWidth);
      var randomY = Random.Range(-GetWorldHeight, -20);

      var col = Physics2D.OverlapCircle(new Vector2(randomX, randomY), 0.1f);
      if (col == null || col.tag != "Blocks")
      {
        continue;
      }

      Destroy(col.gameObject);
      Instantiate(TNTBlock, new Vector2(randomX, randomY), Quaternion.identity, GameObject.Find("World").transform);
    }
  }

  void spawnBugs()
  {
    int spawnOffset = 20;

    for (int i = 0; i < 20; i++)
    {
      var randomXPos = Random.Range(0, worldWidth);
      var randomYPos = Random.Range(-GetWorldHeight + 1, -1);

      var col = Physics2D.OverlapCircle(new Vector2(randomXPos, randomYPos), 0.1f);

      while (col == null || col.tag != "WorldBackground")
      {
        randomXPos = Random.Range(0, worldWidth);
        randomYPos = Random.Range(-GetWorldHeight + 1, -1);

        col = Physics2D.OverlapCircle(new Vector2(randomXPos, randomYPos), 0.1f);
      }

      var randomBug = Random.Range(0, 2);

      if (randomBug == 0)
      {
        Instantiate(normalBug, new Vector2(randomXPos, randomYPos), Quaternion.identity, GameObject.Find("World").transform);
      }
      else
      {
        Instantiate(extremeBug, new Vector2(randomXPos, randomYPos), Quaternion.identity, GameObject.Find("World").transform);
      }

    }
  }

  void Start()
  {
    createDirtLayer(worldWidth, 10);
    createStoneLayer1(worldWidth, 20);
    createIronTinLayer(worldWidth, 20);
    createSilverAluminumLayer(worldWidth, 20);
    createGoldLithiumLayer(worldWidth, 20);
    createRubyCobaltLayer(worldWidth, 20);
    createDiamondLayer(worldWidth, 20);
    createPlatinumLayer(worldWidth, 20);
    createTitaniumLayer(worldWidth, 20);

    spawnTNTBlocks();

    createDungeons();

    //spawnBugs();

    spawnBones(worldWidth);

    setWorldBackground();
  }

  void createTitaniumLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
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
      else if (random <= spawnRate + GetSpawnRate(BoulderEnemy))
      {
        active = BoulderEnemy;
      }

      var spawnedBlock = Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
      spawnedBlock.GetComponent<Blocks>().Hardness = 6;
      spawnedBlock.GetComponent<Blocks>().Health = blockHealthTitaniumLayer;
      }

    GetWorldHeight += height;
  }

  void createPlatinumLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
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
      else if (random <= spawnRate + GetSpawnRate(BoulderEnemy))
      {
        active = BoulderEnemy;
      }

      var spawnedBlock = Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
      spawnedBlock.GetComponent<Blocks>().Hardness = 6;
      spawnedBlock.GetComponent<Blocks>().Health = blockHealthPlatinumLayer;
      }

    GetWorldHeight += height;
  }

  void createDiamondLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
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
      else if (random <= spawnRate + GetSpawnRate(BoulderEnemy))
      {
        active = BoulderEnemy;
      }

      var spawnedBlock = Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
      spawnedBlock.GetComponent<Blocks>().Hardness = 5;
      spawnedBlock.GetComponent<Blocks>().Health = blockHealthDiamondLayer;
      }

    GetWorldHeight += height;
  }

  void createRubyCobaltLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
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
      else if (random <= spawnRate + GetSpawnRate(BoulderEnemy))
      {
        active = BoulderEnemy;
      }

      var spawnedBlock =Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
      spawnedBlock.GetComponent<Blocks>().Hardness = 4;
      spawnedBlock.GetComponent<Blocks>().Health = blockHealthRubyCobaltLayer;
      }

    GetWorldHeight += height;
  }

  void createGoldLithiumLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
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
      else if (random <= spawnRate + GetSpawnRate(BoulderEnemy))
      {
        active = BoulderEnemy;
      }

      var spawnedBlock = Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
      spawnedBlock.GetComponent<Blocks>().Hardness = 4;
      spawnedBlock.GetComponent<Blocks>().Health = blockHealthGoldLithiumLayer;
      }

    GetWorldHeight += height;
  }

  void createSilverAluminumLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
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
      else if (random <= spawnRate + GetSpawnRate(BoulderEnemy))
      {
        active = BoulderEnemy;
      }

      var spawnedBlock = Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
      spawnedBlock.GetComponent<Blocks>().Hardness = 3;
      spawnedBlock.GetComponent<Blocks>().Health = blockHealthSilverAluminumLayer;
      }

    GetWorldHeight += height;
  }

  void createIronTinLayer(int width, int height)
  {
    for (var i = 0; i < height; i++)
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
      else if (random <= spawnRate + GetSpawnRate(BoulderEnemy))
      {
        active = BoulderEnemy;
      }

      var spawnedBlock = Instantiate(active, new Vector2(j, -i - GetWorldHeight), Quaternion.identity, GameObject.Find("World").transform);
      spawnedBlock.GetComponent<Blocks>().Hardness = 3;
      spawnedBlock.GetComponent<Blocks>().Health = blockHealthIronTinLayer;
      }

    GetWorldHeight += height;
  }
}