using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Drag and drop this object on a Unity Text Mesh for which you want to use a CSV localization key
 * */

[RequireComponent(typeof(TextMesh))]
public class LocalizationTextMesh : MonoBehaviour
{
    private TextMesh textToLocalize;
    public string localizationKey = "GREETING";

    private void OnEnable()
    {
        textToLocalize = GetComponent<TextMesh>();
        textToLocalize.text = Localization.GetTranslationByKey(localizationKey);
        Localization.OnLanguageChanged += ChangeText;
    }

    private void OnDisable()
    {
        Localization.OnLanguageChanged -= ChangeText;
    }

    void ChangeText(int index)
    {
        textToLocalize.text = Localization.GetTranslationByKey(localizationKey);
    }
}

