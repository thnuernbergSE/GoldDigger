using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Vector2 lookdirection;

    private Rigidbody2D myRigidbody2D;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool attackWithPickaxe;

    private bool facingRight;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private float jumpforce;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    float attackDistance;

    private float strength = 5f;
    private float dmg = 5f;

    public bool onWall;

    [SerializeField]
    public float climbSpeed;
    

    // Use this for initialization
    void Start()
    {
        facingRight = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
            myRigidbody2D.AddForce(new Vector2(0, jumpforce));
            myAnimator.SetTrigger("jump");
        }

        //if (onWall)
        //{
        //    myRigidbody2D.velocity = new Vector2(horizontal * climbSpeed,myRigidbody2D.velocity.y);
        //}
      

        myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, myRigidbody2D.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks()
    {
        if (attackWithPickaxe && !myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack_pickaxe"))
        {
            myAnimator.SetTrigger("attack_pickaxe");
            HandleRaycast();
            myRigidbody2D.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackWithPickaxe = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookdirection, attackDistance);

        if (hit.collider != null)
        {
            hit.collider.SendMessage("ReceiveDamage", new float[] { strength, dmg });
        }
    }

    private Vector2 Lookdirection()
    {
        Vector2 lookdirection = new Vector2(0,0);
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
                lookdirection = new Vector2(0,-1);
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