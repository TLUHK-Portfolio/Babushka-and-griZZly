using Cinemachine;
using UnityEngine;

public class Slingshot : MonoBehaviour {
    public CinemachineVirtualCamera cam;

    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;
    public Vector3 playerOffset;
    public float maxLength;
    public float bottomBoundary;

    public GameObject ammoPrefab;
    public float ammoPositionOffset;
    public float force;
    public GameObject player;


    bool isMouseDown;
    Rigidbody2D ammo;
    Collider2D ammoCollider;


    void Start() {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        //cam = GetComponent<CinemachineVirtualCamera>();

        CreateAmmo();
    }

    void CreateAmmo() {
        ammo = Instantiate(ammoPrefab).GetComponent<Rigidbody2D>();
        ammoCollider = ammo.GetComponent<Collider2D>();
        ammoCollider.enabled = false;
        ammo.isKinematic = true;
        ResetStrips();
    }

    void Update() {
        if (isMouseDown) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            currentPosition = ClampBoundary(currentPosition);
            if (player) {
                //player.transform.position = new Vector3(currentPosition.x + playerOffset.x, player.transform.position.y,
                //    player.transform.position.z);
            }

            SetStrips(currentPosition);
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
        //cam.Follow = ammo.transform;
        ammo.isKinematic = false;
        Vector3 ammoForce = (currentPosition - center.position) * force * -1;
        ammo.velocity = ammoForce;

        //ammo.GetComponent<BallControl>().canRotate = true;
        /*if (ammoCollider) {
            ammoCollider.enabled = true;
        }*/
        ammo = null;
        ammoCollider = null;

        Destroy(ammo, 3f);
        Invoke("CreateAmmo", 2);
    }

    void ResetStrips() {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position) {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (ammo) {
            Vector3 dir = position - center.position;
            dir.z = 0;
            ammo.transform.position = position + dir.normalized * ammoPositionOffset;
            //ammo.transform.position = new Vector3(ammo.transform.position.x, ammo.transform.position.x, 0);
            ammo.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector) {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
}