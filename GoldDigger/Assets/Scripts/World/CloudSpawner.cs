using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
  [SerializeField] GameObject[] cloudPrefabs = new GameObject[3];

  float nextCloudSpawnTime = 0f;

  // Update is called once per frame
  void Update()
  {
    nextCloudSpawnTime -= Time.deltaTime;

    if (nextCloudSpawnTime <= 0)
    {
      var newCloud = Instantiate(cloudPrefabs[Random.Range(0, 3)],
          new Vector3(transform.position.x, Random.Range(4f, 6f)), Quaternion.identity,
          GameObject.Find("World").transform);

      newCloud.GetComponent<SpriteRenderer>().flipX = (Random.Range(0, 2) == 0 ? true : false);
      newCloud = null;

      nextCloudSpawnTime = Random.Range(8, 15);
    }
  }
}
