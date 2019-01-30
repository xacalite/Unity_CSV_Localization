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
            Debug.LogError("Dropdown value does not correspond to a supported language");
            return;
        }
    }

    private void PopulateOptions(Localization.LocEntry le)
    {
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData(le.languages[0]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[1]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[2]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[3]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[4]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[5]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[6]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[7]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[8]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[9]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[10]));
        dropdown.options.Add(new Dropdown.OptionData(le.languages[11]));
    }
}
