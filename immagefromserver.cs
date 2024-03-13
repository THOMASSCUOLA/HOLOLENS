using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;
using static UnityEngine.Shader;

public class RestApiIMMAGINE : MonoBehaviour
{
    public GameObject imageQuad;


    void Start()
    {
        //bool boolean = true;
        ////while (boolean)
        ////{
        //for (int i = 0; i < 10; i++) {
        //    chiamataIMAGE();
        //    System.Threading.Thread.Sleep(1000);
        ////}
        //}
        //// DA RIFARE CON THREAD!!!
    }


    void Update()
    {
        chiamataIMAGE();
        System.Threading.Thread.Sleep(100);
    }

    // Decodifica la stringa da base64 a una Texture2D (la stringa rappresenta un'immagine)
    public Texture2D Decode(string input)
    {
        byte[] img = System.Convert.FromBase64String(input);
        Texture2D txt = new Texture2D(1, 1);
        txt.LoadImage(img);
        return txt;
    }

    // Inserisce la Texture2D in un nuovo materiale
    public Material MaterialWithTexture(Texture2D texture) {
        Material material = new Material(Shader.Find("Standard"));
        material.mainTexture = texture;
        return material;
    }

    async void chiamataIMAGE()
    {
        try
        {
            // Creo un'istanza di HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Imposto l'indirizzo del server
                client.BaseAddress = new Uri("http://127.0.0.1:3000");

                // Creo un'istanza di HttpResponseMessage
                HttpResponseMessage response = new HttpResponseMessage();

                // Eseguo la chiamata GET
                response = await client.GetAsync("/immagine");

                // Leggo il contenuto della risposta
                string content = await response.Content.ReadAsStringAsync();

                // Applica il materiale con l'immagine all'imageQuad
                MeshRenderer quadRenderer = imageQuad.GetComponent<MeshRenderer>();
                if (quadRenderer == null)
                {
                    Debug.LogError("Il GameObject deve avere un componente MeshRenderer.");
                    return;
                }

                quadRenderer.material = MaterialWithTexture(Decode(content));


            }
        }
        catch (Exception e)
        {
            Debug.LogError("Errore durante la chiamata API: " + e.Message);
        }
    }
}
