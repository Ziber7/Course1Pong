using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Pemain 1 
    public PlayerControl player1;
    private Rigidbody2D player1Rigidbody;

    //Pemain 2
    public PlayerControl player2; 
    private Rigidbody2D player2Rigidbody;

    //Bola
    public BallControl ball; 
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;

    //Skor maksimal
    public int maxScore;

    //Apakah debug window ditampilkan?
    private bool isDebugWindowShown = false;

    //objek gambar prediksi lintasan
    public Trajectory trajectory;


    //Inisialisasi rididbody dan colider
    private void Start() 
    {
            player1Rigidbody = player1.GetComponent<Rigidbody2D>();
            player2Rigidbody = player2.GetComponent<Rigidbody2D>();
            ballRigidbody = ball.GetComponent<Rigidbody2D>();
            ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    //Gui
    void OnGUI()
    {
        //tampilkan skor pemain 1 kiri pemain 2 kanan
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        //Tombol restart untuk memulai dari awal
        if (GUI.Button(new Rect(Screen.width / 2 - 60,35  , 120, 53), "RESTART"))
        {
            //Ketika tombol restart ditekan, reset skor kedua pemain
            player1.ResetScore();
            player2.ResetScore();

            //restart game
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        //Jika pemain 1 menang 
        if (player1.score == maxScore)
        {
            //tampilkan teks
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        //Jika isDebugWindowShown == true , tampilkan text area debug window
        if (isDebugWindowShown)
        {
            //Simpan nilai warna lama GUI
            Color oldColor = GUI.backgroundColor;

            //Beri warna baru
            GUI.backgroundColor = Color.red;

            // Simpan variabel-variabel fisika yang akan ditampilkan. 
            float ballMass = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity; 
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            // Tentukan debug text-nya
            string debugText = 
                "Ball mass = " + ballMass+ "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";
            
            //Tampilkan debug window 
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height - 200, 400 ,110), debugText, guiStyle);

            //Kembalikan warna lama GUI
            GUI.backgroundColor = oldColor;

            
        }
        
        //Toggle nilai debug window keika pemain mengklik tombol ini.
        if (GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120,53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }

    }



    
    // Update is called once per frame
    void Update()
    {
        
    }
}
