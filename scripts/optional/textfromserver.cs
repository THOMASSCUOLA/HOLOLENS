using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class RestApiTESTO : MonoBehaviour
{
    public TextMeshPro testo2;

    void Start()
    {
        chiamata();
    }

    async void chiamata()
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
                response = await client.GetAsync("/scritta");

                // Leggo il contenuto della risposta
                string content = await response.Content.ReadAsStringAsync();

                // Aggiorno il testo del componente TextMeshProUGUI
                testo2.text = content;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Errore durante la chiamata API: " + e.Message);
        }
    }
}
