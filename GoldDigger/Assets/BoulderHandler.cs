using UnityEngine;

public class BoulderHandler : MonoBehaviour
{
  readonly float maxFallDistance = 1f;
  bool doWobble;
  float fallDistance;
  bool isFalling;
  Vector3 oldPos;

  GameObject stoneUnderneath;
  float timePerWobble;
  float timeTillFall = 1f;
  uint wobbleDirection = 1;

  // Use this for initialization
  void Start()
  {
    getStoneUnderneath();
    oldPos = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (isFalling)
    {
      timeTillFall -= Time.deltaTime;
      if (timeTillFall < 0f || !doWobble)
      {
        doWobble = false;
        transform.position = new Vector3(oldPos.x, transform.position.y);
        var toFall = 0.1f;
        if (fallDistance < maxFallDistance)
        {
          transform.position -= new Vector3(0, toFall);
          fallDistance += toFall;
        }
        else
        {
          isFalling = false;
          fallDistance = 0f;
          timeTillFall = 1f;
        }
      }
      else
      {
        if (timePerWobble < 0 && doWobble)
        {
          if (wobbleDirection == 1)
          {
            transform.position += new Vector3(Random.Range(0.005f, 0.05f), 0);
            wobbleDirection = 0;
          }
          else if (wobbleDirection == 0)
          {
            transform.position -= new Vector3(Random.Range(0.005f, 0.05f), 0);
            wobbleDirection = 1;
          }

          timePerWobble = 0.2f;
        }

        timePerWobble -= Time.deltaTime;
      }
    }
    else
    {
      getStoneUnderneath();
    }
  }

  void getStoneUnderneath()
  {
    var col = Physics2D.OverlapCircle(transform.position + new Vector3(0, -1), 0.1f);
    if (col != null)
    {
      if (col.gameObject.layer == 8 || col.gameObject.GetComponent<BoulderHandler>() != null)
      {
        stoneUnderneath = col.gameObject;
        doWobble = true;
      }
    }
    else
    {
      isFalling = true;
    }
  }
}