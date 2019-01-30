using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationTools : MonoBehaviour
{

    public string oldSeparator = ",";
    public string newSeparator = "*";

    public void ReplaceCharInLocCSV()
    {
        Debug.Log("replace character in file");
        string csvFilePath = "Assets\\Resources\\Localization.csv";
        string text = File.ReadAllText(csvFilePath);
        text = text.Replace(oldSeparator, newSeparator);
        File.WriteAllText(csvFilePath, text);
        Debug.Log(text);
        
    }
}
