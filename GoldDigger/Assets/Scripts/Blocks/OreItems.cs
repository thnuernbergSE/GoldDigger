using UnityEngine;

public class OreItems : MonoBehaviour
{
  const float maxOffset = 0.05f;
  float currentOffset;
  float direction = 1;
  [SerializeField] string itemName;
  [SerializeField] int itemValue;
  [SerializeField] int itemWeight;

  Vector3 oldPos;
  readonly float speed = 0.05f;

  // Use this for initialization
  void Start()
  {
    oldPos = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (direction > 0) // Up
    {
      if (currentOffset >= maxOffset) direction *= -1;

      currentOffset += direction * speed * Time.deltaTime;
    }
    else if (direction < 0) // Up
    {
      if (currentOffset <= -maxOffset) direction *= -1;

      currentOffset += direction * speed * Time.deltaTime;
    }

    transform.position = oldPos + new Vector3(0, currentOffset);
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    if (col.tag == "Player") Debug.Log("test");
  }
}