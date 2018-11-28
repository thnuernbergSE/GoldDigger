using UnityEngine;

public class Blocks : MonoBehaviour
{
  [SerializeField]
  string blockName;

  [SerializeField]
  int hardness;

  [SerializeField]
  float health;

  [SerializeField]
  float spawnrate;

  [SerializeField]
  bool breakable = true;

  void breakBlock()
  {
    Destroy(gameObject);
    //TODO: play break Animation
  }

  public float Hardness => hardness;

  public float Spawnrate => spawnrate;

  public void ReceiveDamage(float[] itemInfo)
  {
    if (itemInfo.Length != 2)
    {
      throw new System.Exception("ReceiveDamage not right amount of arguments");
    }

    if(itemInfo[0] >= hardness)
    {
      health -= itemInfo[1];
    }
  }
}