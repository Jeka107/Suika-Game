using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallInfo : MonoBehaviour
{
    [SerializeField] public int ballIndex;
    [SerializeField] public int points;
    [SerializeField] private float ballMass;
    [SerializeField] public Vector3 spawnPoint;

    private Rigidbody2D rb;
   
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();

        rb.mass = ballMass;
    }
    public void GravityOn()
    {
        rb.gravityScale = 1;
    }
}
