using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
//参考元　https://bright777.com/rules_blackjack
public class SceneManagerScript : MonoBehaviour
{
    int buttonFlag = 0;//ターンのためのフラグ
    int Gamestart_turnFlug = 1;
    int resultflag = 0;
    int enemyturn_frag = 0;
    int openflag=0;
    int xray = 0;//カード生成時にx座標をずらすための変数
    int mypoint = 0;//自分の得点　数値用
    int yourpoint = 0;//相手の得点
    int[] number_mycard = new int[20];//自分が持ってる一枚ずつの得点を保管しておく配列
    int[] yourcard = new int[20];//
    int[] card = new int[52];//カードにそれぞれ数字を割り当てる1~52
    //0~12 club 13~25 diamond 26~38 heart 39~51 spade ジョーカーは無い
    public GameObject[] object_mycard;//unityのUIに出す手札のオブジェクト
    public GameObject[] object_yourcard;
    public GameObject[] elements;//好きなカードを出すための配列オブジェクト
    public GameObject open;
    public GameObject hit;
    public Text mypointtext;//unityのUIに表示する自分の点数
    public Text yourpointtext;
    //試合結果を表示するテキスト
    //手札枚数把握用変数
    int me = 0;
    int you = 0;
    int num = 0;
    //ブラックジャックにおいてエースの処理をするときにどっちが持ってるaceか判断する変数
    bool turn;
    //エースを何枚持っているかの変数
    int ace_count_me = 0;
    int ace_count_enemy = 0;
    public Text bet_Text;//自分がbetシーンでかけた枚数を表示する変数
    // Start is called before the first frame update
    void Start()
    {
        //裏向きのカードを自分、相手二枚ずつ生成
        object_mycard[0] = Instantiate(elements[35], this.transform.position + new Vector3(-0.60f, 1.260f, -0.180f), Quaternion.Euler(90, 0, 0));
        object_mycard[1] = Instantiate(elements[35], this.transform.position + new Vector3(-0.450f, 1.260f, -0.180f), Quaternion.Euler(90, 0, 0));
        object_yourcard[0] = Instantiate(elements[35], this.transform.position + new Vector3(-0.60f, 1.260f, 0.30f), Quaternion.Euler(90, 0, 0));
        object_yourcard[1] = Instantiate(elements[35], this.transform.position + new Vector3(-0.450f, 1.260f, 0.30f), Quaternion.Euler(90, 0, 0));
        bet_Text.text=betscene_script.count.ToString();   
    }
    // Update is called once per frame
    void Update()
    {
        if (Gamestart_turnFlug == 1)
        {
            for (int i = 0; i < 52; i++)
            {
                card[i] = i + 1;
            }
            System.Random rng = new System.Random();//シャッフルしてる部分
            int n = card.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int tmp = card[k];
                card[k] = card[n]; ;
                card[n] = tmp;
            }
            //Debug.Log(card[0]);//先頭のカードを表示
            me = 0; you = 0;//ゲームごとのリセット


            
            turn = true;//trueが自分を表す
            number_mycard[me] = card[num];//一番目と二番目のカードを自分の点数とする
            Destroy(object_mycard[me]);//裏向きにしてるカードを消す
            object_mycard[me] = Instantiate(elements[card[num]], this.transform.position + new Vector3(-0.60f, 1.260f, -0.180f), Quaternion.Euler(270, 0, 0));
            me++; num++;
            number_mycard[me] = card[num];
            Destroy(object_mycard[me]);
            object_mycard[me] = Instantiate(elements[card[num]], this.transform.position + new Vector3(-0.450f, 1.260f, -0.180f), Quaternion.Euler(270, 0, 0));
            num++; me++;
            turn = false;//falseが相手を表す
            yourcard[you] = card[num];//三番目と四番目のカードを相手の点数とする
            Destroy(object_yourcard[you]);
            object_yourcard[0] = Instantiate(elements[card[num]], this.transform.position + new Vector3(-0.60f, 1.260f, 0.30f), Quaternion.Euler(90, 0, 0));
            you++; num++;
            yourcard[you] = card[num];
            Destroy(object_yourcard[you]);
            object_yourcard[1] = Instantiate(elements[card[num]], this.transform.position + new Vector3(-0.450f, 1.260f, 0.30f), Quaternion.Euler(90, 0, 0));
            you++; num++;

            yourpoint = 0;
            mypoint = 0;
            for (int i = 0; i < you; i++)
            {
                yourpoint += calcpoint(yourcard[i]);//絵札の処理をこの関数で行う
            }
            for (int i = 0; i < me; i++)
            {
                mypoint += calcpoint(number_mycard[i]);
            }
            mypointtext.text = mypoint.ToString();//自分の得点を表示する


            //カードをそれぞれ二枚配る処理が終わったのでhit,standの処理に移る
            Gamestart_turnFlug = 0;
            buttonFlag = 1;
            xray = 0;
            turn = true;//自分のとき
        }

