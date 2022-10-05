using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallHandler : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Rigidbody2D pivot;
    Vector2 startPos;

    private void Start() {
        startPos = pivot.position;
    }

    public void RespawnBallRoutine()
    {
        StartCoroutine(RespawnBall());
    }

    IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(2f);
        GameObject ballInstance = Instantiate(ball, startPos, Quaternion.identity);
        SpringJoint2D s = ballInstance.GetComponent<SpringJoint2D>();
        s.connectedBody = pivot;
    }

}
