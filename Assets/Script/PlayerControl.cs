using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Tombol untuk bergerak ke atas
    public KeyCode upButton = KeyCode.W;

    //Tombol untuk bergerak ke bawah
    public KeyCode downButton = KeyCode.S;

    //Kecepatan Gerak
    public float speed = 10.0f;

    //Batas atas dan bawah game scene
    public float yBoundary = 9.0f;

    //RigidBody 2d Raket ini 
    private Rigidbody2D rigidBody2D;

    //Skor Pemain
    public int score;

    //Titik tumbukan terakhir dengan bola 
    private ContactPoint2D lastContactPoint;

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    //titik asal lintasan 

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Dapatkan kecepatan raket sekarang
        Vector2 velocity = rigidBody2D.velocity;

        //Jika pemain menekan tombol ke atas, beri kecepatan positif ke komponen Y
        if (Input.GetKey(upButton)) 
        {
            velocity.y = speed;
        }

        //Jika pemian menekan tombol kebawah, beri kecepatan negatif e komponen Y 
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        //Jika pemain tidak menekan tombol apa-apa, kecepatannya nol
        else
        {
            velocity.y = 0.0f;
        }

        //Masukkan kembali kecepatannya ke rigidBody2D
        rigidBody2D.velocity = velocity;

        //dapatkan posisi raket sekarang
        Vector3 position = transform.position;

        //Jika posisi raket melewati batas atas (YBoundary) , kembalikan ke batas atas tersebut
        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }

        //Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        //Masukkan kembali posisinya ke transform
        transform.position = position;


    }

    //ketika tummbukan dengan bola , rekam titik kontaknya
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    //Menaikkan skor sebanyak 1 poin
    public void IncrementScore()
    {
        score++;
    }

    //Mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    //Mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }
}
