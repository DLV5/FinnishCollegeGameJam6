using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = 0;
    bool rotate = false;

    private void Start()
    {
        InitialState();
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.K) && index != pages.Count) 
        {
            RotateForward();
        }
        if(Input.GetKeyUp(KeyCode.L) && !(index - 1 <= -1))
        {
            RotateBack();
        }
    }

    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }
        pages[0].SetAsLastSibling();
    }

    public void RotateForward()
    {
        if (rotate)
            return;
        index++;
        Debug.Log(index);
        float angle = 180; //in order to rotate the page forward, you need to set the rotation by 180 degrees around the y axis
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));

    }


    public void RotateBack()
    {
        if (rotate)
            return;
        Debug.Log(index);
        index--;
        float angle = 0; //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, false));
    }


    IEnumerator Rotate(float angle, bool forward)
    {
        float elapsedTime = 0f;
        float percentageComplete = 0f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z));
        rotate = true;

        while (true)
        {
            elapsedTime += Time.deltaTime * pageSpeed;
            percentageComplete = elapsedTime / pageSpeed;

            //pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, elapsedTime); //smoothly turn the page

            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, percentageComplete);

            if (Quaternion.Angle(pages[index].rotation, targetRotation) < 0.1f)
            {
                pages[index].rotation = targetRotation;

                //if (forward == false)
                //{
                //    index--;
                //}
                rotate = false;
                break;

            }
            yield return null;

        }
    }



}

