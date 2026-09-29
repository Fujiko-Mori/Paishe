using UnityEngine;
using UnityEngine.InputSystem;

public class Paisheur : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    private bool isGrounded;
    private Vector2 move;
    public InputAction moveAction;
    Rigidbody2D rigidbody2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = moveAction.ReadValue<Vector2>();
        Vector2 position = move * speed;
        //rigidbody2d.AddForceX(position.x);
        rigidbody2d.linearVelocityX = position.x;
        if (isGrounded) 
        { 
            rigidbody2d.AddForceY(jumpForce * Mathf.Clamp(move.y,0,1));
            isGrounded = false;
        }
        //Le perso est à y = -1.25, c'est trop rudimentaire pour le moment !
        if (this.transform.position.y < -1f)
        {
            isGrounded = true;
        }

    }
}
