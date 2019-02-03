using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    private TextAsset localizationText;

    public struct LocEntry {

        public string[] translations;

        public LocEntry(string[] supportedLanguages)
        {
            translations = supportedLanguages;
        }
    }

    private string translationSeparatorChar = "*";
    private static string locPrefName = "Language"; // the string for the Unity PlayerPref that will store type of language
    private static Dictionary<string, LocEntry> localization;

    public delegate void LanguageChanged();
    public static event LanguageChanged OnLanguageChanged;

    void Awake()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(locPrefName))) 
        {
            PlayerPrefs.SetString(locPrefName, "English");
        }

        string csvFileName = "Localization";
        localizationText = (TextAsset)Resources.Load(csvFileName);
        try
        {
            //Debug.Log(localizationText.text);
        }
        catch
        {
            Debug.LogError("Null reference exception on localizationText; is the csv file open and locked so it can't be used by Unity?");
        }

        if (string.IsNullOrEmpty(localizationText.text)) // get null ref on this if there is a /n at end of csv file
        {
            string nullContentError = "Failed to find any localization data";
            Debug.Log(nullContentError);
            return;
        }

        // if data is retrieved, split up text from CSV file
        localization = new Dictionary<string, LocEntry>();
        //Debug.Log(fileText);
        string[] lines = localizationText.text.Split("\n" [0] );
 
        for (int i = 0; i < lines.Length; i++) // split method grabs an extra empty line so minus 1 to loop
        {
            //Debug.Log(lines[i]);
            // this is a hard-coded dependency on number of languages, so need to modify this when adding new languages to the LocEntry struct
            string[] keyAndTranslations = lines[i].Split(translationSeparatorChar [0]);

            //Debug.Log(keyAndTranslations.Length);
            string key = keyAndTranslations[0];

            // if block below throws an exception, check the Localization.csv file to ensure there is not empty line after the translations (that seems to happen sometimes on saving/export the csv)
            List<string> supportedLanguageTranslations = new List<string>();
            for (int t = 1; t < keyAndTranslations.Length; t++)
            {
                supportedLanguageTranslations.Add(keyAndTranslations[t]);
            }
            LocEntry locEntry = new LocEntry(supportedLanguageTranslations.ToArray());

            /*
            string words = "";
            for (int f = 0; f < locEntry.translations.Length; f++)
            {
                words += locEntry.translations[f] + ",";
            }
            Debug.Log(locEntry.translations.Length.ToString());
            Debug.Log(words);
            */
            localization.Add(key, locEntry);
        }
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

    public static string GetTranslationByKey(string key)
    {
        LocEntry entry = new LocEntry();

        if (localization == null)
        {

            Debug.Log("Localization dictionary is null; ensure Localization.cs is before LocalizationText.cs in Script Execution Order");
            return "LOC_ERROR_0";
        }
        localization.TryGetValue(key, out entry);

        if (entry.translations == null)
        {
            return "LOC_ERROR_1";
        }
        //Debug.Log("for key - " + key + ", player prefs is - " + PlayerPrefs.GetString("Language"));

        // trimming and normalizing the player pref fixes the bug with loc not working for the last dropdown input o_O
        switch (PlayerPrefs.GetString(locPrefName).Trim().Normalize())
        {
            case "English":
                return entry.translations[0]; // english
            case "Francés":
                return entry.translations[1]; // french
            case "Español":
                return entry.translations[2]; // spanish
            case "Portugués":
                return entry.translations[3]; // portuguese
            case "Deutsche":
                return entry.translations[4]; // german
            case "中文":
                return entry.translations[5]; // chinese simplified
            case "日本人":
                return entry.translations[6]; // japanese
            case "한국어":
                return entry.translations[7]; // korean
            case "Indonesia":
                return entry.translations[8]; // indonesian
            case "हिंदी":
                return entry.translations[9]; // hindi
            case "Pусский":
                return entry.translations[10]; // russian
            case "عربى":
                return entry.translations[11]; // arabic
            default:
                return entry.translations[0];
        }
    }

    public static bool TrySetLanguageByIndex(int languageIndex)
    {
        string languageToSet = "English"; // default to English if index unsupported
        bool success;

        LocEntry languageEntry = GetLocEntryForKey("LANGUAGE");
        if (languageIndex >= languageEntry.translations.Length)
        {
            Debug.Log("Unsuccessful cannot set language; using default");
            success = false;
        }
        else
        {
            success = true;
        }
        languageToSet = languageEntry.translations[languageIndex];
        PlayerPrefs.SetString(locPrefName, languageToSet);
        Debug.Log("Language set to " + PlayerPrefs.GetString(locPrefName));

        if (OnLanguageChanged != null) {
            OnLanguageChanged();
            //Debug.Log("Broadcast change to Localization Text objects");
        }
        else
        {
            Debug.Log("Failed to broadcast language change; are there any active LocalizationTexts in the scene?");
        }
        return success;
    }

    public static int GetIndexFromPlayerPref()
    {
        string playerPrefLanguage = PlayerPrefs.GetString(locPrefName);
        LocEntry languageEntry = GetLocEntryForKey("LANGUAGE");
        for (int i = 0; i < languageEntry.translations.Length; i++)
        {
            if (languageEntry.translations[i] == playerPrefLanguage)
            {
                return i;
            }
        }
        Debug.Log("Failed to get index from player pref");
        return -1;
    }
}
