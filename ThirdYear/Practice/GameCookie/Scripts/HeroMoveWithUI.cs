using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveWithUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb2d;
    public float playerSpeed;
    public float jumpPower;
    public int directionInput;
    private Vector2 direction;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        HeroMove.canJump = true;
    }  

    void FixedUpdate()
    {
        direction = new Vector2(directionInput, 0);
        //rb2d.AddForce(direction * rb2d.mass * playerSpeed / 2);
        rb2d.velocity = new Vector2(playerSpeed * directionInput, rb2d.velocity.y);        
    }

    public void Move(int InputAxis)
    {
        directionInput = InputAxis;
    }

    public void Jump()
    {   
        if (HeroMove.canJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
            HeroMove.canJump = false;
        }
    }
    void Flip()
    {        
        HeroMove.isFacingRight = !HeroMove.isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Update()
    {
        if ((directionInput < 0) && (HeroMove.isFacingRight))
        {
            Flip();
        }

        if ((directionInput > 0) && (!HeroMove.isFacingRight))
        {
            Flip();
        }

    }


}


