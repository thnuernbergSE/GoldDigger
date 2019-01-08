using UnityEngine;

public class SpitBug2 : MonoBehaviour
{
  public float speed;

  bool movingRight;

  void Start()
  {
    movingRight = transform.parent.GetComponent<Patrol>().GetMovingRight;

    Destroy(gameObject, 3);
  }

  // Update is called once per frame
  void Update()
  {
    if (movingRight)
    {
      transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }
    if (!movingRight)
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

    if (!other.gameObject.tag.Equals("Enemy"))
    {
      DestroySpit();
    } 
  }

  public void DestroySpit()
  {
    Destroy(gameObject);
  }
}

