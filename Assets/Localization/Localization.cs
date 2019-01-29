using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    public Text logText;
    private TextAsset localizationText;

    struct LocEntry {
        public string english, french, spanish;

        public LocEntry(string l1, string l2, string l3)
        {
            english = l1;
            french = l2;
            spanish = l3;
        }
    }

    private static string locPrefName = "Language"; // the string for the Unity PlayerPref that will store type of language
    private static Dictionary<string, LocEntry> localization;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(locPrefName))) 
        {
            PlayerPrefs.SetString(locPrefName, "English");
        }

        string csvPath = "Localization";
        localizationText = (TextAsset)Resources.Load(csvPath);
        Debug.Log(localizationText.text);

        if (string.IsNullOrEmpty(localizationText.text))
        {
            string nullContentError = "Failed to find any localization data";
            Debug.Log(nullContentError);
            logText.text += "nullContentError";
            return;
        }

        // if data is retrieved, split up text from CSV file
        localization = new Dictionary<string, LocEntry>();
        //Debug.Log(fileText);
        string[] lines = localizationText.text.Split(new[] { Environment.NewLine },
                                        StringSplitOptions.None);

        for (int i = 0; i < lines.Length; i++) // split method grabs an extra empty line so minus 1 to loop
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

        logText.text += "Localization dictionary loaded from CSV";
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

    public static void SetLanguage(string language)
    {
        PlayerPrefs.SetString(locPrefName, language);
    }


    public static string GetValueByKey(string key)
    {
        LocEntry entry = new LocEntry();

        if (localization == null)
        {

            Debug.Log("Localization dictionary is null; ensure Localization.cs is before LocalizationText.cs in Script Execution Order");
            return "LOC_ERROR_0";
        }
        localization.TryGetValue(key, out entry);

        if (string.IsNullOrEmpty(entry.english))
        {
            return "LOC_ERROR_1";
        }

        switch (PlayerPrefs.GetString("Language"))
        {
            case "English":
                return entry.english;
            case "French":
                return entry.french;
            case "Spanish":
                return entry.spanish;
            default:
                return entry.english;
        }
    }
}
