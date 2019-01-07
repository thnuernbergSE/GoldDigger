using UnityEngine;

public class Pickaxe : MonoBehaviour
{
  [SerializeField] float dmg;

  [SerializeField] int itemCost;

  [SerializeField] string name;

  [SerializeField] Sprite sprite;

  [SerializeField] float strength;

  public float Strength => strength;

  public float Damage => dmg;

  public string Name => name;

  public Sprite GetSprite => sprite;

  public int ItemCost => itemCost;
}