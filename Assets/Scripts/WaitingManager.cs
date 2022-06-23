using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class WaitingManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text message;

    // Start is called before the first frame update
    void Start()
    {
        message.text = "Player " + PhotonNetwork.NickName + " joined room " + PhotonNetwork.CurrentRoom.Name + "\nWaiting for players...";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if(PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel("MainGame");
        }
    }
}
