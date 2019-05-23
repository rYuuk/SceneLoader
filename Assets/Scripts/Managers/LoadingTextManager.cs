using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

namespace SceneLoader
{
    [System.Serializable]
    public class LoadingText
    {
        /// <summary>
        /// Unique id of text.
        /// </summary>
        public int id;
        /// <summary>
        /// Tag of text - UI, Gameplay, Mechanics.
        /// </summary>
        public string tag;
        /// <summary>
        /// Weight defines the priority of text, heigher the weight heigher priority.
        /// </summary>
        public int weight;
        /// <summary>
        /// Actual lodaing text.
        /// </summary>
        public string text;

        public override string ToString()
        {
            return string.Format("id: {0}, tag: {1}, weight: {2}, text: {3}", id, tag, tag, text);
        }
    }

    public class LoadingTextManager
    {
        /// <summary>
        /// Class to parse response from GET request.
        /// </summary>
        [System.Serializable]
        private class Response
        {
            public List<LoadingText> rows;
        }

        private const string LOADING_TEXT_URL = "http://gsx2json.com/api?id=1WzopQHboEwVm_kaqzgprjL80Anr2plw_R1HLWxVBSdU&sheet=1&columns=false";
        private const string LOADING_TEXT_FILENAME = "LoadingText";

        private UnityWebRequest _request;

        public List<LoadingText> loadingTexts { get; private set; }

        public LoadingTextManager()
        {
            Fetch();
        }

        /// <summary>
        /// Fetches loading text from google sheets by doing a GET request.
        /// </summary>
        private void Fetch()
        {
            loadingTexts = new List<LoadingText>();
            _request = UnityWebRequest.Get(LOADING_TEXT_URL);
            Debug.Log("#LoadingText, Sending request to url: " + LOADING_TEXT_URL);
            _request.SendWebRequest().completed += OnResponseReceived;
        }


        /// <summary>
        /// Adds new loading text to list, incase required for on the fly adding of loading text.
        /// </summary>
        public void Add(string tag, int weight, string text)
        {
            LoadingText loadingText = new LoadingText
            {
                tag = tag,
                weight = weight,
                text = text
            };

            loadingText.id = loadingTexts.Count + 1;
        }

        private void OnResponseReceived(AsyncOperation operation)
        {
            if (_request.isHttpError || _request.isNetworkError)
            {
                //If request fails, load the cached json from persistent data path.
                string json = LoadFromFile();

                //Elseif no cached json, load from resources.
                if (string.IsNullOrEmpty(json))
                    json = Resources.Load<TextAsset>(LOADING_TEXT_FILENAME).text;

                Response response = JsonUtility.FromJson<Response>(json);
                loadingTexts = response.rows;
            }
            else
            {
                string json = _request.downloadHandler.text;
                Debug.Log("#LoadingText, Received response: " + json);
                if (!string.IsNullOrEmpty(json))
                {
                    //Cache the json to peristent data path.
                    SaveToFile(json);
                    Response response = JsonUtility.FromJson<Response>(json);
                    loadingTexts = response.rows;
                }
            }
        }

        //Saves json to peristent data path
        private void SaveToFile(string json)
        {
            string filePath = Application.persistentDataPath + "/" + LOADING_TEXT_FILENAME + ".json";
            Debug.Log("#LoadingText, Saving json to path: " + filePath);
            File.WriteAllText(filePath, json);
        }

        //Loads json from peristent data path
        private string LoadFromFile()
        {
            string filePath = Application.persistentDataPath + "/" + LOADING_TEXT_FILENAME + ".json";
            Debug.Log("#LoadingText, Loading json from path: " + filePath);
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);

            return "";
        }

    }
}