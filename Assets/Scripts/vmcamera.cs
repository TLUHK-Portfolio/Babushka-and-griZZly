using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class vmcamera : MonoBehaviour {
    private CinemachineVirtualCamera cam;
    private GameObject green_ball;
    private GameObject red_ball;
    private Rigidbody2D greenRb;
    private Rigidbody2D redRb;
    public float thrust = 5;
    public float waitingTime = 10f;
    public TMP_Text tekst;

    
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
        //greenRb.isKinematic = true;
        
        red_ball = GameObject.FindWithTag("red_ball");
        redRb = red_ball.GetComponent<Rigidbody2D>();
        redRb.isKinematic = true;
        //StartCoroutine(moveBalls());
        
        // algne pos
        targetPosition = new Vector3(greenRb.position.x, greenRb.position.y, -25); 

        
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
        yield return new WaitForSeconds(1f);
        Quaternion Rotation = Quaternion.Euler( 0, 0, -30f);
        tekst.text = "Mutikese kord";
        cam.Follow = green_ball.transform;
        //greenRb.gravityScale = 1;
        //greenRb.isKinematic = false;
        //greenRb.AddForce( Rotation * Vector2.up * thrust, ForceMode2D.Impulse);
        //green_ball.GetComponent<BallControl>().canRotate = true;

        
        yield return new WaitForSeconds(waitingTime);
        tekst.text = "GriZZly kord";
        Rotation = Quaternion.Euler( 0, 0, 45f);
        cam.Follow = red_ball.transform;
        redRb.isKinematic = false;

        redRb.AddForce(Rotation * Vector2.up * thrust, ForceMode2D.Impulse);
        red_ball.GetComponent<BallControl>().canRotate = true;
 
    }

}