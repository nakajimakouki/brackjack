using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startscenemanager : MonoBehaviour
{
    public GameObject startbuton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStartButton()
    {
        startbuton.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("main Scene");//ボタンを押すとスタート画面から自宅にシーン移動する
    }
}
