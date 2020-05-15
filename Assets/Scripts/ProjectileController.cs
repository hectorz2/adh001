using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region Attributes

    private Collider2D collider;
    private Rigidbody2D rb;

    #endregion Attributes


    #region LifeCycle

    private void Start()
    {
        this.collider = this.GetComponent<Collider2D>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(this.rb.velocity != new Vector2(0,0))
        {
            this.transform.rotation = Quaternion.LookRotation(this.rb.velocity);
        }
    }

    #endregion LifeCycle
}
