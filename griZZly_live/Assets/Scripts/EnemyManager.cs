using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameObject AmmoPrefab;
    public float force;
    public GameObject LaunchPosition;
    public Slider HealthBar;
    public float damageFlashTime = .45f;
    private Color origColor;
    private Animator animator;
    private bool onShootingAction;
    Rigidbody2D ammo;
    private PolygonCollider2D col;
    private AudioSource source;
    GameState currentGameState;
    private bool grizzlyThrowing;
    // hoia nulli peal kui tahad normaalset mängu, vastasel juhul saab karu ühe hitiga kotti
    public int debugDamage = 100000;

    public void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        animator = gameObject.GetComponent<Animator>();
        col = gameObject.GetComponent<PolygonCollider2D>();
        grizzlyThrowing = false;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        origColor = gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color;
    }

    private void CreateAmmo()
    {
        if (!GameObject.FindWithTag("Stone"))
        {
            /*GameObject kiviPossa = GameObject.Find("KiviPos");
            ammo = Instantiate(AmmoPrefab, kiviPossa.transform.TransformPoint(Vector3.zero), Quaternion.identity).GetComponent<Rigidbody2D>();
            GameObject.FindWithTag("Stone").transform.position = GameObject.FindWithTag("KiviPos").transform.TransformPoint(Vector3.zero);
            ammo.isKinematic = true;
            Debug.Log(kiviPossa);
            Debug.Log(ammo);*/
            ammo = Instantiate(AmmoPrefab).GetComponent<Rigidbody2D>();
            LaunchPosition = GameObject.Find("KiviPos");
            ammo.transform.position = LaunchPosition.transform.position;
            ammo.isKinematic = true;
        }
    }

    void Shoot() {
        GameObject stone = GameObject.FindWithTag("Stone");
        if (stone) {
            ammo =  stone.GetComponent<Rigidbody2D>();
            onShootingAction = false;
            animator.SetBool("viska", false);
            float ai = Random.Range(.5f, 1.5f);
            if (SceneManager.GetActiveScene().buildIndex == 2) {
                ai = Random.Range(.75f, 1.25f);
            }

            Quaternion Rotation = Quaternion.Euler(0, 0, 45f * ai);
            ammo.isKinematic = false;
            Vector2 f = Rotation * Vector2.up * force;
            ammo.AddForce(f, ForceMode2D.Impulse);
            ammo.GetComponent<Ammo>().Release();
            StartCoroutine(EnableCollider());
        }
    }

    public void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.EnemyTurn)
        {
            CreateAmmo();
            if (col)
            {
                col.enabled = false;
            }

            grizzlyThrowing = true;
        }
        else if (state == GameState.FallowAmmo2)
        {
            onShootingAction = true;
            if (animator)
            {
                animator.SetBool("viska", true);
                if (source)
                    source.Play();
            }

            grizzlyThrowing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(gameObject.name + " collided with " + col.collider.name);

        if (!grizzlyThrowing)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[col.contactCount];
            col.GetContacts(contacts);
            float totalImpulse = 0;
            foreach (ContactPoint2D contact in contacts)
            {
                totalImpulse += contact.normalImpulse * .025f + debugDamage;
            }

            IndicateDamage();

            HealthBar.value -= totalImpulse;
            if (HealthBar.value <= 0) {
                StartCoroutine(waitForIt());
            }
        }
        
    }

    private void IndicateDamage()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<MeshRenderer>())
            {
                child.GetComponent<MeshRenderer>().material.color = Color.red;
            } else if (child.GetComponent<SpriteRenderer>()) {
                child.GetComponent<SpriteRenderer>().material.color = Color.red;
            }
        }

        Invoke("ResetMesh", damageFlashTime);
    }

    private void ResetMesh()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<MeshRenderer>())
            {
                child.GetComponent<MeshRenderer>().material.color = origColor;
            } else if (child.GetComponent<SpriteRenderer>()) {
                child.GetComponent<SpriteRenderer>().material.color = origColor;
            }
        }
    }

    public void Update()
    {
        if (onShootingAction && ammo && ammo.isKinematic)
        {
            ammo.transform.position = LaunchPosition.transform.position;
        }
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(.5f);
        col.enabled = true;
    }
    
    IEnumerator waitForIt()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.UpdateGameState(GameState.Win);
    }
    
    
}