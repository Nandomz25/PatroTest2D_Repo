using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Librer�a necesaria para leer el New Input System

public class PlayerController2DX : MonoBehaviour
{
    //Referecias privadas generales
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] PlayerInput playerInput;
    Vector2 moveInput; //Variable para referenciar el input de los controladores

    [Header("Movement Parameters")]
    public float speed;
    [SerializeField] bool isFacingRight;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;



    // Start is called before the first frame update
    void Start()
    {
        //Para autoreferenciar: nombre de variable = GetComponent<tipodevariable>()
        playerRB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        if (moveInput.x > 0 && !isFacingRight) Flip();
        if (moveInput.x < 0 && isFacingRight) Flip();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        playerRB.velocity = new Vector3(moveInput.x * speed,playerRB.velocity.y,0);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    void GroundCheck()
    {
        //isGrounded es verdadero cuando el circulo detector toque la layer ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }

    #region Input Methods
    //M�todos que permiten leer el input del New Input System
    //Crearemos un m�todo por cada acci�n

    public void HandleMovement(InputAction.CallbackContext context)
    {
        //Las acciones de tpo VALUE deben almacenarse = ReadValue
        moveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
       
        }

    }

    #endregion



}
