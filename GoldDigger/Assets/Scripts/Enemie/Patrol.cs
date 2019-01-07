using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{ 
  public float SpitCooldown = 2f;

  public float speed;

  private float distance;

  private bool movingRight = true;

  public bool GetMovingRight => movingRight;


  public GameObject spiderattack;

  public bool canSpit;
  void Start()
  {
  }

  void Update()
  {
    SpitCooldown -= Time.deltaTime;

    if (SpitCooldown <= 0  && canSpit)
    {

      SpitCooldown = 5f;
      Instantiate(spiderattack, transform.position, Quaternion.identity, transform);
    }

    transform.Translate(Vector2.right * speed * Time.deltaTime);
  }

  void OnCollisionEnter2D(Collision2D col)
  {
      if (movingRight)
      {
        transform.eulerAngles = new Vector3(0, -180, 0);

        movingRight = false;
      }
      else
      {
        transform.eulerAngles = new Vector3(0, 0, 0);

        movingRight = true;
      }
    
  }

  public void BugTakesDamage()
  {
    Destroy(gameObject);
  }
}