using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class sonic : MonoBehaviour
{
    //animation
    public Animator animator;

    //Particle
    public ParticleSystem dash;
    public ParticleSystem dust;

    //Materials
    public Material matLeft;
    public Material matRight;

    //Directions
    float xInput;
    float xDist;

    //changing directions
    public int lastDirection = 1;

    //movement
    public static float moveSpeed = 10.0f;
    public float velocity = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //jumpability
    public Transform feet;
    public LayerMask groundLayers;
    public LayerMask wallLayers;

    //Dash
    public static float dashDistance = 15f;
    public static float velocityAfterDashMultiplier = 5;
    public bool isDashing;
    public bool dashCharge = false;
    public float gravityHelp;

    //Death
    public GameObject deathText;
    public bool isDead = false;

    Rigidbody2D rb;

    //WallJumpy

    public static float wallJumpVelocityBase = 0.75f;
    public float wallSlideSpeed = -3f;
    public float wallDistance = 0.5f;
    public bool isWallSliding = false;
    public bool wallJumpCharge = true;
    
    RaycastHit2D WallCheckHit;
    public float wallJumpVelocity = wallJumpVelocityBase;
    public float wallJumpTime = 0.05f;
    public float jumpTime;
    

    //TitleScreenEscape
    public string titleScreen;


    // Awake is called when the sonic is initialised. here we just add rb for later purpouse
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityHelp = rb.gravityScale;
        deathText = GameObject.Find("deathText");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(titleScreen);
        }

        if (isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * velocity, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
            dust.Play();
            //maybe also check lastDirection to further make wallJumping on one wall impossible
        }
        else if (isWallSliding && wallJumpCharge && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, wallJumpVelocity);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * velocity, ForceMode2D.Impulse);

            if (wallJumpVelocity >= 0)
            {
                dust.Play();
            }

            wallJumpVelocity = wallJumpVelocity - 8f;
            animator.SetBool("Jump", true);
            wallJumpCharge = false;
        }

        if (isDashing == false && dashCharge == true)
        {
            //Dash Up Right
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.J))
            {
                StartCoroutine(Dash(Vector2.up + Vector2.right));
            }
            //Dash Up Left
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.J))
            {
                StartCoroutine(Dash(Vector2.up + Vector2.left));
            }
            //Dash Down Right
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.J))
            {
                StartCoroutine(Dash(Vector2.down + Vector2.right));
            }
            //Dash Down Left
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.J))
            {
                StartCoroutine(Dash(Vector2.down + Vector2.left));
            }
            //Dash left
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.J))
            {
                StartCoroutine(Dash(Vector2.left));
            }
            //Dash right
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.J))
            {
                StartCoroutine(Dash(Vector2.right));
            }
            //Dash up
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.J))
            {
                StartCoroutine(Dash(Vector2.down));
            }
            //Dash down
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.J))
            {
                StartCoroutine(Dash(Vector2.up));
            }
        }

        //Wall Jump

        if (lastDirection == 1)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, wallLayers);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallLayers);
        }

        if (WallCheckHit && !isGrounded() && xInput != 0)
        {
            isWallSliding = true;
            animator.SetBool("WallSliding", true);
            wallJumpCharge = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
            animator.SetBool("WallSliding", false);
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }
        else
        {
            wallJumpVelocity = wallJumpVelocityBase;
        }
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        if (isDashing == false)
        {
            //Falling faster and Jump being based on Pressduration respectivly 
            if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        
            //get input values
            xInput = Input.GetAxis("Horizontal");
            //float yInput = Input.GetAxis("Vertical");

            animator.SetFloat("Speed", Mathf.Abs(xInput));

            // compute new position of sonic
            xDist = xInput * moveSpeed * Time.deltaTime;
            //float yDist = yInput * moveSpeed * Time.deltaTime;


            //before Mathf.Abs(transform.position.x + xDist) > 8.22 --> xDist =0 now in OnCollisonStay
            if (isWallSliding)
            {
                xDist = 0;
            }

            if ((xInput < 0 && lastDirection == 1) || (xInput > 0 && lastDirection == -1))
            {
                transform.Rotate(0, 180, 0);
                lastDirection *= -1;

                if (isGrounded())
                {
                    dust.Play();
                }
            }
            transform.position = transform.position + new Vector3(xDist, 0, 0);
        }    
    }


    public bool isGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.2f, groundLayers);

        if (groundCheck != null)
        {
            dashCharge = true;
            wallJumpCharge = true;
            return true; 
        }
        return false;
    }

    public IEnumerator Dash(Vector2 direction)
    {

        isDashing = true;
        animator.SetBool("Dash", true);
        if(lastDirection == 1) {
            dash.GetComponent<ParticleSystemRenderer>().material = matRight;
        }
        else
        {
            dash.GetComponent<ParticleSystemRenderer>().material = matLeft;
        }

        dash.Play();
        dashCharge = false;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        GetComponent<Rigidbody2D>().AddForce(direction  * dashDistance, ForceMode2D.Impulse);
        
        rb.gravityScale = 0;

        //Dash DURATION!!
        yield return new WaitForSeconds(0.15f);
        //adjust multiplier to adjust how much velocity is added after dash
        rb.velocity = new Vector2(direction.x * velocityAfterDashMultiplier, direction.y* velocityAfterDashMultiplier);
        rb.gravityScale = gravityHelp;
        isDashing = false;

        dash.Stop();
        animator.SetBool("Dash", false);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isGrounded())
        {
            animator.SetBool("Jump", false);
        }

        if (col.gameObject.tag == "Enemy")
        {
            isDead = true;
            deathText.transform.position = new Vector3(deathText.transform.position.x, deathText.transform.position.y, 0);
            deathText.SetActive(true);

            transform.position = new Vector3(0, -3f, 1);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Renderer>().enabled = false;

            Invoke("nextLife", 2);
        }
    }

    private void nextLife()
    {
        isDead = false;
        deathText.SetActive(false);

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Renderer>().enabled = true;

        rb.gravityScale = gravityHelp;
    }
}

