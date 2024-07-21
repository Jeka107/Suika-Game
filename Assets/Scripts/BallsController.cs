using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] public List<GameObject> balls = new List<GameObject>();
    [SerializeField] public Transform ballsParent;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private float waitBeforeNextBallSpawn;
    [SerializeField] private int[] numbers = {128,256,512,1024,2048}; 

    public static BallsController instance;
    private GameObject currentBall;
    [HideInInspector] public Bounds bounds;
    public int result;
    private bool canThrow = true;

    private void Awake()
    {
        instance = this;
        RandomResult();
        StartCoroutine(SpawnBall(0f));

        InputManager.onSpacePressed += ThrowBall;
    }
    private void OnDestroy()
    {
        InputManager.onSpacePressed -= ThrowBall;
    }
    private void ThrowBall()
    {
        if (canThrow)
        {
            currentBall.transform.SetParent(ballsParent, true);
            currentBall.GetComponent<BallInfo>().GravityOn();

            canThrow = false;
            StartCoroutine(SpawnBall(waitBeforeNextBallSpawn));
        }
    }
    IEnumerator SpawnBall(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        int x = Random.Range(0, 4);
        BallInfo currentInfo;

        currentBall = Instantiate(balls[x], transform.position, Quaternion.identity, playerSprite);
        currentInfo = currentBall.GetComponent<BallInfo>();
        currentBall.transform.localPosition = currentInfo.spawnPoint;

        bounds = currentBall.GetComponent<CircleCollider2D>().bounds;
        player.ChangeBoundry();
        canThrow = true;
    }
    public void RandomResult()
    {
        int random=Random.Range(0, numbers.Length-1);

        result= numbers[random];
    }
}
