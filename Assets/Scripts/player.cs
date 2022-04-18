using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    public float moverSpeed = 5f;

    GameObject currentFloor;
    [SerializeField]int HP;
    public GameObject HPBar; 

    public Text scoreText;

    public GameObject replayButton;
    int score;
    float scoreTime;
    void Start()
    {
        HP = 10;
        score=0;
        scoreTime=0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow)){
         transform.Translate(moverSpeed*Time.deltaTime,0,0);
         GetComponent<SpriteRenderer>().flipX = false;
         GetComponent<Animator>().SetBool("run",true);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-moverSpeed*Time.deltaTime,0,0);
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("run",true);
        }
        else{
             GetComponent<Animator>().SetBool("run",false);
        }
        UpdateScore();
    }


     void OnCollisionEnter2D(Collision2D other) {
 
        if(other.gameObject.tag =="Normal")
        {
            if(other.contacts[0].normal == new Vector2(0f,1f)){
                Debug.Log("撞到第一種階梯");
                currentFloor=other.gameObject;
                ModifyHP(1);
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        
        }   
        else if(other.gameObject.tag =="Nails")
        {
            if(other.contacts[1].normal == new Vector2(0f,1f)){
                Debug.Log("撞到第二種階梯");
                currentFloor=other.gameObject;
                ModifyHP(-3);
                GetComponent<Animator>().SetTrigger("Hurt");
                other.gameObject.GetComponent<AudioSource>().Play();
            }
             
        }
        else if(other.gameObject.tag =="Ceiling")
        {
            Debug.Log("撞到天花板");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHP(-3);
            GetComponent<Animator>().SetTrigger("Hurt");
            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
         if(other.gameObject.tag =="DeathLine")
        {
            Debug.Log("Game Over");
            die();
        }   
    }


    void ModifyHP(int num){ 
        HP += num;
        if(HP>10){
            HP = 10;
        }
        else if(HP<=0){
            HP = 0;
            die();
        }
        UpdateHPBar();
    }

    void UpdateHPBar(){
        for(int i=0;i<HPBar.transform.childCount;i++){

            if(HP>i){
                HPBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else{
                HPBar.transform.GetChild(i).gameObject.SetActive(false);

            }

        }    
    }
    void UpdateScore(){
        scoreTime += Time.deltaTime;
        if(scoreTime>2f){
            score++;
            scoreTime=0f;
            scoreText.text="地下"+score.ToString()+"層";
        }
    }

    void die(){
        GetComponent<AudioSource>().Play();
        Time.timeScale = 0f;
        replayButton.SetActive(true);
    } 

    public void Replay(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
} 
