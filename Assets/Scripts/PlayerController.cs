using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float TranslationIncrement = 4f;
    public float JumpIncrement = 6f;
    public float TapIncrement = .2f;
    public bool facingRight = true;
    public AudioClip jumpSound;
    public AudioClip tapSound;
    public GameObject unicornHorn;
    public bool win = false;
    public GameObject[] spawnPoints;

    private AudioSource source;
    private bool jumping = false;

    // Use this for initialization
    void Start () {

    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        Debug.Log(spawnPoints.Length);
        unicornHorn.transform.position = spawnPoints[Random.Range(0,spawnPoints.Length-1)].transform.position;

        // Disable unicorn horn particle effect
        ParticleSystem ps = unicornHorn.GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = false;



}

    // Update is called once per frame
    void FixedUpdate () {

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y == 0f)
        {
            jumping = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Move player to the right
            transform.root.position += Vector3.right * Time.deltaTime * TranslationIncrement;
            facingRight = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.GetChild(0).position = transform.root.position + Vector3.right * 0.3f;
            transform.GetChild(1).position = transform.root.position + Vector3.right * 0.3f + Vector3.up * 0.3f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Move player to the left
            transform.root.position += Vector3.left * Time.deltaTime * TranslationIncrement;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
            transform.GetChild(0).position = transform.root.position + Vector3.left * 0.3f;
            transform.GetChild(1).position = transform.root.position + Vector3.left * 0.3f + Vector3.up * 0.3f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumping == false)
            {
                source.PlayOneShot(jumpSound, 0.6f);
            }

            transform.root.position += Vector3.up * Time.smoothDeltaTime * JumpIncrement;
            jumping = true;
        }

        if (win == true)
        {
            unicornHorn.transform.position = transform.GetChild(1).position;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Tap") && (Input.GetKey(KeyCode.Z)))
        {
            Debug.Log("Trigger Stay");
            if (facingRight){
                collision.gameObject.transform.root.position += Vector3.right * TapIncrement;
            }
            else 
            {
                collision.gameObject.transform.root.position += Vector3.left * TapIncrement;
            }

            source.PlayOneShot(tapSound, 2f);

        }

        if (collision.gameObject.CompareTag("LostToy") && (Input.GetKey(KeyCode.X)))
        {
            win = true;
            StartCoroutine(Win());
        }

    }

    IEnumerator Win()
    {
        // Enable unicorn horn particle effect
        ParticleSystem ps = unicornHorn.GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("CongratulationsScreen", LoadSceneMode.Single);
    }

}
