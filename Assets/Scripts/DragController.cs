using UnityEngine;
using TMPro;

public class DragController : MonoBehaviour
{
    [Header("Drag Settings")]
    public LineRenderer line;
    public Rigidbody2D rb;
    public float dragLimit = 3f;
    public float forceToAdd = 10f;

    [Header("UI")]
    public TMP_Text dragCounterText;

    [Header("Sprites")]
    public SpriteRenderer ballRenderer;
    public Sprite firstBallSprite;       
    public Sprite newBallSprite;        
    public Sprite secondBallSprite;     

    public SpriteRenderer backgroundRenderer;
    public Sprite firstBackgroundSprite; 
    public Sprite newBackgroundSprite;  
    public Sprite secondBackgroundSprite;

    [Header("Lives & Hearts")]
    public int lives = 3;              
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    [Header("Music")]
    public AudioSource audioSource;  
    public AudioClip firstMusic;     
    public AudioClip newMusic;        
    public AudioClip secondMusic;     

    private Camera cam;
    private bool isDragging;
    private int stage = 0; 
    private int dragCount = 0;

    Vector3 MousePosition
    {
        get
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            return pos;
        }
    }

    void Start()
    {
        cam = Camera.main;

 
        if (ballRenderer != null && firstBallSprite != null)
            ballRenderer.sprite = firstBallSprite;

        if (backgroundRenderer != null && firstBackgroundSprite != null)
            backgroundRenderer.sprite = firstBackgroundSprite;


        if (line != null)
        {
            line.positionCount = 2;
            line.SetPosition(0, Vector2.zero);
            line.SetPosition(1, Vector2.zero);
            line.enabled = false;
        }


        if (audioSource != null && firstMusic != null)
        {
            audioSource.clip = firstMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        UpdateCounter();
        UpdateHeartsUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
            DragStart();

        if (isDragging)
            Drag();

        if (Input.GetMouseButtonUp(0) && isDragging)
            DragEnd();
    }

    void DragStart()
    {
        isDragging = true;
        if (line != null) line.enabled = true;
        if (line != null) line.SetPosition(0, MousePosition);
    }

    void Drag()
    {
        if (line == null) return;

        Vector3 startPos = line.GetPosition(0);
        Vector3 currentPos = MousePosition;
        Vector3 distance = currentPos - startPos;

        if (distance.magnitude <= dragLimit)
            line.SetPosition(1, currentPos);
        else
            line.SetPosition(1, startPos + distance.normalized * dragLimit);
    }

    void DragEnd()
    {
        isDragging = false;
        if (line != null) line.enabled = false;

        Vector3 startPos = line.GetPosition(0);
        Vector3 currentPos = line.GetPosition(1);
        Vector3 distance = currentPos - startPos;

        if (rb != null)
            rb.AddForce(distance * forceToAdd, ForceMode2D.Impulse);

        dragCount++;
        UpdateCounter();
    }

    void UpdateCounter()
    {
        if (dragCounterText != null)
            dragCounterText.text = "" + dragCount;


        if (stage == 0 && dragCount >= 20)
        {
            stage = 1;
            ChangeStage(newBallSprite, newBackgroundSprite, newMusic);
        }

        else if (stage == 1 && dragCount >= 40)
        {
            stage = 2;
            ChangeStage(secondBallSprite, secondBackgroundSprite, secondMusic);
        }
    }

    void ChangeStage(Sprite ballSprite, Sprite backgroundSprite, AudioClip musicClip)
    {
        if (ballRenderer != null && ballSprite != null)
            ballRenderer.sprite = ballSprite;

        if (backgroundRenderer != null && backgroundSprite != null)
            backgroundRenderer.sprite = backgroundSprite;

        if (audioSource != null && musicClip != null)
        {
            audioSource.Stop();
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void UpdateHeartsUI()
    {
        if (heart1 != null) heart1.SetActive(lives >= 1);
        if (heart2 != null) heart2.SetActive(lives >= 2);
        if (heart3 != null) heart3.SetActive(lives >= 3);
    }


void OnTriggerEnter2D(Collider2D other)
{

    Debug.Log("Collision with: " + other.name);
Debug.Log("Triggered with: " + other.name);
    if (other.CompareTag("DeathZone"))
    {
        lives--;
        UpdateHeartsUI();
        Debug.Log("Lives left: " + lives);

        if (lives <= 0)
        {
            Debug.Log("Game Over!");

        }
        else
        {

            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            transform.position = Vector3.zero;
        }
    }
}
void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Collided with: " + collision.gameObject.name);
}
}
