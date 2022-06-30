using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private GameObject Projectile;

    [SerializeField]
    private float shootingRate;

    [SerializeField]
    private Transform SpawningPoint;

    private float timeFromLastShot;
    private float maxTimeBetweenShots;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        maxTimeBetweenShots = 1.0f / shootingRate;

        timeFromLastShot = maxTimeBetweenShots;

        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeFromLastShot < maxTimeBetweenShots)
        {
            timeFromLastShot += Time.deltaTime;
        }

        if(view.IsMine)
        {
            if(Input.GetButton("Fire1"))
            {
                if(timeFromLastShot >= maxTimeBetweenShots)
                {
                    GameObject projectile = PhotonNetwork.Instantiate(Projectile.name, SpawningPoint.position, Quaternion.identity);

                    ProjectileController pc = projectile.GetComponent<ProjectileController>();
                    pc.SetDirection(SpawningPoint.forward);

                    timeFromLastShot = 0.0f;
                }
            }
        }
    }
}
