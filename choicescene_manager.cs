using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class choicescene_manager : MonoBehaviour
{
    public GameObject text;
    public GameObject right_frame;
    public GameObject left_frame;
    
    // Start is called before the first frame update
    void Start()
    {
        right_frame.SetActive(false);
        left_frame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void left_click(){
        left_frame.SetActive(true);
        right_frame.SetActive(false);
    }
    public void light_click(){
        right_frame.SetActive(true);
        left_frame.SetActive(false);
    }
    public void decision_click(){
        SceneManager.LoadScene("casino scene");
    }
}
