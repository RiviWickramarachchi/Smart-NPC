using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonReader : MonoBehaviour
{
    public TextAsset textJson;
    public KeyList keyList = new KeyList();
    public string openAIKeyValue;
    public static JsonReader Instance {get; private set;}

    public void Awake() {
        if(Instance !=null) {
            Destroy(this.gameObject);
            return;
        }
        else {
            Instance = this;
            keyList = JsonUtility.FromJson<KeyList>(textJson.text);
            Debug.Log("Data Initialized");
            for (int i = 0; i < keyList.key.Length; i++)
            {
                if (keyList.key[i].keyName == "openAI_key")
                {
                    openAIKeyValue = keyList.key[i].keyValue;
                }
            }
        }
    }

    [System.Serializable]
    public class Key {
        public string keyName;
        public string keyValue;
    }

    [System.Serializable]
    public class KeyList {
        public Key[] key;
    }

}
