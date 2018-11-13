using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Block
{
    Dirt,
    Stone
}

public abstract class Blocks : MonoBehaviour
{

    protected string name;
    protected int hardness;
    protected bool breakable = true;
    protected int posX;
    protected int posY;

    public Blocks(string name, int hardness, int posX, int posY, bool breakable = true)
    {
        this.name = name;
        this.hardness = hardness;
        this.posX = posX;
        this.posY = posY;
        this.breakable = breakable;

        GameObject DirtBlock = new GameObject(name);
        DirtBlock.AddComponent<SpriteRenderer>();
        SpriteRenderer sp = DirtBlock.GetComponent<SpriteRenderer>();
        sp.sprite = Resources.Load<Sprite>("Sprites/Blocks/" + name);
        DirtBlock.transform.position = new Vector2(posX, posY);

    }

    void Start()
    {

    }

    void setTexture()
    {
        

    }

    void onBreak()
    {
        Destroy(gameObject);
    }
}
