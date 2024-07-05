using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenemanager : MonoBehaviour
{
    public Text text_havecoin;
    public Text text_wincoin;
    int wincoin;
    // Start is called before the first frame update
    void Start()
    {
        text_havecoin.text=betscene_script.have_coin.ToString();
        text_wincoin.text=betscene_script.count.ToString();
        wincoin=betscene_script.have_coin+betscene_script.count;
        wincoin+=betscene_script.count;
        Invoke("win",1);
        PlayerPrefs.SetInt("key1",wincoin);
    }

    // Update is called once per frame
    void Update()
    {
        if(wincoin>200){
            SceneManager.LoadScene("clear scene");
        }
    }
    public void retry_button_click(){
        SceneManager.LoadScene("bet scene");
    }
    void win(){
        text_havecoin.text=wincoin.ToString();
    }
}
