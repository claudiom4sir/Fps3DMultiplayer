// this class was maked for support the translation between data from the game and data from database
public class DataTranslator {

    public static string[] ForGetting(string data) // this is a support method used for parse the data receved by database
    {
        string[] info = data.Split('/');
        return info;
    }

    public static string ForSetting(string[] data) // it is used for translatting from the game data format to database data format
    {
        return null;
    }

}
