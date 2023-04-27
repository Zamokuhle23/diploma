using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //[Header("Enemy Health and Damage")]

    [Header("Enemy")]
    public NavMeshAgent enemyAgent;

    [Header("Enemy Guarding var")]
    public GameObject[] walkPoints;
    int currentPosition = 0;
    public float enemySpeed;
    float walkingPointRadius = 2;

    [Header("Enemy Situation")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerVisionRadius;
    public bool playerShootingRadius;




    //[Header("Enemy Health and Damage")]

    void Update()
    {

    }


}
