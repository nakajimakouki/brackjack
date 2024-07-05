using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermanager : MonoBehaviour
{
    public float speed = 0.0001f;
    private Rigidbody rb;
    Animator animator;
    bool rightmove;
    bool leftmove;
    bool frontmove;
    bool backmove;
    public GameObject door;
    //Vector3 posi =this.transform.Position;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();//接触系の情報取得
        //変数にAnimatorの情報を取得して入れる
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Vector3 force;
        if(Input.GetKey("right")==true||rightmove==true)//右にキーを押したらキャラクターが右に向きを変え動く
        {
           
            rb.velocity=new Vector3(-20,0,0);
            this.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        if (Input.GetKeyUp("right")==true||Input.GetKeyUp("left")==true||
        Input.GetKeyUp("up")==true||Input.GetKeyUp("down")==true)//velocityの仕様によりキーを離しても減速式で少しだけ移動が続くため
        {
            rb.velocity=new Vector3(0,0,0);
        }
        if(rightmove==false&&leftmove==false&&frontmove==false&&backmove==false){
            rb.velocity=new Vector3(0,0,0);
        }
        if (Input.GetKey("left") == true||leftmove==true)//左にキーを押したらキャラクターが左に向きを変え進む
        {
            rb.velocity=new Vector3(20,0,0);
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey("up") == true||backmove==true)//上にキーを押したらキャラクターが奥に向きを変え動く
        {
            rb.velocity=new Vector3(0,0,-20);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey("down") == true||frontmove==true)//下にキーを押したらキャラクターが手前に向きを変え動く
        {
            rb.velocity=new Vector3(0,0,20);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //移動キーを押したらランアニメーションが起動するようにする
        if (Input.GetKeyDown("right"))
        {
            animator.SetBool("Running", true);
        }
        if (Input.GetKeyUp("right"))
        {
            animator.SetBool("Running", false);
        }
        if (Input.GetKeyDown("left"))
        {
            animator.SetBool("Running", true);
        }
        if (Input.GetKeyUp("left"))
        {   
            animator.SetBool("Running", false);
        }
        if (Input.GetKeyDown("up"))
        {
            animator.SetBool("Running", true);
        }
        if (Input.GetKeyUp("up"))
        {
            animator.SetBool("Running", false);
        }
        if (Input.GetKeyDown("down"))
        {
            animator.SetBool("Running", true);
        }
        if (Input.GetKeyUp("down"))
        {   
            animator.SetBool("Running", false);
        }
    }

    void OnCollisionEnter(Collision col){//接触時に体がスライドしてしまう不具合があったためテコ入れ用
        if(col.gameObject.tag=="wallLight"){
            this.transform.position += new Vector3(20 * Time.deltaTime,0,0.6210f * Time.deltaTime);
        }
        if(col.gameObject.tag=="wallLeft"){
            this.transform.position+= new Vector3(-13.50f*Time.deltaTime,0,0.010f);
        }
        if(col.gameObject.tag=="wallback"){
            this.transform.position+= new Vector3(0,0,-0.5110f);
        }
        if(col.gameObject.tag=="door"){
            SceneManager.LoadScene("setumei scene 1");
            door.GetComponent<AudioSource>().Play();
        }
    }

    public void RightButtonDown()//以下移動ボタン実装用のコード
    {
        rightmove = true;
        animator.SetBool("Running", true);
    }
    public void RightButtonUp()
    {
        rightmove = false;
        animator.SetBool("Running", false);
    }
    public void LeftButtonDown()
    {
        leftmove = true;
        animator.SetBool("Running", true);
    }
    public void LeftButtonUp()
    {
        leftmove = false;
        animator.SetBool("Running", false);
    }
    public void downButtonDown()
    {
        frontmove = true;
        animator.SetBool("Running", true);
    }
    public void downButtonUp()
    {
        frontmove = false;
        animator.SetBool("Running", false);
    }
    public void upButtonDown()
    {
        backmove = true;
        animator.SetBool("Running", true);
    }
    public void upButtonUp()
    {
        backmove = false;
        animator.SetBool("Running", false);
    }
}
