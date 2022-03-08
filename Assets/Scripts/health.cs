using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace com.Multi_Player_Game2.Multi_Player_Game2{
 
public class health : MonoBehaviourPunCallbacks
{
     public  float  healthCount;
    // Start is called before the first frame update
    void Start()
    {
        healthCount= 30;
    }

    // Update is called once per frame
    void Update()
    {
        
            
            
    }
    
    public void takeDmage(){


            healthCount= healthCount-5;
            if(healthCount == 0){
                Destroy(gameObject);
                PhotonNetwork.LeaveRoom();
            }

    }
}
}
