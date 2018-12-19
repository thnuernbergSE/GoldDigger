using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

  // Use this for initialization
  public int MaxHealth
  {
    get;
    set;
  }
  public int AcutalHealth
  {
    get;
  }

  public int Stammina
  {
    get;
  }

  int money;

  public int Money => money;

  public bool AddMoney(int amount)
  {
    money += amount;
    return true;
  }

  public bool RemoveMoney(int amount)
  {
    if (money < amount)
    {
      return false;
    }

    money -= amount;
    return true;
  }

  public GameObject Pickaxe
  {
    get { return mainTool; }
    set { mainTool = value; }
  }

  public float GetYPos => transform.position.y;

  [SerializeField]
  private GameObject mainTool;

  private Pickaxe pickScript;

  void Start()
  {
    money = 20;
  }

  // Update is called once per frame
  void Update()
  {

  }
}