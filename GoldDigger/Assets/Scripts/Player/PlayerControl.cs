using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
  Vector2 lookdirection;

  private Rigidbody2D myRigidbody2D;

  private Animator myAnimator;

  [SerializeField] private float movementSpeed;

  private bool attackWithPickaxe;

  private bool facingRight;

  [SerializeField] private float groundRadius;

  [SerializeField] private float jumpForce;

  private bool isGrounded;

  private bool jump;

  [SerializeField] private bool airControl;

  [SerializeField] private LayerMask whatIsGround;

  [SerializeField] private Transform[] groundPoints;

  [SerializeField] float attackDistance;

  private float strength;
  private float dmg;

  [SerializeField] private float coolDown = 0.5f;

  private float coolDownTimer;



  //TODO:
  bool wallJumping;


  GameObject pivotPoint;
  bool isPickAnimationRunning = false;

  public float GetYPosition => transform.position.y;


  // Use this for initialization
  void Start()
  {
    facingRight = true;
    myRigidbody2D = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    pivotPoint = GameObject.Find("Pivot");

    if (pivotPoint == null)
    {
      throw new NullReferenceException("Pivot Point not found! - PlayerControl.cs");
    }

  }

  void Update()
  {
    HandleInput();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    float horizontal = Input.GetAxisRaw("Horizontal");

    isGrounded = IsGrounded();

    HandleMovement(horizontal);

    Flip(horizontal);

    HandleLayers();

    HandlePickaxe();

    HandleAttacks();

    ResetValues();


  }

  private void HandleMovement(float horizontal)
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

  float i = 0f;
  

  private void HandleAttacks()
  {
    if (isPickAnimationRunning)
    {
      var pivotMax = 50;
      var pivotMin = -30f;
      float rate = 3f;


      i += rate * Time.deltaTime;

      pivotPoint.transform.eulerAngles = new Vector3(0,0,Mathf.LerpAngle(pivotMax, pivotMin, i));;

      Debug.Log(pivotPoint.transform.rotation);

      if (i > 1)
      {
        isPickAnimationRunning = false;
        i = 0f;
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

  private void HandleInput()
  {
    Physics2D.queriesStartInColliders = false;
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1);

    if (Input.GetKey(KeyCode.Mouse0))
    {
      attackWithPickaxe = true;
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      jump = true;
    }

    if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && hit.collider != null)
    {
      GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed * hit.normal.x, movementSpeed);

      //transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one; // nicht auskommentieren wichtig für später vlt
    }
    else if (hit.collider != null && wallJumping)
    {
      wallJumping = false;
    }

  }

  private void Flip(float horizontal)
  {
    if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
    {
      facingRight = !facingRight;
      Vector3 theScale = transform.localScale;

      theScale.x *= -1;

      transform.localScale = theScale;
    }
  }

  private void ResetValues()
  {
    attackWithPickaxe = false;
    jump = false;
  }

  private bool IsGrounded()
  {
    if (myRigidbody2D.velocity.y <= 0)
    {
      foreach (Transform point in groundPoints)
      {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

        for (int i = 0; i < colliders.Length; i++)
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

  private void HandleLayers()
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

  private void HandleRaycast()
  {
    Vector2 lookdirection = Lookdirection();

    RaycastHit2D hit = Physics2D.Raycast(transform.position, lookdirection, attackDistance, 1 << 8);

    if (hit.collider != null)
    {
      hit.collider.SendMessage("ReceiveDamage", new float[] {strength, dmg});
    }
  }

  private void HandlePickaxe()
  {
    Player player = gameObject.GetComponent<Player>();
    Pickaxe pickaxe = player.Pickaxe.GetComponent<Pickaxe>();
    strength = pickaxe.Strength;
    dmg = pickaxe.Damage;

  }

  private Vector2 Lookdirection()
  {
    Vector2 lookdirection = new Vector2(0, 0);
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
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

}