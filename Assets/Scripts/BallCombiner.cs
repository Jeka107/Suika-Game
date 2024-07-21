using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCombiner : MonoBehaviour
{
    public delegate void OnCombinedBalls(int points);
    public static event OnCombinedBalls onCombinedBalls;

    [SerializeField] private GameObject particleEffect;
    private BallInfo ballInfo;
    
    private void Awake()
    {
        ballInfo = GetComponent<BallInfo>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int defaultLayer = LayerMask.NameToLayer("Default");
        gameObject.layer = defaultLayer;

        if (collision.gameObject.tag=="Ball")
        {
            BallInfo collisionInfo=collision.gameObject.GetComponent<BallInfo>();
            if(collisionInfo!=null)
            {
                if(ballInfo.ballIndex==collisionInfo.ballIndex)
                {
                    int thisId=gameObject.GetInstanceID();
                    int otherId=collision.transform.GetInstanceID();

                    if (thisId>otherId)
                    {
                        Vector3 middlePosition = (transform.position + collision.transform.position) / 2;
                        
                        if (ballInfo.ballIndex==BallsController.instance.balls.Count-1)//last ball - Destroy
                        {
                            Instantiate(particleEffect, middlePosition, Quaternion.identity);
                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }
                        else
                        {
                            GameObject newBall=Instantiate(SpawnCombinedBall(ballInfo.ballIndex), middlePosition, Quaternion.identity, BallsController.instance.ballsParent);
                            Instantiate(particleEffect, middlePosition, Quaternion.identity, newBall.transform);

                            BallInfo newBallInfo = newBall.GetComponent<BallInfo>();
                            newBallInfo.GravityOn();

                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }
                        onCombinedBalls?.Invoke(ballInfo.points);
                    }
                }
            }
        }
    }
    private GameObject SpawnCombinedBall(int index)
    {
        GameObject newBall = BallsController.instance.balls[index+1];
        return newBall;
    }
}

