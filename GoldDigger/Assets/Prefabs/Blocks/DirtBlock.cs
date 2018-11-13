using UnityEngine;

public class DirtBlock : Blocks
{
    public GameObject go;
    public DirtBlock(string name, int hardness, int posX, int posY, bool breakable = true) 
        : base(name, hardness, posX, posY, breakable)
    {
        
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
