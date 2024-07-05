using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class losescenemanager : MonoBehaviour
{
    public Text text_havecoin;
    public Text text_losecoin;
    int newcoin;
    // Start is called before the first frame update
    void Start()
    {
        text_havecoin.text=betscene_script.have_coin.ToString();
        text_losecoin.text=betscene_script.count.ToString();
        newcoin=betscene_script.have_coin;
        PlayerPrefs.SetInt("key1",newcoin);
    }

    // Update is called once per frame
    void Update()
    {
        if(newcoin<0){
            SceneManager.LoadScene("gameover scene");
        }
    }
    public void retry_button_click(){
        SceneManager.LoadScene("bet scene");
    }
    
}
