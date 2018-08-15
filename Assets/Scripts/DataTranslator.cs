using UnityEngine;
// this class was maked for support the translation between data in game format and database format
public class DataTranslator  {

    public static int GetInfo(string s, string tag) // it takes data and tag and return the value of the tag's stat
    {
        string[] data = s.Split('/');
        foreach (string str in data)
            if (str.StartsWith(tag))
            {
                string[] str1 = str.Split(' ');
                return int.Parse(str1[1]);
            }
        Debug.Log("tag " + tag + " doesn't exist");
        return -1;
    }

    public static string SetData(int kills, int deaths) // it return string like data that will be stored
    {
        return "[KILLS] " + kills + "/[DEATHS] " + deaths;
    }

}
