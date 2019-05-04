using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.iOS;

public class DuelMatchMaker : MonoBehaviourPunCallbacks
{
    public bool inDuel = false;
    public GameObject HandCardPrefab;
    
    const string GameVersion = "1";
    const string RoomName = "room";

    private List<GameObject> handCards;

    void Start()
    {
        Init();
    }
    
    void Init()
    {
        handCards = new List<GameObject>();
    }
    
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
            if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
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
                Debug.Log("A");
                if (player.CustomProperties.ContainsKey("RTF"))
                {
                    if ((bool) player.CustomProperties["RTF"])
                    {
                        readyCount++;
                    }
                }
            }
            
            Debug.Log(readyCount);

            //readyの人が2人以上いたらじゃんけんぽん
            if (readyCount >= 2)
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

    [PunRPC]
    void ShareWorldMap(byte[] world_bytes)
    {
        ARWorldMap worldMap = ARWorldMap.SerializeFromByteArray(world_bytes);
    }
    
    void StartDuel()
    {
        Debug.Log("Duel Standby!");
        inDuel = true;
        
        //位置情報の共有
        var session = UnityARSessionNativeInterface.GetARSessionNativeInterface();
        session.GetCurrentWorldMapAsync(worldMap => {
            if (worldMap != null)
            {
                var worldMapInBytes = worldMap.SerializeToByteArray(); // ← これがシリアライズされたワールドマップのデータ
                GetComponent<PhotonView>().RPC("ShareWorldMap", RpcTarget.OthersBuffered, worldMapInBytes);
            }
            else
            {
                Debug.Log("worldMap is null");
            }
        });
        
        //手札の生成(初回のみ)
        if (handCards.Count == 0)
        {
            Vector3 summonPosition1 =
                Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * 1.5f;
            PocketHumanData handCard1 = UserDataManager.LoadRandomPocketHumanData();
            GameObject obj1 =
                PhotonNetwork.Instantiate(HandCardPrefab.name, summonPosition1, Camera.main.transform.rotation);
            obj1.GetComponent<HandCard>().SetPocketHumanData(handCard1);

            Vector3 summonPosition2 = Camera.main.transform.position +
                                      Camera.main.transform.rotation * Vector3.forward * 1.5f +
                                      Camera.main.transform.rotation * Vector3.left;
            PocketHumanData handCard2 = UserDataManager.LoadRandomPocketHumanData();
            GameObject obj2 =
                PhotonNetwork.Instantiate(HandCardPrefab.name, summonPosition2, Camera.main.transform.rotation);
            obj2.GetComponent<HandCard>().SetPocketHumanData(handCard2);

            Vector3 summonPosition3 = Camera.main.transform.position +
                                      Camera.main.transform.rotation * Vector3.forward * 1.5f +
                                      Camera.main.transform.rotation * Vector3.right;
            PocketHumanData handCard3 = UserDataManager.LoadRandomPocketHumanData();
            GameObject obj3 =
                PhotonNetwork.Instantiate(HandCardPrefab.name, summonPosition3, Camera.main.transform.rotation);
            obj3.GetComponent<HandCard>().SetPocketHumanData(handCard3);
            
            handCards.Add(obj1);
            handCards.Add(obj2);
            handCards.Add(obj3);
        }//2巡目以降
        else
        {
            //アクティブなやつをremoveして、非アクティブなやつをアクティブにする
            foreach (var handCard in handCards)
            {
                if (handCard.activeSelf)
                {
                    handCard.SetActive(false);
                    handCards.Remove(handCard);
                    break;
                }
            }

            foreach (var handCard in handCards)
            {
                handCard.SetActive(true);
            }
        }
    }

    void Janken()
    {
        string bloodtype_mine = PhotonNetwork.LocalPlayer.CustomProperties["BT"].ToString();
        string bloodtype_enemy = "A";
        foreach (var player in PhotonNetwork.PlayerListOthers)
        {
            bloodtype_enemy = player.CustomProperties["BT"].ToString();
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
        
        StartDuel();
    }

    public static void SetReady(string blood_type)
    {
        PhotonNetwork.SetPlayerCustomProperties(new Hashtable() { { "RTF" , true } });
        PhotonNetwork.SetPlayerCustomProperties(new Hashtable(){ { "BT", blood_type } });
    }
}