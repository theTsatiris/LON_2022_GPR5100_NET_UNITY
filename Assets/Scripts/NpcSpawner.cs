using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject NPC;

    [SerializeField]
    private Transform[] spawningPoints;

    [SerializeField]
    private int maximumNPCs = 5;

    [SerializeField]
    private float spawningInterval = 5.0f;

    private int currentNPCcount;
    private float timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        currentNPCcount = 0;
        timeBetweenSpawns = spawningInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if((PhotonNetwork.IsMasterClient) && (PhotonNetwork.CurrentRoom.PlayerCount > 1))
        {
            if(timeBetweenSpawns <= 0.0)
            {
                if(currentNPCcount < maximumNPCs)
                {
                    int randomIndex = Random.Range(0, spawningPoints.Length);
                    Vector3 randomSpawnPos = spawningPoints[randomIndex].position;
                    PhotonNetwork.Instantiate(NPC.name, randomSpawnPos, Quaternion.identity);
                    
                    currentNPCcount++;
                    timeBetweenSpawns = spawningInterval;
                }
            }
            else
            {
                timeBetweenSpawns -= Time.deltaTime;
            }
        }
    }
}
