using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.

    private Collider2D playerCollider;

    private GameManager gameManager;
    private CameraController cameraController;

    private List<int> idsCollisioned = new List<int>();

    // Use this for initialization
    void Start()
    {
        this.playerCollider = GetComponent<Collider2D>();
        this.gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        this.cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float translation = moveHorizontal * this.speed;

        float cameraLimit = this.cameraController.GetCameraStartX();
        //If the player is goint to leave the camera
        if(this.playerCollider.bounds.min.x + translation < cameraLimit)
        {
            //The translation is the difference, so if is 0, it doesn't move
            translation = cameraLimit - this.playerCollider.bounds.min.x;
        }

        transform.Translate(translation, moveVertical * 0.15f, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Section"))
        {
            int id = collider.GetInstanceID();
            if (!this.idsCollisioned.Contains(id))
            {
                int position = this.idsCollisioned.Count + 1;
                this.idsCollisioned.Add(id);

                this.gameManager.AddSection(position);

                float colliderStartX = collider.bounds.min.x;
                this.cameraController.SetLeftLimit(colliderStartX);
                if(position == 1) //The first generated room by player
                {
                    this.cameraController.SetCameraStartOnLimit();
                }
            }
        }
    }
}
