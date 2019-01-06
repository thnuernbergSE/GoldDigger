using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitBug2 : MonoBehaviour
{

  public float speed;

  bool direction;

  void Start()
  {
    direction = GameObject.Find("Bug2").GetComponent<Patrol>().GetMovingRight;
  }

  // Update is called once per frame
  void Update()
  {
    
    if (direction)
    {
      transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);

    }
    else if (!direction)
    {
      transform.localScale = new Vector2(-1, 1);
      transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
  }


  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag.Equals("Player"))
    {
      GameObject.Find("Player").GetComponent<Player>().SendMessage("TakeDamage",1);
    }
    DestroySpit();
  }
  public void DestroySpit()
  {
    Destroy(gameObject);
  }
}

