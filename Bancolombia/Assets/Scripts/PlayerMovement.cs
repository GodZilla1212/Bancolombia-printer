using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    public float speed;
    float actualSpeed;
    bool jump = false;
    float horizontalMove = 0f;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * actualSpeed;
        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void EbanleMovement ()
    {
        actualSpeed = speed;
    }

    public void DisableMovement ()
    {
        actualSpeed = 0;
    }
}
