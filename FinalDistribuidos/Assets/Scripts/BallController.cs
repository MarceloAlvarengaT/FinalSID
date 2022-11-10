using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    bool setSpeed;
    [SerializeField] float speedUp;
    float xSpeed;
    float YSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if(GameController.instance.inPlay == true)
        {
            if (!setSpeed)
            {
                setSpeed = true;

                xSpeed = Random.Range(1f, 2f) * (Random.Range(0, 2) * 2 - 1);
                YSpeed = Random.Range(1f, 2f) * (Random.Range(0, 2) * 2 - 1);
            }
            MoveBall();
        }
    }

    private void MoveBall()
    {
        rb.velocity = new Vector2(xSpeed, YSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Wall")
        {
            xSpeed = xSpeed * -1;
        }
        if(other.transform.tag == "Paddle")
        {
            YSpeed = YSpeed * -1;

            if(YSpeed > 0)
            {
                YSpeed += speedUp;
            }
            else
            {
                YSpeed -= speedUp;
            }
            if (xSpeed > 0)
            {
                xSpeed += speedUp;
            }
            else
            {
                xSpeed -= speedUp;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EndOne")
        {
            GameController.instance.scoreOne++;
            if (PhotonNetwork.IsMasterClient)
            {
                GameController.instance.textOne.text = GameController.instance.scoreOne.ToString();
            }
            else
            {
                GameController.instance.textTwo.text = GameController.instance.scoreOne.ToString();
            }
            GameController.instance.inPlay = false;
            setSpeed = false;
            rb.velocity = Vector2.zero;
            this.transform.position = Vector2.zero;
        }
        else if (other.tag == "EndTwo")
        {
            GameController.instance.scoreTwo++;
            if (PhotonNetwork.IsMasterClient)
            {
                GameController.instance.textTwo.text = GameController.instance.scoreOne.ToString();
            }
            else
            {
                GameController.instance.textOne.text = GameController.instance.scoreOne.ToString();
            }
            GameController.instance.inPlay = false;
            setSpeed = false;
            rb.velocity = Vector2.zero;
            this.transform.position = Vector2.zero;
        }
    }
}
