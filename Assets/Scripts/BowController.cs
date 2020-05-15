using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{

    private GameObject arrowPrefab;

    public float initialVelocity = 5f;

    // Start is called before the first frame update
    void Start()
    {
        this.arrowPrefab = (GameObject)Resources.Load("Prefabs/Arrow", typeof(GameObject));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            launchArrow();
        }

    }

    void launchArrow()
    {

        GameObject newArrow = Instantiate(this.arrowPrefab) as GameObject;

        newArrow.transform.position = this.transform.position + newArrow.GetComponent<BoxCollider2D>().bounds.size/2;

        Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();

        Camera camera = Camera.main;
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 bowPosition = this.transform.position;

        Vector3 shootDirection = (mousePosition - bowPosition).normalized;

        rb.velocity = new Vector2(shootDirection.x * this.initialVelocity, shootDirection.y * this.initialVelocity);
    }
}
