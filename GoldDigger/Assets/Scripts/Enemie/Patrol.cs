using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{ 
  public float SpitCooldown = 2f;

  public float speed;

  private float distance;

  private float timeBtwShots;

  public float startTimeShots;

  private bool movingRight = true;

  public bool GetMovingRight => movingRight;

  public Transform wallDetection;

  public GameObject spiderattack;

  public bool canSpit;
  void Start()
  {
    timeBtwShots = startTimeShots;
  }

  void Update()
  {
    SpitCooldown -= Time.deltaTime;

    if (SpitCooldown <= 0  && canSpit)
    {
      SpitCooldown = 5f;
      Instantiate(spiderattack, transform.position, Quaternion.identity);
    }

    transform.Translate(Vector2.right * speed * Time.deltaTime);

    RaycastHit2D wallinfo = Physics2D.Raycast(wallDetection.position, Vector2.down, 2f);

    timeBtwShots = startTimeShots;

    if (!wallinfo.collider)
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
  }

  public void BugTakesDamage()
  {
    Destroy(gameObject);
  }
}