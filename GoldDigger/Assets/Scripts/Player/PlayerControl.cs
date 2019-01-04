using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
  

  [SerializeField] readonly float coolDown = 0.5f;
  [SerializeField] bool airControl;

  bool openInventory;

  float animationTime;

  [SerializeField] float attackDistance;

  bool attackWithPickaxe;
  float coolDownTimer;
  int dmg;
  bool facingRight;

  [SerializeField] Transform[] groundPoints;
  [SerializeField] float groundRadius;
  bool isGrounded;
  bool isPickAnimationRunning;
  bool jump;
  [SerializeField] float jumpForce;

  Vector2 lookdirection;
  [SerializeField] float movementSpeed;

  Animator myAnimator;

  Rigidbody2D myRigidbody2D;

  GameObject pivotPoint;
  int strength;

  GameObject tool;
  GameObject thinkingCloud;


  bool wallJumping;

  [SerializeField] LayerMask whatIsGround;

  public float GetYPosition => transform.position.y;

  private GameObject Inventory;

  // Use this for initialization
  void Start()
  {
    facingRight = true;
    myRigidbody2D = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    Inventory = GameObject.Find("Inventory");
    pivotPoint = GameObject.Find("Pivot");
    tool = GameObject.Find("Tool");
    thinkingCloud = GameObject.Find("thinkCloud");

    thinkingCloud.SetActive(false);
    if (pivotPoint == null)
    {
      throw new NullReferenceException("Pivot Point not found! - PlayerControl.cs");
    }

    if (tool == null)
    {
      throw new NullReferenceException("Tool not found! - PlayerControl.cs");
    }
  }

  void Update()
  {
    HandleInput();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    var horizontal = Input.GetAxisRaw("Horizontal");

    isGrounded = IsGrounded();

    HandleMovement(horizontal);

    Flip(horizontal);

    HandleLayers();

    HandlePickaxe();

    HandleAttacks();

   HandleInventoryUI();

    ResetValues();
  }

  void HandleMovement(float horizontal)
  {
    if (myRigidbody2D.velocity.y < 0)
    {
      myAnimator.SetBool("land", true);
    }

    if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack_pickaxe") && (isGrounded || airControl))
    {
      myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, myRigidbody2D.velocity.y);
    }

    if (isGrounded && jump)
    {
      isGrounded = false;
      myRigidbody2D.AddForce(new Vector2(0, jumpForce));
      myAnimator.SetTrigger("jump");
    }


    myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, myRigidbody2D.velocity.y);

    myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
  }

  void HandleAttacks()
  {
    if (isPickAnimationRunning)
    {
      var pivotMax = 50;
      var pivotMin = -30f;
      var rate = 3f;

      animationTime += rate * Time.deltaTime;

      if (transform.localScale.x > 0)
      {
        pivotPoint.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(pivotMax, pivotMin, animationTime));
      }
      else
      {
        pivotPoint.transform.eulerAngles = new Vector3(0, 0, -Mathf.LerpAngle(pivotMax, pivotMin, animationTime));
      }

      if (animationTime > 1)
      {
        attackWithPickaxe = false;
        isPickAnimationRunning = false;
        animationTime = 0f;
        pivotPoint.transform.eulerAngles = transform.localScale.x > 0 ? new Vector3(0, 0, 25) : new Vector3(0, 0, -19);
      }
    }

    coolDownTimer -= Time.deltaTime;

    if (attackWithPickaxe && isGrounded && coolDownTimer <= 0)
    {
      isPickAnimationRunning = true;
      HandleRaycast();
      coolDownTimer = coolDown;
      SendMessage("UseStamina", 2);
    }
  }

  void HandleInput()
  {
    Physics2D.queriesStartInColliders = false;
    var hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1);

    if (Input.GetKey(KeyCode.Mouse0))
    {
      attackWithPickaxe = true;
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      jump = true;
      SendMessage("UseStamina", 2);
    }

    if (Input.GetKeyDown(KeyCode.I))
    {
      if (openInventory)
      {
        openInventory = false;
      }
      else
      {
        openInventory = true;
      }
    }

    if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && hit.collider != null)
    {
      GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed * hit.normal.x, movementSpeed);
      SendMessage("UseStamina", 2);
    }
    else if (hit.collider != null && wallJumping)
    {
      wallJumping = false;
      SendMessage("UseStamina", 2);
    }
  }

  void Flip(float horizontal)
  {
    if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
    {
      facingRight = !facingRight;
      var theScale = transform.localScale;

      theScale.x *= -1;

      transform.localScale = theScale;
    }
  }

  void ResetValues()
  {
    attackWithPickaxe = false;
    jump = false;
  }

  bool IsGrounded()
  {
    if (myRigidbody2D.velocity.y <= 0)
    {
      foreach (var point in groundPoints)
      {
        var colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

        for (var i = 0; i < colliders.Length; i++)
        {
          if (colliders[i].gameObject != gameObject)
          {
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("land", false);
            return true;
          }
        }
      }
    }

    return false;
  }

  void HandleLayers()
  {
    if (!isGrounded)
    {
      myAnimator.SetLayerWeight(1, 1);
    }
    else
    {
      myAnimator.SetLayerWeight(1, 0);
    }
  }

  void HandleRaycast()
  {
    var lookdirection = Lookdirection();

    var hit = Physics2D.Raycast(transform.position, lookdirection, attackDistance, 1 << 8);

    if (hit.collider != null)
    {
      hit.collider.SendMessage("ReceiveDamage", new[] {strength, dmg});
    }
  }

  void HandlePickaxe()
  {
    var player = gameObject.GetComponent<Player>();
    var pickaxe = player.Pickaxe.GetComponent<Pickaxe>();
    tool.GetComponent<SpriteRenderer>().sprite = pickaxe.GetSprite;
    strength = (int) pickaxe.Strength;
    dmg = (int) pickaxe.Damage;
  }

  Vector2 Lookdirection()
  {
    var lookdirection = new Vector2(0, 0);
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    var direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
    direction.Normalize();

    if (direction.x > 0 && direction.y > 0)
    {
      if (direction.x < direction.y)
      {
        lookdirection = new Vector2(0, 1);
      }

      if (direction.x >= direction.y)
      {
        lookdirection = new Vector2(1, 0);
      }
    }

    if (direction.x < 0 && direction.y < 0)
    {
      if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
      {
        lookdirection = new Vector2(0, -1);
      }

      if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
      {
        lookdirection = new Vector2(-1, 0);
      }
    }

    if (direction.x > 0 && direction.y < 0)
    {
      if (direction.x < Mathf.Abs(direction.y))
      {
        lookdirection = new Vector2(0, -1);
      }

      if (direction.x >= Mathf.Abs(direction.y))
      {
        lookdirection = new Vector2(1, 0);
      }
    }

    if (direction.x < 0 && direction.y > 0)
    {
      if (Mathf.Abs(direction.x) < direction.y)
      {
        lookdirection = new Vector2(0, 1);
      }

      if (Mathf.Abs(direction.x) >= direction.y)
      {
        lookdirection = new Vector2(-1, 0);
      }
    }

    return lookdirection;
  }

  void HandleInventoryUI()
  {
    if (openInventory)
    {
      Inventory.SetActive(true);
    }
    else
    {
      Inventory.SetActive(false);
    }
  }
}