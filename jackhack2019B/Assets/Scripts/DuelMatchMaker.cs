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
            //readyの人数
            int readyCount = 0;
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.ContainsKey("RTF"))
                {
                    if ((bool) player.CustomProperties["RTF"])
                    {
                        readyCount++;
                    }
                }
            }

            //readyの人が2人以上いたらじゃんけんぽん
            if (readyCount >= 1)
            {
                foreach (var player in PhotonNetwork.PlayerList)
                {
                    if (player.CustomProperties.ContainsKey("RTF"))
                    {
                        if ((bool) player.CustomProperties["RTF"])
                        {
                            player.SetCustomProperties(new Hashtable() {{"RTF", false}});
                        }
                    }
                }
                Janken();
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
        
        Vector3 summonPosition2 = Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * 1f + Camera.main.transform.rotation * Vector3.left;
        PocketHumanData handCard2 = UserDataManager.LoadRandomPocketHumanData();
        GameObject obj2 = Instantiate(HandCardPrefab, summonPosition2, Camera.main.transform.rotation);
        obj2.GetComponent<HandCard>().pocketHumanData = handCard2;
        
        Vector3 summonPosition3 = Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * 1f + Camera.main.transform.rotation * Vector3.right;
        PocketHumanData handCard3 = UserDataManager.LoadRandomPocketHumanData();
        GameObject obj3 = Instantiate(HandCardPrefab, summonPosition3, Camera.main.transform.rotation);
        obj3.GetComponent<HandCard>().pocketHumanData = handCard3;
        
        
    }

    void Janken()
    {
        string bloodtype_mine = PhotonNetwork.LocalPlayer.CustomProperties["BT"].ToString();
        string bloodtype_enemy = "A";
        foreach (var player in PhotonNetwork.PlayerListOthers)
        {
            bloodtype_enemy = ZassoUtility.FindPocketHumanData(player.CustomProperties["BT"].ToString()).BloodType;
        }

        //勝ちパターン
        if ((bloodtype_mine == "A" && bloodtype_enemy == "O")
            || (bloodtype_mine == "O" && bloodtype_enemy == "B")
            || (bloodtype_mine == "B" && bloodtype_enemy == "A"))
        {
            Debug.Log("Win!");
        }
        //負けパターン
        else if ((bloodtype_mine == "A" && bloodtype_enemy == "B")
            || (bloodtype_mine == "O" && bloodtype_enemy == "A")
            || (bloodtype_mine == "B" && bloodtype_enemy == "O"))
        {
            Debug.Log("Lose...");
        }
        //引き分けパターン
        else
        {
            Debug.Log("Tie");
        }
    }

    public static void SetReady(string blood_type)
    {
        PhotonNetwork.SetPlayerCustomProperties(new Hashtable() { { "RTF" , true } });
        PhotonNetwork.SetPlayerCustomProperties(new Hashtable(){ { "BT", blood_type } });
    }
}