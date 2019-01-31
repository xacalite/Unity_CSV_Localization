using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Drag and drop this object on a Unity UI Text object for which you want to use a CSV localization key
 * */

[RequireComponent(typeof(Text))]
public class LocalizationText : MonoBehaviour
{
    private Text textToLocalize;
    public string localizationKey = "GREETING";

    private void OnEnable()
    {
        textToLocalize = GetComponent<Text>();
        textToLocalize.text = Localization.GetTranslationByKey(localizationKey);
        Localization.OnLanguageChanged += ChangeText;
    }

    private void OnDisable()
    {
        Localization.OnLanguageChanged -= ChangeText;
    }

    void ChangeText()
    {
        textToLocalize.text = Localization.GetTranslationByKey(localizationKey);
    }

    public void SetLocalizationKey(string newKey)
    {
        localizationKey = newKey;
        textToLocalize.text = Localization.GetTranslationByKey(localizationKey);
    }
}

