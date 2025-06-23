using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] public float maxMoveRate = 0.3f;
    [SerializeField] public float minMoveRate = 0.01f;
    [SerializeField] public float moveRate;
    [SerializeField] public float maxSegments;
    [SerializeField] AnimationCurve speedCurve;

    public Transform snakeSegments;    
    public Text scoreText;

    [SerializeField]
    public Button resetButton;
    public Button quitButton;
    public Text gameOverText;
    public GameObject grayScreen;
    public AudioSource foodSFX;


    private List<Transform> _segments;
    private float moveTimer;
    private Vector2 _direction;
    private bool isGameOver = false;

    int score = 0;

    private void Start()
    {
        _direction = Vector2Int.right;
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }
    private void Update()
    {
        if (isGameOver) return;

        InputManagent();
    }
    private void FixedUpdate()
    {
        if (isGameOver) return;

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveRate)
        {
            moveTimer = 0f;
            Move();
        }        
    }
    private void InputManagent()
    {
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2Int.down)
        {
            _direction = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2Int.up)
        {
            _direction = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2Int.right)
        {
            _direction = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2Int.left)
        {
            _direction = Vector2Int.right;
        }
    }
    private void Move()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        Vector2 newPos = new Vector2(
        Mathf.Round(transform.position.x) + _direction.x,
        Mathf.Round(transform.position.y) + _direction.y);

        transform.position = new Vector3(newPos.x, newPos.y, 0f);        
    }

    private void Grow()
    {
        Transform segments = Instantiate(this.snakeSegments);
        segments.position = _segments[_segments.Count - 1].position;
        _segments.Add(segments);
        StartCoroutine(EnableColliderDelayed(segments.gameObject));
        UpdateMoveRate();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            foodSFX.Play();
            score++;
            scoreText.text = score.ToString();
        } else if(other.tag == "Obstacle")
        {
            isGameOver = true;
            GrayScreenOn();
        }
    }
    private IEnumerator EnableColliderDelayed(GameObject segment)
    {
        yield return new WaitForSeconds(moveRate); 
        segment.tag = "Obstacle"; 
    }
    private void GrayScreenOn()
    {
        if(isGameOver == true)
        {
            grayScreen.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            resetButton.gameObject.SetActive(true);
        }
    }
    //Speed counting
    private void UpdateMoveRate()
    {
        float t = Mathf.Clamp01(_segments.Count / maxSegments);

        float curveValue = speedCurve.Evaluate(t); 

        moveRate = Mathf.Lerp(maxMoveRate, minMoveRate, curveValue);
    }
    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);            
        }
        _segments.Clear();
        _segments.Add(this.transform);
        this.transform.position = new Vector3(0f, -1.45f, 0f);

        scoreText.text = "0";
        isGameOver = false;
        grayScreen.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        moveRate = maxMoveRate;
    }   

    public void ResetButtonOnClicked()
    {
        ResetState();
    }
    public void QuitButtonOnClicked()
    {
        Debug.Log("Dabam");
        SceneManager.LoadScene(0);
    }   
}
