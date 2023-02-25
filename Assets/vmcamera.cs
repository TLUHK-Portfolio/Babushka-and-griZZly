using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class vmcamera : MonoBehaviour {
    private CinemachineVirtualCamera cam;
    private GameObject green_ball;
    private GameObject red_ball;
    private Rigidbody2D greenRb;
    private Rigidbody2D redRb;
    public float thrust = 5;
    public float waitingTime = 10f;

    private bool isDone = false;
    
    // Lerping stuff
    // Lerping Smooth Speed
    private float smoothSpeed = .025f;
    // The current target position 
    private Vector3 targetPosition;
    // The current intermediary Lerp Position
    private Vector3 smoothPosition;
    // Start is called before the first frame update
    void Start() {
        
        cam = GetComponent<CinemachineVirtualCamera>();
        
        green_ball = GameObject.FindWithTag("green_ball");
        greenRb = green_ball.GetComponent<Rigidbody2D>();
        
        red_ball = GameObject.FindWithTag("red_ball");
        redRb = red_ball.GetComponent<Rigidbody2D>();
        //StartCoroutine(moveBalls());
        
        // algne pos
        targetPosition = new Vector3(redRb.position.x, redRb.position.y, -10); 

    }

    // Late Update ////////////////////////////////////////////////////////////
    // Lerp toward target position and rotation //////////////////////////////
    void LateUpdate()
    {
        // Lerp toward target position at all times.
        smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothPosition;
 
        // Lerp toward target rotation at all times.
        //smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
        //transform.rotation = smoothRotation;
    }

    IEnumerator moveBalls() {
        Debug.Log("rohelise kord");
        cam.Follow = green_ball.transform;
        greenRb.AddForce(Vector2.right * thrust, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(waitingTime);
        Debug.Log("punase kord");
       //if (!redRb) yield break;
        cam.Follow = red_ball.transform;
        redRb.AddForce(Vector2.left * thrust, ForceMode2D.Impulse);
        isDone = true;
    }

}