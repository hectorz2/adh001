using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    public GameObject player;


    private Vector3 offset;
    private float leftLimit = 0;
    private Vector3 lastPosition;

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        this.offset = transform.position - player.transform.position;
        this.lastPosition = transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + this.offset;
        float cameraStartX = this.GetCameraStartX();
        
        //Debug.DrawLine(new Vector2(this.limit, 0), new Vector2(this.limit, 20), Color.red, 1f);
        if (this.leftLimit != 0 && cameraStartX < this.leftLimit)
        {
            if(this.transform.position.x < this.lastPosition.x)
            {
                this.transform.position = new Vector3(this.lastPosition.x, this.transform.position.y, this.transform.position.z);
            }
        } 

        this.lastPosition = transform.position;
    }

    public void SetLeftLimit(float leftLimit)
    {
        this.leftLimit = leftLimit;
    }

    public float GetLeftLimit()
    {
        return this.leftLimit;
    }


    public float GetCameraStartX()
    {
        float cameraWidth = this.GetCameraWidth();
        float cameraStartX = transform.position.x - (cameraWidth / 2);

        return cameraStartX;
    }

    public void SetCameraStartOnLimit()
    {
        float width = this.GetCameraWidth();

        transform.position = player.transform.position + this.offset;
        transform.position = new Vector3(this.leftLimit + (width / 2), transform.position.y, transform.position.z);

        this.lastPosition = transform.position;
    }

    public float GetCameraWidth()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        return width;
    }
}
