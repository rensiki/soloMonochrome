using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    Transform leftEye;
    Transform rightEye;
    Transform leftEar;
    Transform rightEar;
    Transform tail;
    Transform bPos;

    Vector2 leftEye_Pos;
    Vector2 rightEye_Pos;
    Vector2 leftEar_Pos;
    Vector2 rightEar_Pos;
    Vector2 tail_Pos;
    Vector2 bPos_Pos;



    float rotateTimer = 0;
    float x = 0;
    bool isGround;
    public float moveSpeed = 2f;
    public float moveMaxSpeed = 2f;
    public float jumpPower = 5f;
    public float x_maxSpeed = 10f;
    public float y_maxSpeed = 10f;
    bool isJumping = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        leftEye = transform.GetChild(0);
        rightEye = transform.GetChild(1);
        leftEar = transform.GetChild(2);
        rightEar = transform.GetChild(3);
        tail = transform.GetChild(4);
        bPos = transform.GetChild(5);

        leftEye_Pos = leftEye.localPosition;
        rightEye_Pos = rightEye.localPosition;
        leftEar_Pos = leftEar.localPosition;
        rightEar_Pos = rightEar.localPosition;
        tail_Pos = tail.localPosition;
        bPos_Pos = bPos.localPosition;

    }
    void Update()
    {
        VelocityLimit();
        PlayerJumping();
    }
    void FixedUpdate() {

        PlayerMoving();
    }

    void PlayerJumping()
    {
        /*if (Input.GetButtonDown("Jump")&& isGround)
        //&& transform.rotation == Quaternion.Euler(0, 0, 0)
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            //애니메이션 효과 적용할때 다시 고려
            //isJumping = true;
            rb.velocity += new Vector2(0,jumpPower);
            Debug.Log("Jump");
        }*/
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            rb.velocity += new Vector2(0, jumpPower);
            Debug.Log("Jump");
        }


    }
    void VelocityLimit()
    {
        //x축 속도 제한
        if (rb.velocity.x > x_maxSpeed)
        {
            rb.velocity = new Vector2(x_maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -x_maxSpeed)
        {
            rb.velocity = new Vector2(-x_maxSpeed, rb.velocity.y);
        }
        //y축 속도 제한
        if (rb.velocity.y > y_maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, y_maxSpeed);
        }
        else if (rb.velocity.x < -y_maxSpeed)
        {
            rb.velocity = new Vector2(-rb.velocity.x, y_maxSpeed);
        }
    }
    void PlayerMoving()
     {
        x = Input.GetAxis("Horizontal");
        if(!(x>0&&rb.velocity.x > moveMaxSpeed) && 
            !(x<0&&rb.velocity.x < -moveMaxSpeed))
        {
            rb.velocity += new Vector2(x * moveSpeed, 0);
        }

        if (isGround && transform.rotation == Quaternion.Euler(0, 0, 0))
            {
                if (x > 0)
                {
                    //Debug.Log("Right");
                    leftEye.localPosition = leftEye_Pos;
                    rightEye.localPosition = rightEye_Pos;
                    leftEar.localPosition = leftEar_Pos;
                    rightEar.localPosition = rightEar_Pos;
                    tail.localPosition = tail_Pos;
                    bPos.localPosition = bPos_Pos;

                    tail.localRotation = Quaternion.Euler(0, 0, -20);
                }
                else if (x < 0)
                {
                    //Debug.Log("Left");
                    leftEye.localPosition = new Vector2(leftEye_Pos.x * -1, leftEye_Pos.y);
                    rightEye.localPosition = new Vector2(rightEye_Pos.x * -1, rightEye_Pos.y);
                    leftEar.localPosition = new Vector2(leftEar_Pos.x * -1, leftEar_Pos.y);
                    rightEar.localPosition = new Vector2(rightEar_Pos.x * -1, rightEar_Pos.y);
                    tail.localPosition = new Vector2(tail_Pos.x * -1, tail_Pos.y);
                    bPos.localPosition = new Vector2(bPos_Pos.x * -1, bPos_Pos.y);

                    tail.localRotation = Quaternion.Euler(0, 0, 20);   
            }
            }

     }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
            if (other.gameObject.tag == "Ground")
            {
                //Debug.Log("Not Ground");
                isGround = false;
                rotateTimer = 0;
            }
    }

    void OnCollisionStay2D(Collision2D other)
    {
            /*if(isJumping)
            {
                isJumping = false;
            }*/
            if (other.gameObject.tag == "Ground")
            {
                //Debug.Log("Ground");
                isGround = true;
                //timer 코딩
                if (transform.rotation != Quaternion.Euler(0, 0, 0))
                {
                    rotateTimer += Time.deltaTime;
                    if (rotateTimer > 2f)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        rotateTimer = 0;
                    }
                }
            }
    }


}

