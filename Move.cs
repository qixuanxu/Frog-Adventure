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
