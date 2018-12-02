using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ANRU_playercontroller : MonoBehaviour {

    public float speed;

    public Text counttext;         
    public Text endtext;

    private Rigidbody2D rb2d;      
    private int count;

    private float timer;
    private int wholetime;

    public AudioSource starsound;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        endtext.text = "";
        SetCountText();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        timer = timer + Time.deltaTime;
        if (timer >= 10)
        {
            endtext.text = "you lose.";
            StartCoroutine(ByeAfterDelay(2));

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            starsound.Play ();
            count = count + 2;
            GameLoader.AddScore(count); 
            SetCountText();
        }
        
    }
    void SetCountText()
    {
        counttext.text = "score: " + count.ToString();
        if (count >= 10)
        {
            endtext.text = "you win!";
            StartCoroutine(ByeAfterDelay(2));
        }
            
    }
    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GameLoader.gameOn = false;
    }
}
