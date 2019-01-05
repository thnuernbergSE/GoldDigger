using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion: MonoBehaviour
{
  public GameObject explosionEffect;

  float countdown;

  float radius = 1.0f;

  bool hasExplodede = false;

  public float force = 700f;

  public void ReceiveDamage(int[] itemInfo)
  {
    explosion();
  }

  void explosion()
  {
        SoundManager.PlaySound("bombExplosion");
        Instantiate(explosionEffect, transform.position, transform.rotation);

    Destroy(gameObject);
  }


}

