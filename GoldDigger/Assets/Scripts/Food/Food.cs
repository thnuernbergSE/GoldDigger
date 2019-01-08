using UnityEngine;

public class Food : MonoBehaviour
{
  [SerializeField]
  string name;

  [SerializeField]
  Sprite sprite;

  [SerializeField]
  int energy;

  [SerializeField]
  int cost;

  public Sprite GetSprite => sprite;

  public int Energy => energy;

  public int Cost => cost;

  public string Name => name;
}
  