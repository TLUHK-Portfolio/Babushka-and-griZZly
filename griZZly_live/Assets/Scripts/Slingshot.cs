using Cinemachine;
using UnityEngine;

public class Slingshot : MonoBehaviour {
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public Vector3 currentPosition;
    public float maxLength;
    public float bottomBoundary;
    public GameObject birdPrefab;
    public GameObject player;
    public float playerOffset;
    public float birdPositionOffset;
    public float force;

    private GameObject cam;
    bool isMouseDown;
    Rigidbody2D bird;
    Collider2D birdCollider;

    void Start() {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        CreateBird();
    }

    void CreateBird() {
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;
        bird.isKinematic = true;
        ResetStrips();
        cam = GameObject.FindWithTag("MainCamera");
        if (cam) {
            cam.GetComponent<CinemachineVirtualCamera>().Follow = bird.transform;
        }
    }

    void Update() {
        if (isMouseDown) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition
                                                                       - center.position, maxLength);

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if (birdCollider) {
                birdCollider.enabled = true;
            }
        }
        else {
            ResetStrips();
        }
    }

    private void OnMouseDown() {
        isMouseDown = true;
    }

    private void OnMouseUp() {
        isMouseDown = false;
        Shoot();
        currentPosition = idlePosition.position;
    }

    void Shoot() {
        bird.isKinematic = false;
        Vector3 birdForce = (currentPosition - center.position) * force * -1;
        bird.velocity = birdForce;
        bird.GetComponent<Ammo>().Release();
        bird = null;
        birdCollider = null;
        Invoke("CreateBird", 2);
    }

    void ResetStrips() {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position) {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        var position1 = player.transform.position;
        position1 = new Vector3(position.x + playerOffset, position1.y, position1.z);
        player.transform.position = position1;
        if (!bird) return;
        Vector3 dir = position - center.position;
        bird.transform.position = position + dir.normalized * birdPositionOffset;
        bird.transform.right = -dir.normalized;
    }

    Vector3 ClampBoundary(Vector3 vector) {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
}