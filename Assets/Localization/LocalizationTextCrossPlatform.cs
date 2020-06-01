using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Drag and drop this object on a Unity UI Text object for which you want to use a CSV localization key
 * */

[RequireComponent(typeof(Text))]
public class LocalizationTextCrossPlatform : MonoBehaviour
{
    private Text textToLocalize;
    public string defaultLocalizationKey = "Default loc text";
    public string iosLocalizationKey = "Default loc text";
    public string androidLocalizationKey = "Default loc text";

    private void OnEnable()
    {
        textToLocalize = GetComponent<Text>();
        SetTextFromLocKey();
        Localization.OnLanguageChanged += ChangeText;
    }

    private void OnDisable()
    {
        Localization.OnLanguageChanged -= ChangeText;
    }

    void ChangeText()
    {
        SetTextFromLocKey();
    }

    private void SetTextFromLocKey()
    {
#if UNITY_IOS
        textToLocalize.text = Localization.GetTranslationByKey(iosLocalizationKey);
#elif UNITY_ANDROID
        textToLocalize.text = Localization.GetTranslationByKey(androidLocalizationKey);
#else
        textToLocalize.text = Localization.GetTranslationByKey(defaultLocalizationKey);
#endif
    }
}

