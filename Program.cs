using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

Console.WriteLine("Hello, World!");
string idd = "";
string answer = "";
string newId = "";
string room = "";
string temp = "";
string days = "";
Console.WriteLine("Here are all the temp measurements");
//Opretter en ny HttpKlient der kan kommunikere over HTTP, med en using () {}
using (var client = new HttpClient())
{
    //Får tilbage med client.Get metoden, husk await da det er async
    HttpResponseMessage response = await client.GetAsync("http://localhost:5254/api/temps");
    //Sørger for at body content bliver lavet om til en string og udskriver den
    string responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);
    Console.WriteLine("Would you like to continue? Y/N");
    answer = Console.ReadLine();
    if (answer == "y")
    {
        Console.WriteLine("Which Id would you like to search for?");
        idd = Console.ReadLine();
        Int32.Parse(idd);
        //Laver et nyt httpGet men bare hvor den kalder GetById route fra controlleren
        HttpResponseMessage response2 = await client.GetAsync("http://localhost:5254/api/temps/" + idd);
        string responseBody2 = await response2.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody2);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("Would you like to add a new? Yes/No");
        answer = Console.ReadLine();
        if (answer == "yes")
        {
            Console.Write("Enter id: ");
            newId = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter room no: ");
            room = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter temp_C: ");
            temp = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter day: ");
            days = Console.ReadLine();
            Console.WriteLine("Enter yes to send");
            answer = Console.ReadLine();
            int id1 = int.Parse(newId);
            int tempo = int.Parse(temp);
            var temperature = new
            {
                id = id1,
                roomNo = room,
                temp_C = tempo,
                day = days
            };
            if (answer == "yes") 
            {
                //Json Serialiserer variablet/objektet temperature
                string json = JsonSerializer.Serialize(temperature);
                Console.WriteLine(json);
                //sætter en ny stringcontent variabel med Json-stringen, indkoder det med UTF8
                //Og i type application/json
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine(content);
                //Kalder postasync fra klient -- Husk async og korrekt routing til Post eks. /add
                HttpResponseMessage response3 = await client.PostAsync("http://localhost:5254/api/temps/add", content);
                Console.WriteLine(response3);
            }
            else
            {
                Console.WriteLine("Close");
                Console.ReadKey();
            }



        }
        Console.WriteLine("Press any key to close..");
    }



    Console.ReadKey();
}
