using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D currentBallRigidbody;
    SpringJoint2D currentBallSprintJoint;
    Touch touch;
    BallHandler ballHandler;

    private Camera mainCamera;
    private bool isDragging = false;

    [SerializeField] float maxRightDragDistance = 1f;
    [SerializeField] float minBottomDragDistance = -3.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentBallRigidbody = GetComponent<Rigidbody2D>();
        currentBallSprintJoint = GetComponent<SpringJoint2D>();
        ballHandler = FindObjectOfType<BallHandler>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && !isDragging)
        {
            touch = Input.GetTouch(0);
            if(touch.phase != TouchPhase.Ended)
            {
               MoveBall(touch);
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                LaunchBall();
                isDragging = true;
            }
        }
    }

    private void MoveBall(Touch touch)
    {
        currentBallRigidbody.isKinematic = true;
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(touch.position);

        if(worldPosition.x < maxRightDragDistance && worldPosition.y > minBottomDragDistance)
            currentBallRigidbody.position = worldPosition;
        
        else if (worldPosition.x > maxRightDragDistance && worldPosition.y > minBottomDragDistance)
            currentBallRigidbody.position = new Vector2(maxRightDragDistance, worldPosition.y);

        else if(worldPosition.y < minBottomDragDistance && worldPosition.x < maxRightDragDistance)
            currentBallRigidbody.position = new Vector2(worldPosition.x, minBottomDragDistance);
        
        else
            currentBallRigidbody.position = new Vector2(maxRightDragDistance, minBottomDragDistance);
        
    }

    private void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false;
        ballHandler.RespawnBallRoutine();
        Invoke(nameof(DestroyBallAfterLaunch), 2.5f);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {    
        if(touch.phase == TouchPhase.Ended && other.gameObject.tag == "Pivot Trigger")
        {
            currentBallSprintJoint.enabled = false;
        }
    }

    // If the ball still on the pivot and not exit from the trigger zone.
    private void OnTriggerStay2D(Collider2D other) {
        if(touch.phase == TouchPhase.Ended && other.gameObject.tag == "Pivot Trigger" && isDragging)
            currentBallSprintJoint.enabled = false;
    }

    private void DestroyBallAfterLaunch()
    {
        Destroy(gameObject);
    }
}
