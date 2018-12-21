using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField]
  int maxHealth;

  [SerializeField]
  int maxStamina;

  [SerializeField]
  int currentHealth;

  [SerializeField]
  int currentStamina;

  int money;

  public int MaxHealth
  {
    get { return maxHealth; }
    set { currentHealth = Mathf.Min(value, MaxHealth); }
  }
  public int CurrentHealth
  {
    get { return currentHealth; }
    set { currentHealth = Mathf.Min(value, MaxHealth); }
  }

  public int MaxStamina
  {
    get {return maxStamina; }
    set { currentStamina = Mathf.Min(value,MaxStamina); }
  }

  public int CurrentStamina
  {
    get { return currentStamina; }
    set
    {currentStamina = Mathf.Min(value, MaxStamina);}
  }

  

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

  public bool AddStammina(int amount)
  {
    currentStamina += amount;
    currentStamina = Mathf.Min(currentStamina, MaxStamina);
    return true;
  }

  public bool AddHealth(int amout)
  {
    currentHealth += amout;
    currentHealth = Mathf.Min(currentHealth, MaxHealth);
    return true;
  }

  public bool TakeDamge(int amout)
  {
    
    currentHealth -= amout;
    currentHealth = Mathf.Max(currentHealth,0);
    return true;
  }

  public bool UseStamina(int amount)
  {
    currentStamina -= amount;
    currentStamina = Mathf.Max(currentStamina, 0);
    return true;
  }

}