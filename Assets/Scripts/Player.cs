using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;

    private CameraController cameraPlayer;

    private float speed;

    [SerializeField] private float WalkSpeed = 1;
    [SerializeField] private float RunSpeed = 1.5f;
    [SerializeField] private float jump = 10;
    [SerializeField] private float mainForce = 10;

    public bool canJump = true;
    public bool canPlay = true;
    public bool canRotate = true;
    public bool canDoubleJump = false;

    private bool isWalking = false;
    private bool isRunning = false;
    private bool isDead = false;
    private bool isJumping = false;
    private bool isDoubleJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraPlayer = this.GetComponent<CameraController>();
        speed = WalkSpeed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {       
        Move();
        if (canRotate)
        {
            this.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * 2, 0);
        }
    }

    public void Move()
    {
        float x = 0;
        float y = 0;
        if (canPlay)
        {
            x = Input.GetAxisRaw("Horizontal") * speed * mainForce;
            y = Input.GetAxisRaw("Vertical") * speed * mainForce;
            if (Input.GetKeyDown(KeyCode.Space)  && canJump)
            {
                if (!isJumping)
                    Jump();
                else if (canDoubleJump && !isDoubleJumping)
                    DoubleJump();
            }
           
        }

        Vector3 move = transform.right * x + transform.forward * y;
        Vector3 UpMove = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = move.normalized * speed * mainForce + UpMove; //Set Speed of player with same velocity on all axis

        if (Mathf.Abs(x)+Mathf.Abs(y) == 0)
        {
            isWalking = false;
            isRunning = false;
        }
      

        if (move.magnitude > 0.1f && !isJumping)
        {
           // MusicManager.Instance.playRun(runClip);
        }
        if (y < 0 && !isJumping)
        {
            RunBack();
        }
        else if (rb.velocity.magnitude > 0.1f && !isJumping)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                Run();
            else Walk();
        }
        else if (!isJumping && x == 0 && y == 0)
        {
            playAnim("Standing");
        }

    }

    public void Walk()
    {
        cameraPlayer.FOVLoin();

        isRunning = false;
        isWalking = true;
        playAnim("Walking");
        speed = WalkSpeed;
    }
    public void RunBack()
    {
        isRunning = false;
        isWalking = true;
        speed = WalkSpeed;
        playAnim("RunBack");
    }
    public void Run()
    {
        cameraPlayer.FOVProche();
        isRunning = true;
        isWalking = false;
        playAnim("Running");
        speed = RunSpeed;
    }
    public void Jump()
    {
        rb.AddForce(transform.up * jump * mainForce, ForceMode.Impulse);
        isJumping = true;
       // MusicManager.Instance.playEffect(jumpClip);
        playAnim("Jump");
    }

    public void Land()
    {
        isDoubleJumping = false;
        isJumping = false;
        playAnim("Standing");
    }
    public void LookVector (Vector3 vectorLook) // Define Rotation Y with the Y direction of a Vector
    {
        transform.localRotation = Quaternion.Euler(0,vectorLook.y,0);
    }
    public void DoubleJump()
    {
        rb.AddForce(transform.up * jump * mainForce, ForceMode.Impulse);
        isDoubleJumping = true;
       // MusicManager.Instance.playEffect(jumpClip);
        playAnim("DoubleJump");
    }
    public void playAnim(string str)
    {
        anim.SetBool("RunBack", false);
        anim.SetBool("Standing", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Jump", false);
        anim.SetBool("DoubleJump", false);
        anim.SetBool("Die", false);
    
        anim.SetBool(str, true);
    }

    private void OnTriggerEnter(Collider collision)
    {
      
    }
    public bool getIsWalking()
    {
        return isWalking;
    }
    public bool getIsRunning()
    {
        return isRunning;
    }
    public bool getIsMoving()
    {
        return isWalking || isRunning;
    }

    public bool getIsJumping()
    {
        return isJumping;
    }

    public void ToDie()
    {
        StartCoroutine(Die());
    }
    private void OnTriggerExit(Collider collision)
    {


    }

    private void OnCollisionStay(Collision collision)
    {
       
    }

    private void OnCollisionExit(Collision collision)
    {
       
    }
    IEnumerator Die()
    {
      //  MusicManager.Instance.playEffect(dieClip);
        this.isDead = true;
        yield return new WaitForSeconds(2);
    }
}
