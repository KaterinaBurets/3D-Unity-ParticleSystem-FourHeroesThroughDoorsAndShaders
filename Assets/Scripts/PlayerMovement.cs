using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;

    private Vector3 velocity;
    private bool isGrounded = false;

    [SerializeField] private Animator _characterAnim;

    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        WalkAnimation();

        isGrounded = false;
    }

    private void WalkAnimation()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            _characterAnim.SetBool("isIdle", false);
            _characterAnim.SetBool("isWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            _characterAnim.SetBool("isWalking", false);
            _characterAnim.SetBool("isIdle", true);
        }
    }
}    