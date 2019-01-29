using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizationText : MonoBehaviour
{
    private Text textToLocalize;
    public string localizationKey = "GREETING";

    private void OnEnable()
    {
        textToLocalize = GetComponent<Text>();
        textToLocalize.text = Localization.GetValueByKey(localizationKey);
    }
}
