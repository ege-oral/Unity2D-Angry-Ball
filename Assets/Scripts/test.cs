using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    private Touch touch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                print(touch.phase.ToString());
                timeTouchEnded = Time.time;
            }
            else if(Time.time - timeTouchEnded > displayTime)
            {
                print(touch.phase.ToString());
                timeTouchEnded = Time.time;
            }
        }
        else if(Time.time - timeTouchEnded > displayTime)
        {
            print(" ");
        }
    }
}
