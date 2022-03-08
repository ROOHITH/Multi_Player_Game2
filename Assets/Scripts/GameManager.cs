using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using UnityEngine.SceneManagement;


namespace com.Multi_Player_Game2.Multi_Player_Game2{

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerGameObject;
    public GameObject playerControllerUI;
    // Start is called before the first frame update
    void Start()
    {
        int randnum = Random.Range(0,400);
        PhotonNetwork.Instantiate(playerGameObject.name,new Vector3(0,1f,0),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
}
}
