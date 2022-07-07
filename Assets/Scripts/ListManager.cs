using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class ListManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject listItemPrefab;
    [SerializeField]
    private GameObject listItemParent;

    private Dictionary<string, RoomInfo> cachedRooms;
    private Dictionary<string, GameObject> roomListItems;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();

        cachedRooms = new Dictionary<string, RoomInfo>();
        roomListItems = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(GameObject listItem in roomListItems.Values)
        {
            Destroy(listItem);
        }
        roomListItems.Clear();

        //Updating cached rooms dictionary
        foreach(RoomInfo room in roomList)
        {
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if (cachedRooms.ContainsKey(room.Name))
                {
                    cachedRooms.Remove(room.Name);
                }
            }
            else //ADD TO OR UPDATE DICTIONARY
            {
                if (cachedRooms.ContainsKey(room.Name)) //If exists, update!
                {
                    cachedRooms[room.Name] = room;
                }
                else //If doesnt exist, add!
                {
                    cachedRooms.Add(room.Name, room);
                }
            }
        }

        foreach (RoomInfo room in cachedRooms.Values)
        {
            GameObject listItem = Instantiate(listItemPrefab, listItemParent.transform);
            listItem.transform.localScale = Vector3.one;

            listItem.transform.Find("RoomName").GetComponent<TMP_Text>().text = room.Name;
            listItem.transform.Find("NumOfPlayers").GetComponent<TMP_Text>().text = "Players: " + room.PlayerCount + "/" + room.MaxPlayers;
            listItem.transform.Find("JoinBtn").GetComponent<Button>().onClick.AddListener(() => OnJoinButtonClick(room.Name));

            roomListItems.Add(room.Name, listItem);
        }
    }

    private void OnJoinButtonClick(string roomName)
    {
        if (PhotonNetwork.InLobby)
            PhotonNetwork.LeaveLobby();

        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            //PhotonNetwork.LoadLevel("WaitingScene");
            SceneManager.LoadScene("WaitingScene");
        }
        else
        {
            PhotonNetwork.LoadLevel("MainGame");
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + " - " + message);
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("RoomManagementScene");
    }
}
