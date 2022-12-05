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
