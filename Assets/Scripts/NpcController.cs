using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NpcController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private PlayerController[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController nearestPlayer = null;
        float minDistance = float.MaxValue;
        
        foreach(PlayerController player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                nearestPlayer = player;
            }
        }

        if(nearestPlayer != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
            transform.LookAt(nearestPlayer.transform.position);
        }
    }

    [PunRPC]
    public void Die()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            NpcSpawner.currentNPCcount--;
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
