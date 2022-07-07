using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DamageController : MonoBehaviour
{
    [SerializeField]
    private float startingHealth;

    [SerializeField]
    private Image healthBar;

    private float Health;
    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        Health = startingHealth;
        healthBar.fillAmount = Health / startingHealth;

        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //Empty as my brain
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.fillAmount = Health / startingHealth;

        if (Health <= 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {
        if(view.IsMine)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
