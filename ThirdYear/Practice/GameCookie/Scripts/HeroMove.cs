using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{

    public Rigidbody2D body;
    public float playerSpeed;
    public float jumpPower;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;
    public KeyCode addForceButton = KeyCode.W;
    public static bool isFacingRight = true;
    private int directionInput;
    private Vector2 direction;
    public static bool canJump;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        canJump = true;
    }


    void FixedUpdate()
    {
        
        body.AddForce(direction * body.mass * playerSpeed / 2);
        //body.velocity = new Vector2(playerSpeed * directionInput, body.velocity.y);

        if (Input.GetKey(addForceButton) && canJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            canJump = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
       
    void Update()
    {
        if (Input.GetKey(leftButton)) directionInput = -1;
        else if (Input.GetKey(rightButton)) directionInput = 1;
        else directionInput = 0;
        direction = new Vector2(directionInput, 0);
        if (directionInput > 0 && !isFacingRight) Flip();
        else if (directionInput < 0 && isFacingRight) Flip();
    }
}
