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

    public struct LocEntry {
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

    public delegate void LanguageChanged(int index);
    public static event LanguageChanged OnLanguageChanged;

    // Start is called before the first frame update
    void Awake()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(locPrefName))) 
        {
            PlayerPrefs.SetString(locPrefName, "English");
        }

        string csvFileName = "Localization";
        localizationText = (TextAsset)Resources.Load(csvFileName);
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
            //Debug.Log(lines[i]);
            // this is a hard-coded dependency on number of languages, so need to modify this when adding new languages to the LocEntry struct
            string[] keyAndTranslations = lines[i].Split("," [0]);

            //Debug.Log(keyAndTranslations.Length);
            string key = keyAndTranslations[0];

            // if line below throws an exception, check the Localization.csv file to ensure there is not empty line after the translations (that seems to happen sometimes on saving/export the csv)
            LocEntry locEntry = new LocEntry(keyAndTranslations[1], keyAndTranslations[2], keyAndTranslations[3]);

            localization.Add(key, locEntry);
        }

        logText.text += "Localization dictionary loaded from CSV";
    }

    public static void SetLanguage(string language)
    {
        PlayerPrefs.SetString(locPrefName, language);
    }


    public static LocEntry GetLocEntryForKey(string key)
    {
        LocEntry entry = new LocEntry();
        localization.TryGetValue(key, out entry);
        return entry;
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

    public static bool TrySetLanguageByIndex(int languageIndex)
    {
        string languageToSet = "English"; // default to English if index unsupported
        bool success = true; ; // only unsuccessful if default case is reached
        switch (languageIndex)
        {
            case 0:
                languageToSet = "English";
                break;
            case 1:
                languageToSet = "French";
                break;
            case 2:
                languageToSet = "Spanish";
                break;
            default:
                success = false;
                break;
        }
        PlayerPrefs.SetString(locPrefName, languageToSet);
        Debug.Log("Language set to " + PlayerPrefs.GetString(locPrefName));

        if (OnLanguageChanged != null) {
            OnLanguageChanged(languageIndex);
            Debug.Log("Broadcast change to Localization Text objects");
        }
        else
        {
            Debug.Log("Failed to broadcast language change; are there any active LocalizationTexts in the scene?");
        }

        return success;
    }
}
