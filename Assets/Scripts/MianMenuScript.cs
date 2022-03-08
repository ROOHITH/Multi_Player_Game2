using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace com.Multi_Player_Game2.Multi_Player_Game2{
public class MianMenuScript : MonoBehaviourPunCallbacks
{
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;
    public List<GameObject> unityGameObjectsPanels = new List<GameObject>();
     
     public InputField UserInputField;
    public InputField RoomInputField;
     public string User = null;
   
    public string Room = null;
    string gameVersion = "1";

    public GameObject PlayerViewPortContent;
     public GameObject playernametext;
      public GameObject Roomnametext;
     


      void Awake()
        {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }
   // public GameObject textDisplay;
    // Start is called before the first frame update
    void Start()
    {
       activatePanel(0);
       

    }

    // Update is called once per frame
    void Update()
    {   
        
       
    }
     void activatePanel(int u){
         Debug.Log(unityGameObjectsPanels[0]);
         Debug.Log(unityGameObjectsPanels.Count);
         int i=0;
         for(i=0;i<(unityGameObjectsPanels.Count)-1;i++){
           unityGameObjectsPanels[i].SetActive(false);  
         }
         unityGameObjectsPanels[u].SetActive(true);
    }
    
    public void StoreName()
    {   
      // User = unityGameObjectsPanels[0].transform.GetChild(0).GetComponent<Text>().text;
        
        //User = Username_field.text.ToString();
        User = UserInputField.GetComponent<InputField>().text;
        Debug.Log(User);
        

    }
   
     
    
    
    public void storeRoom(){

         
         Room = RoomInputField.GetComponent<InputField>().text;
         Debug.Log(Room);

    }

        public void Connect()
        {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
                 StoreName();
                 storeRoom();
                 PhotonNetwork.NickName=User;
                
        }

        public override void OnConnectedToMaster()
        {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN"+Room);
          
            if(User != null && User != "" && PhotonNetwork.IsConnected && Room!=null)
        {
            PhotonNetwork.JoinOrCreateRoom(Room, new RoomOptions { MaxPlayers = maxPlayersPerRoom },TypedLobby.Default);
        } 
        }
       
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
           
        }

        public override void OnJoinedRoom()
        {
          Debug.LogWarningFormat("room joined");
                 GameObject obj;
                    
                    Roomnametext.GetComponentInChildren<Text>().text = Room;
                activatePanel(1);
                foreach (var player in PhotonNetwork.PlayerList) // 2 Players Room
                    {
                        obj = Instantiate(playernametext);
                    obj.transform.parent = PlayerViewPortContent.transform;
                    obj.GetComponentInChildren<Text>().text = player.ToString() ;
                    Debug.LogWarningFormat(player.ToString() );
                    if(player.IsMasterClient){
                             Debug.LogWarningFormat(player.ToString()+"is master" );
                    }
                    }
        }
       public override void OnCreatedRoom(){

                Debug.LogWarningFormat("room created");
                
        }
        public override void OnCreateRoomFailed(short returnCode, string message){

            Debug.LogWarningFormat("Room createtion failed");
            activatePanel(0);
            

        }

        public void onPlay(){
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogWarningFormat("PhotonNetwork : Trying to Load a level by the master Client");
                PhotonNetwork.LoadLevel(1);
            }

        }
        
        
        

    
}
}
