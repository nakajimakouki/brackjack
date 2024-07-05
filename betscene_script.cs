using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class betscene_script : MonoBehaviour
{
    public  Text bet_text;
    public Text text_have_coin;
    public GameObject betcoin;
    public GameObject button;
    public GameObject plusbutton;
    public static int count;
    public GameObject[] row_coin;
    public static int have_coin;
    public static int a1;
    
    // Start is called before the first frame update
    void Start()
    {
        count=0;
        a1=PlayerPrefs.GetInt("key1",50);
        have_coin=a1;
    }

    // Update is called once per frame
    void Update()
    {
        bet_text.text=count.ToString();
        text_have_coin.text=have_coin.ToString(); 
    }
    public void plus_button_click(){//上限を50枚にする
        if(count<50){
        count+=1;
        have_coin-=1;
        row_coin[count-1]=Instantiate(betcoin,new Vector3(-0.010f,1.30f,0.0010f),Quaternion.identity);
        plusbutton.GetComponent<AudioSource>().Play();
        }
    }
    public void minus_button_click(){
        if(count>0){
            Destroy(row_coin[count-1]);
            count-=1;
            have_coin+=1;
        }
    }
    public void startbutton_click(){
        button.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("casino scene");
    }
}
