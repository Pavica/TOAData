using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonic : MonoBehaviour
{
    public Animator animator;
    float moveSpeed = 10.0f;
    int lastDirection = 1;
    Vector2 jumpHeight = new Vector2(0, 12);
    
    bool collidesFromAbove = true;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        

        //get input values
        float xInput = Input.GetAxis("Horizontal");
        //float yInput = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(xInput));

        //Jump
        if ((Input.GetKey(KeyCode.W)  || Input.GetKey(KeyCode.UpArrow)) && collidesFromAbove == true)
        {
            collidesFromAbove = false;
            GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
        }

        // compute new position of sonic
        float xDist = xInput * moveSpeed * Time.deltaTime;
        //float yDist = yInput * moveSpeed * Time.deltaTime;
    
        if(Mathf.Abs(transform.position.x + xDist) > 8.22){
            xDist = 0;
        }    

        if((xInput < 0 && lastDirection == 1) || (xInput > 0 && lastDirection == -1)){
            transform.Rotate(0, 180, 0);
            lastDirection *= -1;
        }
        transform.position = transform.position + new Vector3(xDist, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(transform.position.y > col.collider.gameObject.transform.position.y + 0.25){
            collidesFromAbove = true;
        }
    }
    
    /*
    private void OnCollisionEnter2D(Collision2D collision){
        collided = false;
        GameObject go = collision.gameObject;
        if(go.tag == "plattform"  && (go.transform.position.y > transform.position.y)){
            collided = true;
        }
    }*/

    private void OnCollisionExit2D(Collision2D col)
    {
        collidesFromAbove = false;
    }
}

