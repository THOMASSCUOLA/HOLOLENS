using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using UnityEngine.UI;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.Input;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class webrequest : MonoBehaviour
{
    public GameObject imageQuad;
    public PressableButton btn1; // Utilizzando Interactable di MRTK3
    public PressableButton btn2; // Utilizzando Interactable di MRTK3

    private string serverURL1 = "http://185.137.146.14/jpg/image.jpg";
    private string serverURL2 = "http://129.125.136.20/jpg/image.jpg";
    private string currentURL;

    private bool isRequesting = false;
    private float intervallo = 0.1f;
    private float timer = 0f;

    void Start()
    {
        btn1.OnClicked.AddListener(ChangeURL1); // Aggiungendo un listener al click del pulsante MRTK3
        btn2.OnClicked.AddListener(ChangeURL2); // Aggiungendo un listener al click del pulsante MRTK3
        currentURL = serverURL1;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!isRequesting && timer >= intervallo)
        {
            StartCoroutine(LoadImage());
            timer = 0f;
        }
    }

    IEnumerator LoadImage()
    {
        isRequesting = true;

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(currentURL))
        {
            www.useHttpContinue = false;
            www.redirectLimit = 0;
            www.chunkedTransfer = false;
            www.certificateHandler = new BypassCertificate();

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                if (www.isHttpError || www.isNetworkError)
                {
                    Debug.LogError("HTTP request error: " + www.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);

                    MeshRenderer quadRenderer = imageQuad.GetComponent<MeshRenderer>();
                    if (quadRenderer == null)
                    {
                        Debug.LogError("The GameObject must have a MeshRenderer component.");
                        yield break;
                    }

                    Material material = new Material(Shader.Find("Standard"));
                    material.mainTexture = texture;
                    quadRenderer.material = material;
                }
            }
        }

        isRequesting = false;
    }

    void ChangeURL1()
    {
        currentURL = serverURL1;
    }

    void ChangeURL2()
    {
        currentURL = serverURL2;
    }

    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
