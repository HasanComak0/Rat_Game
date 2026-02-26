using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpSpeed;
    private Rigidbody2D rb;
    public bool doubleJump;

    public Transform groundCheck;
    public float circleRadius = 0.1f;
    public LayerMask _layerMask;
    float posX;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
                doubleJump= false;
            }
        }
        flip();
    }
    private void FixedUpdate()
    {
        posX = Input.GetAxisRaw("Horizontal");

        Vector2 velocity = rb.linearVelocity;
        velocity.x = posX * speed;
        rb.linearVelocity = velocity;
    }
    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, circleRadius, _layerMask) != null;
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, circleRadius);
    }
    void flip()
    {
        if (posX > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        if (posX < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
    }
}
