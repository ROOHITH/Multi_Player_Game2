using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.Multi_Player_Game2.Multi_Player_Game2{
public class PlayerMovement : MonoBehaviour
{
     [Header("Movement")]
      [SerializeField] float moveSpeed = 6f;
    float horizontalMovement;
    float verticalMovement;
    Rigidbody rb;
    Vector3 moveDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
     private void Update()
    {
        

        MyInput();
        
    }
    void MyInput()
    {
        horizontalMovement = SimpleInput.GetAxisRaw("Horizontal");
        verticalMovement = SimpleInput.GetAxisRaw("Vertical");
        Debug.Log(horizontalMovement);
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }
   private void FixedUpdate() {
        MovePlayer();
    }
    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
    }
    
}
}
