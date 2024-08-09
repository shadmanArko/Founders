using System.Text.Json;

namespace SendMassage
{
    public class Class1
    {

        public string SendTheMassage()
        {
            return "gg";
            // return ReadMessageFromJson(
            //     @"C:\Users\User\Documents\_Work_Dont Touch\_Unity Projects\ProjectTime\Mods\01\Code\message.Json");
        }
    
        // static string ReadMessageFromJson(string jsonFilePath)
        // {
        //     try
        //     {
        //         // Read the JSON file
        //         string jsonContent = File.ReadAllText(jsonFilePath);
        //
        //         // Deserialize the JSON content
        //         JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
        //
        //         // Access the message property
        //         if (jsonDocument.RootElement.TryGetProperty("message", out JsonElement messageElement))
        //         {
        //             return messageElement.GetString();
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         //Console.WriteLine("An error occurred while reading the JSON file: " + ex.Message);
        //     }
        //
        //     return string.Empty;
        // }
    }
}