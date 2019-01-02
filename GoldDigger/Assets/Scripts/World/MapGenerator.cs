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

  [SerializeField] GameObject BoneBlock;

  [SerializeField] int blockHealthDirtLayer = 0;
  [SerializeField] int blockHealthStoneLayer = 0;
  [SerializeField] int blockHealthIronTinLayer = 0;
  [SerializeField] int blockHealthSilverAluminumLayer = 0;
  [SerializeField] int blockHealthGoldLithiumLayer = 0;
  [SerializeField] int blockHealthRubyCobaltLayer = 0;
  [SerializeField] int blockHealthDiamondLayer = 0;
  [SerializeField] int blockHealthPlatinumLayer = 0;
  [SerializeField] int blockHealthTitaniumLayer = 0;

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
      var randomYPos = Random.Range(-GetWorldHeight, -1);

      var col = Physics2D.OverlapCircle(new Vector2(randomXPos, randomYPos), 0.1f);

      if(col == null)
      {
        Debug.Log("X:" + randomXPos + ";  Y: " + randomYPos);
      }

      Destroy(col.gameObject);

      Instantiate(BoneBlock, new Vector2(randomXPos, randomYPos), Quaternion.identity, GameObject.Find("World").transform);
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