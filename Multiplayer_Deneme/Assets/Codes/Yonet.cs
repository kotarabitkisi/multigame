using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Yonet : MonoBehaviourPunCallbacks
{
    public GameObject MainMenu, RoomMenu;
    public TMP_InputField PlayerNameText;
    public TextMeshProUGUI NameInput,connectionInfo;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {      
        PhotonNetwork.AutomaticallySyncScene =true;
        PhotonNetwork.ConnectUsingSettings();
    }

   public override void OnConnectedToMaster()
    {
        connectionInfo.text = "connection success";
        PhotonNetwork.JoinLobby();
    }
    public void QuickFind()
    {
        PhotonNetwork.NickName = NameInput.text;
        MainMenu.SetActive(false);
        RoomMenu.SetActive(true);
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    [PunRPC]
   public void setplayerlist() 
    {
        PlayerNameText.text = "";
        int i = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            i++;
            PlayerNameText.text +=i.ToString()+". "+player.NickName+"\n";
        }
    }
    public void CreateRoom()
    {
        MainMenu.SetActive(false);
        RoomMenu.SetActive(true);
        PhotonNetwork.CreateRoom("name",null,null);
    }
    public void JoinRoom()
    {
        MainMenu.SetActive(false);
        RoomMenu.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnCreatedRoom()
    {
        photonView.RPC("setplayerlist", RpcTarget.AllBuffered);
    }
    public override void OnJoinedRoom()
    {
        photonView.RPC("setplayerlist", RpcTarget.AllBuffered);
    }
   
    public void Exit()
    {
        Application.Quit();
    }
    public void StarttoPlay()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level 1");
        }
    }
    
}
