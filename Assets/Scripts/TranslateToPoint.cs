using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateToPoint : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;
    private GameObject obj;
    public bool isTranslating = false;

    private float duration = 1f;
    private float elipseTime;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isTranslating)
        {
            elipseTime += Time.deltaTime;
            float purcent = elipseTime / duration;
            obj.transform.position = Vector3.Lerp(start, end, purcent);
            if (purcent >= 1)
            {
                ResetDash();

            }
        }

    }

    public void TranslateObject(GameObject Obj, Vector3 Start, Vector3 End, float Duration)
    {
        print("oktranslate");
        obj = Obj;
        start = Start;
        end = End;
        duration = Duration;
        isTranslating = true;

    }

    public void StopTRanslate()
    {
       
        ResetDash();
    }

    private void ResetDash()
    {
        print("finito");

        isTranslating = false;
        elipseTime = 0;
    }
}
