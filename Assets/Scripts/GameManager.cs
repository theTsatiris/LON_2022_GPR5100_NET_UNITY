using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    private bool ToggleCursorState;

    // Start is called before the first frame update
    void Start()
    {
        ToggleCursorState = true;
        //false = unlocked and visible
        //true = locked and invisible
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("c"))
        {
            ToggleCursorState = !ToggleCursorState;
        }

        if(ToggleCursorState)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    public override void OnLeftRoom()
    {
        //base.OnLeftRoom();
        SceneManager.LoadScene("RoomManagementScene");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //base.OnPlayerLeftRoom(otherPlayer);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            //HANDLE WINNING CELEBRATIONS. MAYBE HAVE A COOKIE...
            PhotonNetwork.LeaveRoom();
        }
    }
}
