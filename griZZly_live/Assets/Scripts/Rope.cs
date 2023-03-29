using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    public Transform StartPoint;
    public Transform EndPoint;
    public GameObject AmmoPrefab;
    public float force;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.15f;
    private int segmentLength = 20;
    private float lineWidth = 0.025f;
    private bool moveToMouse;
    private Vector3 mousePositionWorld;
    private int indexMousePos;
    [SerializeField]
    private GameObject followAmmo;

    Rigidbody2D ammo;
    Collider2D ammoCollider;
    Vector3 center;
    private bool ammoCreated; 

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < segmentLength; i++) {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    void Update() {
        DrawRope();
        if (GameManager.Instance.State == GameState.PlayerTurn) { // Input.GetMouseButtonDown(0) && 
            if (Input.GetMouseButtonDown(0) && ammo) {
                moveToMouse = true;    
            }
            CreateAmmo();
            center = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }
        if (Input.GetMouseButtonUp(0) && GameManager.Instance.State == GameState.PlayerTurn && ammo) {
            Debug.Log("Shuut");
            moveToMouse = false;
            Shoot();
        }

        Vector3 screenMousePos = Input.mousePosition;
        float xStart = StartPoint.position.x;
        float xEnd = EndPoint.position.x;
        mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(screenMousePos.x, screenMousePos.y, 10));
        //float currX = mousePositionWorld.x;
        float currX = followAmmo.transform.position.x;

        float ratio = (currX - xStart) / (xEnd - xStart);
        if (ratio > 0) {
            indexMousePos = (int)(segmentLength * ratio);
            if (ammo && moveToMouse) {
                ammo.transform.position = mousePositionWorld;
                //followAmmo.transform.position = mousePositionWorld;
            }
        }
    }

    private void CreateAmmo() {
        if (!ammo) {
            ammo = Instantiate(AmmoPrefab).GetComponent<Rigidbody2D>();
            ammoCollider = ammo.GetComponent<Collider2D>();
            ammo.isKinematic = true;
            ammo.position = followAmmo.transform.position;
        }
    }

    void Shoot() {
        ammo.isKinematic = false;
        Vector3 birdForce = (mousePositionWorld - center) * (force * -1);
        ammo.velocity = birdForce;
        ammo.GetComponent<Ammo>().Release();
        ammo = null;
        ammoCollider = null;
    }

    private void FixedUpdate() {
        Simulate();
    }

    private void Simulate() {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1f);

        for (int i = 1; i < segmentLength; i++) {
            RopeSegment firstSegment = ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++) {
            ApplyConstraint();
        }
    }

    private void ApplyConstraint() {
        //Constrant to Mouse
        //RopeSegment firstSegment = ropeSegments[0];
        //firstSegment.posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //ropeSegments[0] = firstSegment;

        //Constrant to First Point 
        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.posNow = StartPoint.position;
        ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        RopeSegment endSegment = ropeSegments[ropeSegments.Count - 1];
        endSegment.posNow = EndPoint.position;
        ropeSegments[ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < segmentLength - 1; i++) {
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen) {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen) {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0) {
                firstSeg.posNow -= changeAmount * 0.5f;
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                ropeSegments[i + 1] = secondSeg;
            }
            else {
                secondSeg.posNow += changeAmount;
                ropeSegments[i + 1] = secondSeg;
            }

            if ( indexMousePos > 0 && indexMousePos < segmentLength - 1 && i == indexMousePos) { //moveToMouse &&
                RopeSegment segment = ropeSegments[i];
                RopeSegment segment2 = ropeSegments[i + 1];
                //segment.posNow = new Vector2(mousePositionWorld.x, mousePositionWorld.y);
                //segment2.posNow = new Vector2(mousePositionWorld.x, mousePositionWorld.y);
                segment.posNow = new Vector2(followAmmo.transform.position.x, followAmmo.transform.position.y);
                segment2.posNow = new Vector2(followAmmo.transform.position.x, followAmmo.transform.position.y);
                ropeSegments[i] = segment;
                ropeSegments[i + 1] = segment2;
            }
        }
    }

    private void DrawRope() {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[segmentLength];
        for (int i = 0; i < segmentLength; i++) {
            ropePositions[i] = ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos) {
            posNow = pos;
            posOld = pos;
        }
    }
}