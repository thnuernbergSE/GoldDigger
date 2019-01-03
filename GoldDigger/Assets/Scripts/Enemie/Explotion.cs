using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion: MonoBehaviour
{
  public GameObject explostionEffect;

  float countdown;

  float radius = 1.0f;

  bool hasExplodede = false;

  public float force = 700f;

  public void ReceiveDamage(int[] itemInfo)
  {
    explotion();
  }

  void explotion()
  {
    Instantiate(explostionEffect, transform.position, transform.rotation);
  }


}

