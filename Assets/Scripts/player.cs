using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace com.Multi_Player_Game2.Multi_Player_Game2{
public class player : MonoBehaviourPunCallbacks
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public GameObject playerBUllet;
    public GameObject muzzlePoint;
    public Camera fpscamera;

  

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()

    {


        groundedPlayer = controller.isGrounded;
        

        Vector3 move = new Vector3(SimpleInput.GetAxis("Horizontal"), 0, SimpleInput.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero || photonView.IsMine)
        {
            gameObject.transform.forward = move;
           
        }

        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

       
    }


    public void playerShoot()

    {
        RaycastHit hit;
        GameObject bulletClone;
        Vector3 pos =muzzlePoint.transform.position;
        Debug.Log(pos);
         
        Debug.DrawRay(gameObject.transform.position,gameObject.transform.forward*10,Color.red);
        if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward*10,out hit ,200f)){
           
            Debug.Log(hit.transform.name);
             if(PhotonNetwork.IsConnected){
            
           bulletClone =  PhotonNetwork.Instantiate(playerBUllet.name,hit.point,Quaternion.LookRotation(hit.normal));
       
         }
         else{
             
             bulletClone =  Instantiate(playerBUllet,hit.point,Quaternion.LookRotation(hit.normal));
       
            }

            health health = hit.transform.GetComponent<health>();
            if(health!= null ||  !health.GetComponent<PhotonView>().IsMine){

                health.takeDmage();
            }

            Destroy(bulletClone,3f);
            //bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0,0,6f));
        }
         
        
        
        
    }
}
}