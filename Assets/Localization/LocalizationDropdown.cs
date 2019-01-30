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
        dropdown.onValueChanged.AddListener(OnLanguageIndexChanged);
    }

    private void OnLanguageIndexChanged(int value)
    {
        bool success = Localization.TrySetLanguageByIndex(value);
        if (success) { return; }
        else
        {
            Debug.LogError("Dropdwon value does not correspond to a supported language");
            return;
        }
    }

    private void PopulateOptions(Localization.LocEntry le)
    {
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData(le.languages[0]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[1]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[2]));
    }
}
