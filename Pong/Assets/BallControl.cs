using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;
 
    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
 
        // Mulai game
        RestartGame();

        trajectoryOrigin = transform.position;
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;
 
        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //tambahan kode untuk memenuhi tugas
        //menentukan magnitude gaya inisiasi
        float magnitudeInitialForce = Mathf.Sqrt(xInitialForce * xInitialForce + yInitialForce * yInitialForce);

        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        //tambahan kode untuk memenuhi tugas
        //maka nilai gaya komponen x adalah akar dari (magnitude kuadrat - yRandomInitialForce kuadrat)
        float xRandomInitialForce = 
            Mathf.Sqrt(magnitudeInitialForce * magnitudeInitialForce - yRandomInitialForce * yRandomInitialForce);

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        //jika nilai di bawah 1 maka bola bergerak ke kiri.
        //jika tidak, bola bergerak ke kanan.
        if (randomDirection < 1f)
        {
            //gunakan gaya untuk menggerakan bola
            //rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce)); //kode dari tutorial
            rigidBody2D.AddForce(new Vector2(-xRandomInitialForce, yRandomInitialForce));
        }
        else
        {
            //rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce)); //kode dari tutorial
            rigidBody2D.AddForce(new Vector2(xRandomInitialForce, yRandomInitialForce));
        }
    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();
 
        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
