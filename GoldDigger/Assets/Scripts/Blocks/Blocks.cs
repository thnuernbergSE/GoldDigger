using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public string blockName;
    public int hardness;
    public bool breakable = true;

    void Start()
    {

    }

    void onBreak()
    {
        Destroy(gameObject);
    }
}
