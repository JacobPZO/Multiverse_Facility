using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // 1
    public float moveSpeed = 10f;
    public float rotateSpeed = 125f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public float bulletSpeed = 100f;

    // 2
    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;

    private GameBehavior _gameManager;

    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
        if (collision.gameObject.name == "Cat")
        {
            _gameManager.HP -= 3;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();

            GameObject newBullet = Instantiate(bullet,
                this.transform.position + new Vector3(1, 0, 0),
                    this.transform.rotation) as GameObject;

            Rigidbody bulletRB =
                newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward *
                                           bulletSpeed;
        }
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            playerJump();
        }
        // 3
        vInput = Input.GetAxis("Vertical") * moveSpeed;

        // 4
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        // 5
        /*
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);

        // 6
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    private float speedMultiplier;

    public void ShowGuy()
    {
        
    }
    public void BoostSpeed(float multiplier)
    {
        speedMultiplier = multiplier;
        moveSpeed *= multiplier;
        rotateSpeed *= multiplier;
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
            _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center,
            capsuleBottom, distanceToGround, groundLayer,
                QueryTriggerInteraction.Ignore);
        return grounded;
    }
}
