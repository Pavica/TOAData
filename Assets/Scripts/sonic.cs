using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class sonic : MonoBehaviour
{
    //animation
    public Animator animator;

    //changing directions
    public int lastDirection = 1;

    //movement
    public float moveSpeed = 15.0f;
    public float velocity = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //jumpability
    public Transform feet;
    public LayerMask groundLayers;

    //Dash
    public static float dashDistance = 15f;
    public bool isDashing;
    public bool dashCharge = false;
    public float gravityHelp;

    //Death
    public GameObject deathText;
    public bool isDead = false;

    Rigidbody2D rb;

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
        if (isDead)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * velocity, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
        }

        if(isDashing == false && dashCharge == true)
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
            float xInput = Input.GetAxis("Horizontal");
            //float yInput = Input.GetAxis("Vertical");

            animator.SetFloat("Speed", Mathf.Abs(xInput));

            // compute new position of sonic
            float xDist = xInput * moveSpeed * Time.deltaTime;
            //float yDist = yInput * moveSpeed * Time.deltaTime;

            if (Mathf.Abs(transform.position.x + xDist) > 8.22)
            {
                xDist = 0;
            }

            if ((xInput < 0 && lastDirection == 1) || (xInput > 0 && lastDirection == -1))
            {
                transform.Rotate(0, 180, 0);
                lastDirection *= -1;
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
            return true;
            
        }
        return false;
    }

    public IEnumerator Dash(Vector2 direction)
    {

        isDashing = true;
        animator.SetBool("Dash", true);
        dashCharge = false;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        GetComponent<Rigidbody2D>().AddForce(direction  * dashDistance, ForceMode2D.Impulse);
        
        rb.gravityScale = 0;

        //Dash DURATION!!
        yield return new WaitForSeconds(0.15f);
        rb.velocity = direction;
        rb.gravityScale = gravityHelp;
        isDashing = false;

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

            transform.position = new Vector3(0, -4f, 1);
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

