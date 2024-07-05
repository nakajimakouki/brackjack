using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class setumeiscenescript : MonoBehaviour
{
    int count=0;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void push_before(){
        if (count==0)
        {
            SceneManager.LoadScene("setumei scene 1");
        }
        if (count==1)
        {
            count--;
            text.text="ブラックジャックのルール\n手持ちのカードの合計が21を超えたら負けです\nj以上の絵札は10としてカウントします\nAは1としても11としてもカウントできます";
        }
    }
    public void push_after(){
        if(count==0){
            count++;
            text.text="イカサマの説明 \n右下にあるボタンを押すとイカサマが発動できます\n発動すると相手の一枚目が見えます";
        }
        if(count==1){
            SceneManager.LoadScene("bet scene");
        }
    }
}
