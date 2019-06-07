using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Drag and drop this object on a Unity Text Mesh for which you want to use a CSV localization key
 * */

[RequireComponent(typeof(TextMeshPro))]
public class LocalizationTextMeshPro : MonoBehaviour
{
    private TextMeshPro textToLocalize;
    public string localizationKey = "GREETING";

    private void OnEnable()
    {
        textToLocalize = GetComponent<TextMeshPro>();
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

