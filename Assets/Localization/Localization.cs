using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Localization : MonoBehaviour
{
    struct LocEntry {
        string english;
        string french;
        string spanish;

        public LocEntry(string l1, string l2, string l3)
        {
            english = l1;
            french = l2;
            spanish = l3;
        }
    }

    private Dictionary<string, LocEntry> localization;
    
    // Start is called before the first frame update
    void Awake()
    {
        string fileText = "";

        // try getting the localization data
        try
        {
            fileText = File.ReadAllText(GetCSVFilePath());
        }
        catch
        {
            Debug.Log("Error while attempting to load localization CSV");
            return;
        }

        if (string.IsNullOrEmpty(fileText))
        {
            Debug.Log("Failed to find any localization data");
            return;
        }

        // if data is retrieved, split up text from CSV file
        localization = new Dictionary<string, LocEntry>();
        //Debug.Log(fileText);
        string[] lines = fileText.Split(new[] { Environment.NewLine },
    StringSplitOptions.None);


        for (int i = 0; i < lines.Length - 1; i++) // split method grabs an extra empty line so minus 1 to loop
        {
            Debug.Log(lines[i]);
            // this is a hard-coded dependency on number of languages, so need to modify this when adding new languages to the LocEntry struct
            string[] keyAndTranslations = lines[i].Split("," [0]);

            for (int f = 0; f < keyAndTranslations.Length; f++)
            {
                Debug.Log(keyAndTranslations[f]);
            }

            //Debug.Log(keyAndTranslations.Length);
            string key = keyAndTranslations[0];
            LocEntry locEntry = new LocEntry(keyAndTranslations[1], keyAndTranslations[2], keyAndTranslations[3]); 
            localization.Add(key, locEntry);
        }
        Debug.Log(localization.ToString());
    }

    private string GetCSVFilePath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Localization/" + "Localization.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Localization.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Localization.csv";
#else
        return Application.dataPath +"/"+"Saved_Inventory.csv";
#endif
    }
}
