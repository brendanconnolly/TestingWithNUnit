using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestfulBooker.UI.Data;

namespace TestingWithNUnit.Tests
{
    public class TestData
    {
        public static List<TestCaseData> RoomsFromJsonFile()
        {
            // read and parse file
            var filePath = "rooms.json";
            var contents= File.ReadAllText(filePath);
            var rooms = JsonConvert.DeserializeObject<List<ExternalRoomData>>(contents);
            var testCases = new List<TestCaseData>();
            //build up the test case data
            foreach (var test in rooms)
            {
               var testCase= new TestCaseData(test.RoomData)
                    .SetName(test.TestName)
                    .SetDescription(test.Description);
               
               test.Categories.ForEach(cat=> testCase.SetCategory(cat));
               
               if (test.IsExplicit){testCase.Explicit();}
               if (test.IsIgnored){ testCase.Ignore(test.IgnoreReason);}
               
               testCases.Add(testCase);
            };
            return testCases;

        }
        
        public static string[] CurrencyStrings()
        {
            return new[]
            {
                "88",
                "$88",
                "88.00",
                "88,00",
                "88 00",
            };
        }
        
        public static object[] RoomInfo()
        {
            return new object[]
            {
                new object[]{"1","100", RoomType.Double},
                new object[]{"2","200", RoomType.Family},
            };
        }
        
        public static TestCaseData[] RoomTestCaseData()
        {
            
            return new TestCaseData[]
            {
                new TestCaseData("1","100", RoomType.Double)
                    .SetName("double room").SetDescription("description"),
                
                new TestCaseData("2","200", RoomType.Family)
                    .SetCategory("Family").Explicit()
            };
        }
    }
}