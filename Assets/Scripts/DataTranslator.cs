using UnityEngine;
// this class was maked for support the translation between data in game format and database format
public class DataTranslator  {

    public static string GetInfo(string s, string tag)
    {
        string[] data = s.Split('/');
        foreach (string str in data)
            if (str.StartsWith(tag))
            {
                string[] str1 = str.Split(' ');
                return str1[1];
            }
        Debug.Log("tag " + tag + " doesn't exist");
        return "";
    }

    public static string IncreaseStats(string s, string tag) // it is used for translatting from the game data format to database data format
    {
        string[] str = s.Split('/');
        string finalString = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i].StartsWith(tag))
            {
                int x = int.Parse(str[i].Split(' ')[1]) + 1; // x has the value increased by 1
                finalString = finalString + tag + " " + x;
            }
            else
                finalString = finalString + str[i];
            if (i < str.Length - 1)
                finalString = finalString + "/";
        }
        return finalString;
    }

}
