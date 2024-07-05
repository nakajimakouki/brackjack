using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class drawscenemanager : MonoBehaviour
{
    public Text text_havecoin;
    int newcoin;
    // Start is called before the first frame update
    void Start()
    {
        text_havecoin.text=betscene_script.have_coin.ToString();
        newcoin=betscene_script.have_coin+betscene_script.count;
        PlayerPrefs.SetInt("key1",newcoin);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void retry_button_click(){
        SceneManager.LoadScene("bet scene");
    }

}
