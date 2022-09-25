using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Recenter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Recenter()
    {
        yield return new WaitForSeconds(1.08f);
        this.transform.parent.position = this.transform.position;
        //this.transform.localPosition = Vector3.zero;
    }
}
