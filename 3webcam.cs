using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using MixedReality.Toolkit.UX;


public class RestApiIMMAGINE : MonoBehaviour
{
    private string[] urls = {
        "http://109.233.191.228:8090/cam_5.jpg",
        "http://109.233.191.228:8090/cam_6.jpg",
        "http://109.233.191.228:8090/cam_7.jpg"
    };

    public GameObject[] quad;

    public PressableButton button1;
    public PressableButton button2;
    public PressableButton button3;

    public GameObject canva1;
    public GameObject canva2;
    public GameObject canva3;



   

    private bool isRequesting = false;
    private float intervallo = 0.1f;
    private float timer = 0f;



    void Start()
    {

        if (quad == null || quad.Length == 0)
        {
            Debug.LogError("Please assign quads to the script in the inspector");
        }

        if (button1 == null || button2 == null || button3 == null)
        {
            Debug.LogError("Please assign buttons to the script in the inspector");
        }

        if (canva1 == null || canva2 == null || canva3 == null)
        {
            Debug.LogError("Please assign canvases to the script in the inspector");
        }

       
        canva1.SetActive(false);
        canva2.SetActive(false);
        canva3.SetActive(false);

       

       
        button1.OnClicked.AddListener(() => RicreaCanva(canva1));
        button2.OnClicked.AddListener(() => RicreaCanva(canva2));
        button3.OnClicked.AddListener(() => RicreaCanva(canva3));

       
    }

 
    void RicreaCanva(GameObject canva)
    {

       if (canva.activeSelf)
        {
            canva.SetActive(false);
           
        }


        canva.SetActive(true);
    }


   

    void Update()
    {
        timer += Time.deltaTime;

        if (!isRequesting && timer >= intervallo)
        {
            StartCoroutine(LoadImages());
            timer = 0f;
        }
    }

    IEnumerator LoadImages()
    {
        isRequesting = true;

        for (int i = 0; i < urls.Length; i++)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(urls[i]))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error: " + www.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);
                    applicare(texture, i);
                }
            }
        }

        isRequesting = false;
    }

    void applicare(Texture2D texture, int quadIndex)
    {
        if (quadIndex >= 0 && quadIndex < quad.Length && texture != null)
        {
            Renderer renderer = quad[quadIndex].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = texture;
            }

           
            if (canva1.activeSelf && quadIndex == 0)
            {
               
                GameObject quadInCanvas = canva1.transform.Find("1quad").gameObject;
                if (quadInCanvas != null)
                {



                    Renderer quadRenderer = quadInCanvas.GetComponent<Renderer>();
                    if (quadRenderer != null)


                    {
                        quadRenderer.material.mainTexture = texture;
                    }
                }
            }
            else if (canva2.activeSelf && quadIndex == 1)
            {
               
                GameObject quadInCanvas = canva2.transform.Find("1quad").gameObject;
                if (quadInCanvas != null)
                {
                    Renderer quadRenderer = quadInCanvas.GetComponent<Renderer>();
                    if (quadRenderer != null)
                    {
                        quadRenderer.material.mainTexture = texture;
                    }
                }
            }
            else if (canva3.activeSelf && quadIndex == 2)
            {
               
                GameObject quadInCanvas = canva3.transform.Find("1quad").gameObject;
                if (quadInCanvas != null)
                {
                    Renderer quadRenderer = quadInCanvas.GetComponent<Renderer>();
                    if (quadRenderer != null)
                    {
                        quadRenderer.material.mainTexture = texture;
                    }
                }
            }
        }
    }

}
