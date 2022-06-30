using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private PhotonView view;

    private Vector3 direction;

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if(view.IsMine)
        { 
            if (Vector3.Distance(transform.position, startingPosition) >= 50.0f)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        startingPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.CompareTag("NPC"))
        {
            hit.GetComponent<PhotonView>().RPC("Die", RpcTarget.AllBuffered);
        }

        if(view.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
