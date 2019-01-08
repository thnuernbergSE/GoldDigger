using UnityEngine;

public class ExplotionHandler : MonoBehaviour {
  const float radius = 3.0f;

  [SerializeField]
  const float explosionDamage = 1.0f;

  [SerializeField]
  const int force = 700;

  float delay = 0.3f;

  GameObject player;

  // Use this for initialization
  void Start ()
  {
    player = GameObject.Find("Player");
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
    foreach (Collider2D nearbyObject in colliders)
    {
      if (nearbyObject == null || nearbyObject.gameObject.tag != "Blocks")
      {
        continue;
      }

      Destroy(nearbyObject.gameObject);
    }

    Vector2 vector =  player.transform.position - gameObject.transform.position;
    vector.Normalize();

    GameObject.Find("Player").GetComponent<Player>().GetComponent<Rigidbody2D>().AddForce(vector * force);
    GameObject.Find("Player").GetComponent<Player>().SendMessage("TakeDamage", explosionDamage);
  }
	
	// Update is called once per frame
	void Update () {
    delay -= Time.deltaTime;

    if (delay <= 0)
    {
      Destroy(gameObject);
    }
  }
}
