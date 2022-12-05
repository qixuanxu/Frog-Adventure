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
