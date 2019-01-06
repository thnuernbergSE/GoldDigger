using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitBug2 : MonoBehaviour
{

  public float speed;

  

  void Start()
  {
  
  }

  // Update is called once per frame
  void Update()
  {
    if (GameObject.Find("Bug2").GetComponent<Patrol>().GetMovingRight)
    {
      transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
      
    }
    else
    {
      transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
  }

  void FixUpdate()
  {
    
  }

  void OntriggerEnter2D(Collider2D other)
  {
    if (other.gameObject != null)
    {
      if (other.gameObject.tag.Equals("Player"))
      {
        SendMessage("TakeDamage", 1);
      }
      Destroy(gameObject);
    }
  }
}
