using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameTag;

    [SerializeField]
    private GameObject FPSCamera;

    [SerializeField]
    private GameObject UpperBody;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float sensitivity = 1.0f;

    private PhotonView view;

    private float CurrentUpperBodyRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        nameTag.text = view.Owner.NickName;

        if(!view.IsMine)
        {
            FPSCamera.GetComponent<Camera>().enabled = false;
            FPSCamera.GetComponent<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        { 
            if(Input.GetKey("w"))
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }

            float horizontalRotation = Input.GetAxis("Mouse X");

            Vector3 rotationalVector = new Vector3(0.0f, horizontalRotation, 0.0f) * sensitivity;

            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation += rotationalVector;

            transform.rotation = Quaternion.Euler(currentRotation);

            float verticalRotation = Input.GetAxis("Mouse Y") * sensitivity;

            CurrentUpperBodyRotation -= verticalRotation;
            CurrentUpperBodyRotation = Mathf.Clamp(CurrentUpperBodyRotation, -25, 15);

            UpperBody.transform.localEulerAngles = new Vector3(CurrentUpperBodyRotation, 0.0f, 0.0f);
        }
    }
}
