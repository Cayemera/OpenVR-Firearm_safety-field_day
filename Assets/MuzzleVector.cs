using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MuzzleVector : MonoBehaviour
{
    private int unsafe_aims = 0;
    private int tc_count = 0;
    private int sc_count = 0;
    private int cc_count = 0;
    public UnityEvent instructorEvent;
    public UnityEvent tcTextEvent;
     public UnityEvent tcfade;
      public UnityEvent tcunfade;

    public GameObject tcPlane, ccPlane, scPlane;
    public GameObject tcText, ccText, scText;
    
    private void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "Instructor"){
            unsafe_aims++;
            Debug.Log("Pointing gun at insructor " + unsafe_aims + " times!");
            instructorEvent.Invoke();
        }

        if(collider.gameObject.tag == "TrailCarry"){
            tc_count++;
            Debug.Log("Started performing Trail Carry " + tc_count + " time!");
            StartCoroutine(fadeOut(tcPlane.GetComponent<MeshRenderer>(),1f));
            StartCoroutine(fadeOutText(tcText.GetComponent<TextMesh>(),1f));
            tcTextEvent.Invoke();
        }

        if(collider.gameObject.tag == "ShoulderCarry"){
            sc_count++;
            Debug.Log("Started performing Shoulder Carry " + sc_count + " time!");
        }

         if(collider.gameObject.tag == "CradleCarry"){
            cc_count++;
            Debug.Log("Started performing Cradle Carry " + cc_count + " time!");
            StartCoroutine(fadeOut(ccPlane.GetComponent<MeshRenderer>(),1f));
            //tcTextEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider collider){
        if(collider.gameObject.tag == "TrailCarry"){
            Debug.Log("Performing Trail Carry");
            tcfade.Invoke();
        }
        if(collider.gameObject.tag == "ShoulderCarry"){
            Debug.Log("Performing Shoulder Carry");
        }
        if(collider.gameObject.tag == "CradleCarry"){
            Debug.Log("Performing Cradle Carry");
        }
    }

    private void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "TrailCarry"){
            Debug.Log("Stopped Performing Trail Carry");
            StartCoroutine(fadeIn(tcPlane.GetComponent<MeshRenderer>(),1f));
            tcunfade.Invoke();
        }
            if(collider.gameObject.tag == "ShoulderCarry"){
                Debug.Log("Stopped Performing Shoulder Carry");
        }

        if(collider.gameObject.tag == "CradleCarry"){
            Debug.Log("Stopped Performing Cradle Carry");
            StartCoroutine(fadeIn(ccPlane.GetComponent<MeshRenderer>(),1f));
           // tcunfade.Invoke();
        }
    }

   IEnumerator fadeIn(MeshRenderer MyRenderer,float duration){
    float counter = 0;
    //Get current color
    Color spriteColor = MyRenderer.material.color;

    while (counter < duration)
    {
        counter += Time.deltaTime;
        //Fade from 1 to 0
        float alpha = Mathf.Lerp(0,0.368f, counter / duration);
       // Debug.Log(alpha);

        //Change alpha only
        MyRenderer.material.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
        //Wait for a frame
        yield return null;
    }
    }

    IEnumerator fadeOut(MeshRenderer MyRenderer,float duration){
    float counter = 0;
    //Get current color
    Color spriteColor = MyRenderer.material.color;

    while (counter < duration)
    {
        counter += Time.deltaTime;
        //Fade from 1 to 0
        float alpha = Mathf.Lerp(spriteColor.a, 0, counter / duration);
       // Debug.Log(alpha);

        //Change alpha only
        MyRenderer.material.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
        //Wait for a frame
        yield return null;
    }
    }

    IEnumerator fadeOutText(TextMesh tm,float duration){
    float counter = 0;
    //Get current color
    Color spriteColor = tm.color;

    while (counter < duration)
    {
        counter += Time.deltaTime;
        //Fade from 1 to 0
        float alpha = Mathf.Lerp(spriteColor.a, 0, counter / duration);
       // Debug.Log(alpha);

        //Change alpha only
        tm.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
        //Wait for a frame
        yield return null;
    }
    }


}
