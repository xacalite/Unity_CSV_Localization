using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Attach to a Unity UI dropdown object to switch between languages
 * */
[RequireComponent (typeof(Dropdown))]
public class LocalizationDropdown : MonoBehaviour
{
    Dropdown dropdown;

    void OnEnable()
    {
        dropdown = GetComponent<Dropdown>();
        Localization.LocEntry locEntry = Localization.GetLocEntryForKey("LANGUAGE");
        PopulateOptions(locEntry);
        dropdown.value = Localization.GetIndexFromPlayerPref();
        dropdown.onValueChanged.AddListener(OnLanguageIndexChanged);
    }

    private void OnLanguageIndexChanged(int value)
    {
        Debug.Log("Language index changed via dropdown");
        bool success = Localization.TrySetLanguageByIndex(value);
        if (success) { return; }
        else
        {
            Debug.LogError("Dropdown value does not correspond to a supported language");
            return;
        }
    }

    private void PopulateOptions(Localization.LocEntry le)
    {
        dropdown.options.Clear();

        for (int i = 0; i < le.translations.Length; i++)
        {
            dropdown.options.Add(new Dropdown.OptionData(le.translations[i]));
        }
    }
}
