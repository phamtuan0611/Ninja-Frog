using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Rigidbody2D theRB;
    [SerializeField] private float jumpForce;
    [SerializeField] private float runSpeed;
    private float activeSpeed;

    private bool isGrounded;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool canDoubleJump;

    [SerializeField] private Animator anim;

    [SerializeField] private float knockbackLength, knockbackSpeed;
    private float knockbackCounter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {

            //Check xem co o tren Ground khong
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            if (knockbackCounter <= 0)
            {
                activeSpeed = moveSpeed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    activeSpeed = runSpeed;
                }

                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y);

                // Input.GetButtomDown("Jump") == Phim Space
                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded == true)
                    {
                        Jump();
                        canDoubleJump = true;
                        anim.SetBool("isDoubleJumping", false);
                    }
                    else if (canDoubleJump == true)
                    {
                        Jump();
                        canDoubleJump = false;
                        //anim.SetBool("isDoubleJumping", true);
                        anim.SetTrigger("isDoubleJump");
                    }
                }

                //ChangeDirection
                if (theRB.velocity.x >= 0)
                {
                    transform.localScale = Vector3.one;
                }
                if (theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
            else
            {
                //Thoi gian knockback se keo dai 0.5 giay. Dieu nay co nghia la sau 0.5 giay, knockbackCounter se giam xuong 0, va nhan vat se khong con bi day lui nua.
                knockbackCounter -= Time.deltaTime;
                theRB.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, theRB.velocity.y);
            }

            //Handel Animation
            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("ySpeed", theRB.velocity.y);
        }
    }

    public void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        AudioManager.instance.allSFXPlay(14);
    }

    public void isKnock()
    {
        theRB.velocity = new Vector2(0f, jumpForce * 0.5f);
        anim.SetTrigger("isKnocking");
        knockbackCounter = knockbackLength;
    }

    public void BouncePlayer(float bounceAmount)
    {
        theRB.velocity = new Vector2(0f, bounceAmount);

        canDoubleJump = true;

        anim.SetBool("isGrounded", true);
    }
}
