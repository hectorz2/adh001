using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.

    private List<int> idsCollisioned = new List<int>();

    // Use this for initialization
    void Start()
    {
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        transform.Translate(moveHorizontal * this.speed, moveVertical * 0.15f, 0.0f);
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(moveHorizontal * speed, moveVertical * 0.15f));
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
}
