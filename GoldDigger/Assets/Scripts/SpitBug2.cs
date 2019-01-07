using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitBug2 : MonoBehaviour {

    public float speed;

    Patrol patrol;

    void Start()
    {
        patrol = transform.parent.gameObject.GetComponent<Patrol>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (patrol.GetMovingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, 0);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, 0);
        }
    }

    void OntriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spieler")
        {
      other.SendMessage("TakeDamage",1);
        }
        Destroy(gameObject);
    }
}
