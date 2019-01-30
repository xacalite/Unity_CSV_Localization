using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationTools : MonoBehaviour
{
    public void ReplaceCharInLocCSV(string oldSeparator, string newSeperator)
    {
        Debug.Log("replace character in file");
        string csvFilePath = "Assets\\Resources\\Localization.csv";
        string text = File.ReadAllText(csvFilePath);
        text = text.Replace(oldSeparator, newSeperator);
        File.WriteAllText(csvFilePath, text);
        Debug.Log(text);
        
    }
}
