using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    float hor;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jumping")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool wasGrounded;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private int maxJumps = 2;
    int jumpRemaining;//kalan zýplama hakkýmýz

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private Vector2 GroundCheckSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private LayerMask GroundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        flip();
    }
    private void Update()
    {
        GroundCheck();//bu kodu kalan zýpklama hakkýmýzý kontrol etmek için koyuyoruz eðer jumpRemaining>0 zýplayabiliyoruz.
        jump();
    }
    private void Move()
    {
        hor = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(hor * moveSpeed, rb.linearVelocity.y);
    }
    private void jump()
    {
        if (jumpRemaining > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
                //rb.AddForce(Vector2.up*jumpSpeed,ForceMode2D.Impulse);
                jumpRemaining--;
            }
        }

    }
    private void GroundCheck()
    {
        wasGrounded = isGrounded;//az önce yerdemiydim.

        isGrounded = Physics2D.OverlapBox(groundCheckPos.position, GroundCheckSize, 0, GroundLayer);//þu an yerdemiyim.


        if (wasGrounded == false && isGrounded == true)//eðer az önce havadaysam ve þimdi yere deðdiysem zýplama hakkýmý yenile (!wasGrounded && isGrounded)
        {
            jumpRemaining = maxJumps;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheckPos.position, GroundCheckSize);
    }
    void flip()
    {
        if (hor > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        if (hor < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
    }
}
