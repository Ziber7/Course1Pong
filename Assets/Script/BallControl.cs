using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    //Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    //Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    //Titik asal lintasan bola 
    private Vector2 trajectoryOrigin;

    void ResetBall()
    {
        //Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        //Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //Tentukan nilai komponen y dari gaya dorong antara yInitialForce dan yInitialForce
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float xRandomInitialForce = Random.Range(-xInitialForce, xInitialForce);

        //Tentukan nilai acak antara 0 (inklusif) dan 2 (ekslusif)
        float randomDirection = Random.Range(0, 2);
        
        //Jika nilainya dibawah 1 , bola bergerak ke kiri
        //Jika tidak, bola bergerak ke kanan
        if (randomDirection < 1.0f)
        {
            //gunakan gaya untuk menggerakan bola
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        } else {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));

        }
    }

    void RestartGame()
    {
        //Kembalikan bola pada posisi semula
        ResetBall();

        //Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall" , 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        //Mulai Game
        RestartGame();

        trajectoryOrigin = transform.position;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