        if (enemyturn_frag == 1)
        {
            turn = false;//相手のとき
            //カードを一枚ずつ見せるためのコード
            if (openflag==0)
            {
            Invoke("youropen", 1);
            }
            else
            {
                xray=0;
                xray++;
            }
            Invoke("youropen2", 2);
            Invoke("youropen3", 3);
            Invoke("youropen4", 4);
            Invoke("youropen5", 5);
            Invoke("youropen6", 6);
            Invoke("youropen7", 7);  
        }

        if (resultflag == 1)
        {
            //結果を確認できたらシーン切り替え
            if (mypoint > 21 && yourpoint > 21 || mypoint == yourpoint)
            {
                Invoke("draw",1.50f);//引き分け
            }
            if (yourpoint < 22 && mypoint < yourpoint || mypoint > 21 && yourpoint < 22)
            {
                Invoke("lose",1.50f);//負け
            }
            if (mypoint < 22 && yourpoint > 21 || mypoint < 22 && mypoint > yourpoint)
            {
                Invoke("win",1.50f);//勝ち
            }
        }

    }
    public void stand_button_click()
    {
        if (buttonFlag == 1)//ボタンを適切なタイミングでしか押せないようにする
        {
            buttonFlag = 0;//standボタンを押したら敵の得点処理に移る
            enemyturn_frag = 1;
        }
    }
    public void hit_button_click()
    {
        if (buttonFlag == 1)
        {
            //hitボタンを押したら二枚目の左に新たにもう一枚生成する
            xray++;
            number_mycard[me] = card[num];
            object_mycard[me] = Instantiate(elements[card[num]], this.transform.position + new Vector3(-0.450f + xray * 0.150f, 1.260f, -0.180f), Quaternion.Euler(270, 0, 0));
            mypoint += calcpoint(number_mycard[me]);//それを得点に足す
            mypointtext.text = mypoint.ToString();
            num++; me++;
            hit.GetComponent<AudioSource>().Play();
        }
        if (mypoint > 21 && ace_count_me > 0)
        {
            //バーストしかけた時にエースがあるなら11点の処理をしてるエースを一点に変える
            mypoint -= 10;
            ace_count_me--;
            mypointtext.text = mypoint.ToString();
        }
        if (mypoint > 21 && ace_count_me > 0)
        {
            mypoint -= 10;
            ace_count_me--;
            mypointtext.text = mypoint.ToString();
        }
        if (mypoint > 21)
        {
            //バーストしたら敵の得点処理に移る
            buttonFlag = 0;
            enemyturn_frag = 1;
        }
    }
    //勝敗結果によってそれぞれシーン切り替えをする
    void win(){
        SceneManager.LoadScene("win scene");
    }
    void draw(){
        SceneManager.LoadScene("draw scene");
    }
    void lose(){
        SceneManager.LoadScene("lose scene");
    }
   
    int calcpoint(int point)//絵札処理の関数
    {
        point = (point % 13) + 1;
        if (point > 10)//11以上は10に
        {
            point = 10;
        }
        if (point == 1)//ace処理
        {
            if (turn == true)//自分のとき
            {
                point = 11;
                ace_count_me += 1;
            }
            if (turn == false)//相手のとき
            {
                point = 11;
                ace_count_enemy += 1;
            }
        }
        return point;//絵札処理を終えたものを返す
    }
    void youropen()//相手の一枚目を裏返す処理
    {
        xray = 0;
        xray++;
        object_yourcard[0].transform.rotation = Quaternion.Euler(270, 0, 0);
        yourpointtext.text = calcpoint(yourcard[0]).ToString();
        //一枚目の得点だけ入れる
    }
    void youropen2()//相手の二枚目を裏返す処理
    {
        object_yourcard[1].transform.rotation = Quaternion.Euler(270, 0, 0);
        yourpointtext.text = yourpoint.ToString();
    }
    void youropen3()//三枚目を裏返す処理処理
    {
        if (yourpoint < 17)
        {
            yourcard[2] = card[2];
            object_yourcard[2] = Instantiate(elements[card[2]], this.transform.position + new Vector3(-0.450f + xray * 0.150f, 1.260f, 0.30f), Quaternion.Euler(270, 0, 0));
            yourpoint += calcpoint(yourcard[2]);
            yourpointtext.text = yourpoint.ToString();
            xray++;
            you++; num++;
        }
        if (yourpoint > 21 && ace_count_enemy > 0)//バーストしそうace処理
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 21 && ace_count_enemy > 0)
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 16)//ディーラー側は17点以上でストップする処理
        {
            enemyturn_frag = 0;
            resultflag = 1;
        }

    }
    void youropen4()
    {
        if (yourpoint < 17)
        {
            yourcard[3] = card[3];
            Debug.Log(num);
            object_yourcard[3] = Instantiate(elements[card[3]], this.transform.position + new Vector3(-0.450f + xray * 0.150f, 1.260f, 0.30f), Quaternion.Euler(270, 0, 0));
            yourpoint += calcpoint(yourcard[3]);
            yourpointtext.text = yourpoint.ToString();
            xray++;
            you++; num++;
        }
        if (yourpoint > 21 && ace_count_enemy > 21)
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 21 && ace_count_enemy > 21)
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 16)
        {
            enemyturn_frag = 0;
            resultflag = 1;
        }
    }
    void youropen5()//5枚目を裏返す処理処理
    {
        if (yourpoint < 17)
        {
            yourcard[4] = card[4];
            object_yourcard[4] = Instantiate(elements[card[4]], this.transform.position + new Vector3(-0.450f + xray * 0.150f, 1.260f, 0.30f), Quaternion.Euler(270, 0, 0));
            yourpoint += calcpoint(yourcard[4]);
            yourpointtext.text = yourpoint.ToString();
            xray++;
            you++; num++;
        }
        if (yourpoint > 21 && ace_count_enemy > 0)//バーストしそうace処理
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 21 && ace_count_enemy > 0)
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 16)//ディーラー側は17点以上でストップする処理
        {
            enemyturn_frag = 0;
            resultflag = 1;
        }

    }
    void youropen6()
    {
        if (yourpoint < 18)
        {
            yourcard[you] = card[num];
            Debug.Log(num);
            object_yourcard[you] = Instantiate(elements[card[num]], this.transform.position + new Vector3(-0.450f + xray * 0.150f, 1.260f, 0.30f), Quaternion.Euler(270, 0, 0));
            yourpoint += calcpoint(yourcard[you]);
            yourpointtext.text = yourpoint.ToString();
            xray++;
            you++; num++;
        }
        if (yourpoint > 21 && ace_count_enemy > 21)
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 21 && ace_count_enemy > 21)
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 17)
        {
            enemyturn_frag = 0;
            resultflag = 1;
        }
    }
    void youropen7()
    {
        if (yourpoint < 18)
        {
            yourcard[you] = card[num];
            Debug.Log(num);
            object_yourcard[you] = Instantiate(elements[card[num]], this.transform.position + new Vector3(-0.450f + xray * 0.150f, 1.260f, 0.30f), Quaternion.Euler(270, 0, 0));
            yourpoint += calcpoint(yourcard[you]);
            yourpointtext.text = yourpoint.ToString();
            xray++;
            you++; num++;
        }
        if (yourpoint > 21 && ace_count_enemy > 21)
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 21 && ace_count_enemy > 21)
        {
            yourpoint -= 10;
            ace_count_enemy--;
            yourpointtext.text = yourpoint.ToString();
        }
        if (yourpoint > 17)
        {
            enemyturn_frag = 0;
            resultflag = 1;
        }
    }
    public void click_open_ability(){
        if (buttonFlag==1)
        {
        object_yourcard[0].transform.rotation = Quaternion.Euler(270, 0, 0);
        yourpointtext.text = calcpoint(yourcard[0]).ToString();
        //一枚目の得点だけ入れる
        openflag=1;
        open.GetComponent<AudioSource>().Play();
        open.SetActive(false);
        }
    }
}
