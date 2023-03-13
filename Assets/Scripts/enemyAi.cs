using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : MonoBehaviour {
    public Vector3 endPos;
    public Vector3 startPos;
    public float speed;
    public float distance;


    void Start() {
        GameObject redBall = GameObject.FindWithTag("red_ball");
        startPos = transform.position;
        endPos = redBall.transform.position;
    }
    
    private void Update() {
        float dist = Vector3.Distance(startPos, endPos);
        distance = dist;

        //Here we assign the rotation
        Vector3 relativePos = endPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
        var tempRot = transform.eulerAngles;

        //This line of code is so that we can point towards the target position, while also pointing to the                            firing angle
        //tempRot.x = transform.eulerAngles.x - getAngle();
        Debug.Log(tempRot);
        //transform.eulerAngles = tempRot;
    }

    float getAngle() {
        float gravity = 9.81f;
        float angle = 0.5f * (Mathf.Asin((gravity * distance) / (speed * speed)));
        return angle * Mathf.Rad2Deg;
    }
}