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

    }

    // Update is called once per frame
    void Update()
    {

    }
}