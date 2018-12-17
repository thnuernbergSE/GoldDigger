using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour {

    [SerializeField]
    float strength;
    [SerializeField]
    float dmg;
    [SerializeField]
    string name;

    [SerializeField]
    Sprite sprite;

    public float Strength
    {
        get { return strength; }
    }
    public float Damage
    {
        get { return dmg; }
    }
    public string Name
    {
        get { return name; }
    }
    public Sprite GetSprite
    {
        get { return sprite; }
    }


}
