using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PaddleController : MonoBehaviour
{
    private PhotonView myPV;
    public string leftKey, rightKey;
    public float speed;

    private void Start()
    {
        myPV = GetComponent<PhotonView>();
        if (myPV.IsMine)
        {
            Camera.main.transform.rotation = transform.rotation;
            myPV.RPC("RPC_SendName", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
        }
    }

    [PunRPC]
    void RPC_SendName(string nameSent)
    {
        GameController.instance.SetTheirName(nameSent);
    }
    // Update is called once per frame
    void Update()
    {
        if (myPV.IsMine)
        {
            PaddleMovement();
        }
    }

    private void PaddleMovement()
    {
        if(Input.GetKey(leftKey) && transform.position.x > -4)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
        }
        if (Input.GetKey(rightKey) && transform.position.x < 4)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
        }
    }
}
