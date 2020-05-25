using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerScript : MonoBehaviour
{
    public float speed;                 //Floating point variable to store the player's movement speed.
    public float forceY;

    private bool jumping = false;     
    private Rigidbody2D rb;
    private List<int> idsCollisioned = new List<int>();

    private bool facingRight = true;
    //SpriteRenderer playerRender; spriteanimation

    // Use this for initialization
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
      //  playerRender = GetComponent<SpriteRenderer>(); spriteanimation
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveX = 0f;
        float moveY = 0f;
               
        if(Input.GetKey("d") || Input.GetKey("a")) 
        {
            moveX = moveHorizontal * speed;

            //if(facingRight == false && moveHorizontal >0) spriteanimation
            //{
              //  Rotation();
            //}
           // else
           // {
             //   Rotation();
           // }
        }
        if(Input.GetKey("w"))
        {           

            if(!jumping)
            {
                Debug.Log("Saltando");
                this.rb.AddForce(transform.up * this.forceY, ForceMode2D.Impulse);

                jumping = true;
            }
            
        }

        transform.Translate(moveX * this.speed, 0.0f, 0.0f);
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collisione");
        if(collider.gameObject.CompareTag("Section"))
        {
            Debug.Log("Seccion sexy");
            int id = collider.GetInstanceID();
            Debug.Log("Id: " + id);
            if (!this.idsCollisioned.Contains(id))
            {
                int position = this.idsCollisioned.Count + 1;
                Debug.Log("Position: " + position);
                this.idsCollisioned.Add(id);

                GameScript sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
                sc.AddSection(position);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && rb.velocity.y < 0.01)
        {
            Debug.Log("Suelo");
            this.jumping = false;
        }
    }

    //private void Rotation() spriteanimation
   // {
   //     facingRight = !facingRight;
   //     playerRender.flipX = !playerRender.flipX;
   // }
}
