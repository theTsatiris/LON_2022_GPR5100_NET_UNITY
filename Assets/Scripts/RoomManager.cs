using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField NickName;
    [SerializeField]
    private TMP_InputField NewRoom;
    [SerializeField]
    private TMP_InputField MaxPlayers;
    [SerializeField]
    private TMP_InputField JoinRoom;

    private byte maxPlayers;

    private static byte MAX_PLAYERS = 10;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ********** ONCLICKS **********
    public void CreateRoom()
    {
        if(InputIsValid(NickName) && InputIsValid(NewRoom) && MaxPlayersIsValid())
        {
            PhotonNetwork.NickName = NickName.text;
            RoomOptions rOptions = new RoomOptions();
            rOptions.IsOpen = true;
            rOptions.IsVisible = true;
            rOptions.MaxPlayers = maxPlayers;

            PhotonNetwork.CreateRoom(NewRoom.text, rOptions);
        }
        else
        {
            Debug.Log("INVALID INPUT!!!");
        }
    }

    public void JoinExistingRoom()
    {
        if(InputIsValid(NickName) && InputIsValid(JoinRoom))
        {
            PhotonNetwork.NickName = NickName.text;
            PhotonNetwork.JoinRoom(JoinRoom.text);
        }
        else
        {
            Debug.Log("INVALID INPUT!!!");
        }
    }

    public void JoinRandomRoom()
    {
        if (InputIsValid(NickName))
        {
            PhotonNetwork.NickName = NickName.text;
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("INVALID INPUT!!!");
        }
    }

    public void SelectRoom()
    {
        if (InputIsValid(NickName))
        {
            PhotonNetwork.NickName = NickName.text;
            SceneManager.LoadScene("RoomListScene");
        }
        else
        {
            Debug.Log("INVALID INPUT!!!");
        }
    }

    // ********** CALLBACKS **********

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

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + " - " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + " - " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("ERROR CODE " + returnCode + " - " + message);

        //Create a room with random max players and random name
        RoomOptions rOptions = new RoomOptions();
        rOptions.IsOpen = true;
        rOptions.IsVisible = true;
        rOptions.MaxPlayers = (byte)Random.Range(2, MAX_PLAYERS);
        string roomName = "Room" + Random.Range(0, 100000);

        PhotonNetwork.CreateRoom(roomName, rOptions);
    }

    // ********** HELPERS **********

    private bool InputIsValid(TMP_InputField inputField)
    {
        string input = inputField.text;
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            return false;
        else
            return true;
    }

    private bool MaxPlayersIsValid()
    {
        string maxPlayerString = MaxPlayers.text;
        if(byte.TryParse(maxPlayerString, out maxPlayers))
        {
            if (maxPlayers > MAX_PLAYERS)
                maxPlayers = MAX_PLAYERS;
            if (maxPlayers < 2)
                maxPlayers = 2;

            return true;
        }
        else
        {
            return false;
        }
    }
}
