using UnityEngine;

public class CloudScript : MonoBehaviour
{
  float flyingSpeed;

  // Use this for initialization
  void Start()
  {
    flyingSpeed = Random.Range(0.5f, 1.5f);
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += new Vector3(flyingSpeed * Time.deltaTime, 0);

    if (transform.position.x > MapGenerator.GetWorldWidth + 10)
    {
      Destroy(gameObject);
    }
  }
}
