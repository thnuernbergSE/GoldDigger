using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
  [SerializeField] bool airControl;

  [SerializeField] float attackDistance;
  [SerializeField] float groundRadius;
  [SerializeField] float jumpForce;
  [SerializeField] float movementSpeed;

  [SerializeField] readonly float coolDown = 0.5f;

  [SerializeField] Transform[] groundPoints;

  [SerializeField] LayerMask whatIsGround;

  float animationTime;
  float coolDownTimer;
  float dmg;
  float strength;

  bool attackWithPickaxe;
  bool facingRight;
  bool isGrounded;
  bool isPickAnimationRunning;
  bool jump;
  bool wallJumping;

  Vector2 lookdirection;

  Animator myAnimator;

  Rigidbody2D myRigidbody2D;

  GameObject pivotPoint;

  public float GetYPosition => transform.position.y;


  // Use this for initialization
  void Start()
  {
    facingRight = true;
    myRigidbody2D = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    pivotPoint = GameObject.Find("Pivot");

    if (pivotPoint == null) throw new NullReferenceException("Pivot Point not found! - PlayerControl.cs");
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

    ResetValues();
  }

  void HandleMovement(float horizontal)
  {
    if (myRigidbody2D.velocity.y < 0) myAnimator.SetBool("land", true);

    if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack_pickaxe") && (isGrounded || airControl))
      myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, myRigidbody2D.velocity.y);

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

      pivotPoint.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(pivotMax, pivotMin, animationTime));
      ;

      Debug.Log(pivotPoint.transform.rotation);

      if (animationTime > 1)
      {
        isPickAnimationRunning = false;
        animationTime = 0f;
      }
    }

    coolDownTimer -= Time.deltaTime;
    if (attackWithPickaxe && !myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack_pickaxe") && isGrounded &&
        coolDownTimer <= 0)
    {
      //myAnimator.SetTrigger("attack_pickaxe");
      //ANIMATION


      isPickAnimationRunning = true;
      HandleRaycast();
      myRigidbody2D.velocity = Vector2.zero;
      coolDownTimer = coolDown;
    }
  }

  void HandleInput()
  {
    Physics2D.queriesStartInColliders = false;
    var hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1);

    if (Input.GetKey(KeyCode.Mouse0)) attackWithPickaxe = true;

    if (Input.GetKeyDown(KeyCode.Space)) jump = true;

    if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && hit.collider != null)
      GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed * hit.normal.x, movementSpeed);
    else if (hit.collider != null && wallJumping) wallJumping = false;
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
      foreach (var point in groundPoints)
      {
        var colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

        for (var i = 0; i < colliders.Length; i++)
          if (colliders[i].gameObject != gameObject)
          {
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("land", false);
            return true;
          }
      }

    return false;
  }

  void HandleLayers()
  {
    if (!isGrounded)
      myAnimator.SetLayerWeight(1, 1);
    else
      myAnimator.SetLayerWeight(1, 0);
  }

  void HandleRaycast()
  {
    var lookdirection = Lookdirection();

    var hit = Physics2D.Raycast(transform.position, lookdirection, attackDistance, 1 << 8);

    if (hit.collider != null) hit.collider.SendMessage("ReceiveDamage", new[] {strength, dmg});
  }

  void HandlePickaxe()
  {
    var player = gameObject.GetComponent<Player>();
    var pickaxe = player.Pickaxe.GetComponent<Pickaxe>();
    strength = pickaxe.Strength;
    dmg = pickaxe.Damage;
  }

  Vector2 Lookdirection()
  {
    var lookdirection = new Vector2(0, 0);
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    var direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
    direction.Normalize();

    if (direction.x > 0 && direction.y > 0)
    {
      if (direction.x < direction.y) lookdirection = new Vector2(0, 1);

      if (direction.x >= direction.y) lookdirection = new Vector2(1, 0);
    }

    if (direction.x < 0 && direction.y < 0)
    {
      if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) lookdirection = new Vector2(0, -1);

      if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y)) lookdirection = new Vector2(-1, 0);
    }

    if (direction.x > 0 && direction.y < 0)
    {
      if (direction.x < Mathf.Abs(direction.y)) lookdirection = new Vector2(0, -1);

      if (direction.x >= Mathf.Abs(direction.y)) lookdirection = new Vector2(1, 0);
    }

    if (direction.x < 0 && direction.y > 0)
    {
      if (Mathf.Abs(direction.x) < direction.y) lookdirection = new Vector2(0, 1);

      if (Mathf.Abs(direction.x) >= direction.y) lookdirection = new Vector2(-1, 0);
    }

    return lookdirection;
  }
}