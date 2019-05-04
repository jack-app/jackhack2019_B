using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class DuelMatchMaker : MonoBehaviourPunCallbacks
{
    public bool inDuel = false;
    public GameObject HandCardPrefab;
    
    const string GameVersion = "1";
    const string RoomName = "room";

    private List<GameObject> HandCards;

    public static void StartMatchMaking()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom(RoomName, new RoomOptions(), new TypedLobby());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("I'm in the room.");
    }

    void Update()
    {
        //ルーム内の人が2人以上になったらゲーム開始
        if (PhotonNetwork.InRoom && inDuel == false)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
            {
                StartDuel();
            }
        }

        if (inDuel)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                //if()
            }
        }
    }
    
    void StartDuel()
    {
        Debug.Log("Duel Standby!");
        inDuel = true;
        
        //位置情報の共有
        
        //手札の生成
        Vector3 summonPosition1 = Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * 1f;
        PocketHumanData handCard1 = UserDataManager.LoadRandomPocketHumanData();
        GameObject obj1 = Instantiate(HandCardPrefab, summonPosition1, Camera.main.transform.rotation);
        obj1.GetComponent<HandCard>().pocketHumanData = handCard1;
        
        Vector3 summonPosition2 = Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * 1f + Vector3.left;
        PocketHumanData handCard2 = UserDataManager.LoadRandomPocketHumanData();
        GameObject obj2 = Instantiate(HandCardPrefab, summonPosition2, Camera.main.transform.rotation);
        obj2.GetComponent<HandCard>().pocketHumanData = handCard2;
        
        Vector3 summonPosition3 = Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * 1f + Vector3.right;
        PocketHumanData handCard3 = UserDataManager.LoadRandomPocketHumanData();
        GameObject obj3 = Instantiate(HandCardPrefab, summonPosition3, Camera.main.transform.rotation);
        obj3.GetComponent<HandCard>().pocketHumanData = handCard3;

    }

    public static void SetReady()
    {
        PhotonNetwork.SetPlayerCustomProperties(new Hashtable() { { "ReadyToFight" , true } });
    }
}