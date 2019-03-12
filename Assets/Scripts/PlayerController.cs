using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    private Rigidbody rb;
    private int count;
    private int count2;
    public Text countText;
    public Text winText;
    public Text doubleornothing;
    public GameObject yes;
    public GameObject no;
    public Text time;
    public float timeLeft = 120.0f;
    public bool stop = true;
    //private bool win = false;
    private float minutes;
    private float seconds;
    private int score;
    private bool multiplier = false;
    //countdown vars
    private float cd = 3.0f;
    private bool cdover = true;
    private bool loser = false;
    private bool winner = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winText.text = "";
        doubleornothing.text = "";
        yes.SetActive(false);
        no.SetActive(false);
        SetCountText();
        startTimer();
    }
    void Update()
    {
        if(cdover)
        {
            cd -= Time.deltaTime;
            winText.text = cd.ToString("0");
            //winText.text = winText.text.Substring(0, 1);
            Debug.Log("***" + winText.text + "***");
        }
       
        if (cd < 0)
        {
            cdover = false;
            //winText.text = "";
            if (stop) return;
            timeLeft -= Time.deltaTime;

            minutes = Mathf.Floor(timeLeft / 60);
            seconds = timeLeft % 60;
            if (seconds > 59) seconds = 59;
            if (minutes < 0)
            {
                stop = true;
                minutes = 0;
                seconds = 0;
                loser = true;
                winText.text = "You lose";
                doubleornothing.text = "Try again?";
                yes.SetActive(true);
                no.SetActive(true);
            }
        }
    }
    private void FixedUpdate()
    {
        if(!stop)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * speed);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            if(multiplier)
            {
                count2++;
               
            }
            else
            {
                count++;
            }
            
            SetCountText();
        }
        if(other.gameObject.CompareTag("triggercube"))
        {
            if(!stop)
            {
                winText.text = "You Win!";
                doubleornothing.text = "Double or nothing?";
                yes.SetActive(true);
                no.SetActive(true);
            }
        }
    }
   
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
    public void startTimer()
    {
        stop = false;
        //timeLeft = 120;
        Update();
        StartCoroutine(updateCoroutine());
    }
    private IEnumerator updateCoroutine()
    {
        while (!stop)
        {
            time.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void computeScore()
    {
        if (loser)
        {
            count = 0;
            count2 = 0;
        }
        if (winner)
        {
            count = count + (count2 * 2);
        }
    }
    public void doubleStart(int i)
    {
        if (i == 1)
        {
            Debug.Log("hit");
            transform.position = target;
            winText.text = "";
            doubleornothing.text = "";
            yes.SetActive(false);
            no.SetActive(false);
            timeLeft = 60;
            multiplier = true;
        }
        if (i == 2)
        {
            Debug.Log("hit");
            transform.position = target;
            winText.text = "";
            doubleornothing.text = "";
            yes.SetActive(false);
            no.SetActive(false);
            timeLeft = 30;
            multiplier = true;
        }
    }
    

}
