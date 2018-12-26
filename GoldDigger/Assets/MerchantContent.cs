using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantContent : MonoBehaviour {

  [SerializeField]
  GameObject[] food;

  public GameObject[]Food
  {
    get { return food; }
  }
}
