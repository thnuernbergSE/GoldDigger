using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion: MonoBehaviour
{
  public GameObject explosionEffect;

  float countdown;

  public void ReceiveDamage(int[] itemInfo)
  {
    explosion();
  }

  void explosion()
  {
    Instantiate(explosionEffect, transform.position, transform.rotation);
    Destroy(gameObject);
  }


}

