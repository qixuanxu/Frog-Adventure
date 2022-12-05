### Week.4 ###
#### UI Design ####
The layout of each interface
![week1_画板 1](https://user-images.githubusercontent.com/91987208/204581829-83027e22-1a67-497e-8b7e-d31a0cd495f5.jpg)

#### Development Process ####
In the early days of game development, I tried to use visual programming to control my game characters，
In later stages of development, I found that the visual code and some of the c## code for the ui canvas would not run at the same time
///
<img width="1337" alt="截屏2022-11-22 下午2 28 33" src="https://user-images.githubusercontent.com/91987208/204582654-c342d652-6d65-428e-b15a-e467b30d6adb.png">
<img width="1199" alt="截屏2022-11-29 下午2 35 59" src="https://user-images.githubusercontent.com/91987208/204582705-e8bd92e0-dc8e-4be6-9c13-48e21a49d52f.png">
<img width="1116" alt="截屏2022-11-29 下午4 17 54" src="https://user-images.githubusercontent.com/91987208/204583867-27ddd80c-a8c6-4971-ad84-da15e3c3904c.png">

### Week.5 ###
Then I switched to c# code to control my character and canvas, and put the modeled animations into unity

Part.1:The code that controls the canvas
  ```
  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;
    public Transform BloodTrans;
    public Transform AgainPanel;
 

    private void Awake()
    {
        instance = this;
    }

    public void GotoScene(string name)
    {
        Application.LoadLevel(name);
    }

    public void ReduceBlood(int index)
    {
        for (int i = 0; i < BloodTrans.childCount; i++)
        {
            var go = BloodTrans.GetChild(i);
            if (go.transform.name==index.ToString())
            {
                go.gameObject.SetActive(false);
            }
        }
    }

   
}
  ```
 Part.2:Code to control Frog
  ```
  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text text;
    private int BloodNum = 0;
    private Rigidbody rigidbody;
    public Transform Sphere;
    public Transform StartPos;
    public AudioSource audio;
    public AudioClip clip;
    public Transform AgainTipPanel;
    public Transform TryaginPanel;
    public Transform AgainView;
    public Transform FrogObj;
    public Transform floorObj;
    private List<string> TextArray = new List<string>() { "When I was a child","Is everything all right my old friend?", "Where you from?", "I think I've heard of you from my mom.",
    "Fate or cage?","See you","Something strange","Its really bitter.","We were both young","Is that our first meet?",
    "I close my eyes and the flashback starts","I'm standin' there","On a balcony in summer air","See the lights","I hate winter, "," I lose my fingerprints in winter",
    "See you ","when I lose my fingerprints","what do I have to prove my identity?","Little did I know","My friends often mention you","you were throwin' pebbles",
    "How are you this month?","Strange and familiar","Is this really our first meeting?","Beggin' you","Please don't go"};
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        //Invoke();
        InvokeRepeating("Spwan", 3, 3);
        isTips = false;
    }

   
   void Spwan()
    {
        GameObject.Instantiate(Sphere, StartPos.position,new Quaternion(0,180,0,0));
    }
     
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = (transform.up).normalized * 10;
            //rigidbody.AddForce(transform.up * 20);
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.name== "keDouPrefab(Clone)")
        {
            AudioSource.PlayClipAtPoint(clip, col.transform.position);
            Debug.Log("-----------��ײ������-----------" + TextArray.Count);
            text.gameObject.SetActive(true);
            int num = Random.Range(0, 27);
            text.text = TextArray[num];
            StartCoroutine(TextCor(text, false));
            GameObject.Destroy(col.gameObject);

            if (BloodNum>=2)
            {
                CancelInvoke("Spwan");
                AgainTipPanel.gameObject.SetActive(true);
                
                //CanvasController.instance.AgainPanel.gameObject.SetActive(true);
                //return;
            }
           
            CanvasController.instance.ReduceBlood(BloodNum);
            BloodNum++;

        }
       
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.transform.name == "Sphere(Clone)")
        {
            StartCoroutine(TextCor(text,false));
        }
       
    }

    IEnumerator TextCor(Text text,bool isShow)
    {
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(isShow);
    }

    private bool isTips;
    public void ClickAgainTipsPanel()
    {
        AgainTipPanel.gameObject.SetActive(false);
        TryaginPanel.gameObject.SetActive(true);
        AgainView.gameObject.SetActive(true);
        FrogObj.gameObject.SetActive(false);
        floorObj.gameObject.SetActive(false);
        CancelInvoke("Spwan");
    }

}

  ```
  
  Part.3 code to control enemy
  
  ```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    private Transform trans;
    void Start()
    {
        trans = this.transform;
    }

    private void Update()
    {
        StartSpawn();
      
    }

    void StartSpawn()
    {
        trans.Translate(-Vector3.left * 5f*Time.deltaTime);
        if (trans.position.x < -15)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}

```
      
<img width="1005" alt="截屏2022-12-05 下午5 49 41" src="https://user-images.githubusercontent.com/91987208/205706932-7f33b5ab-2c23-48cf-a342-4557fb3bcddf.png">

