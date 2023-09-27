using CourceChecker;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

const string splitFileName = @"D:\orientir\event\Kyiv_1409-1809_23_test\D_1\SPLIT.dbf";
const string oldFileName = @"D:\orientir\event\Kyiv_1409-1809_23_test\D_1\OLD.dbf";

var groups = new Dictionary<string, string>
{
    ["Ч21Е"] = "<70 44 53 54 38 65 33 37 59 43 42 [12 48 56 40 45 69 39 49 41 63 47 31 35] 100>"
};

var app = new App(splitFileName, oldFileName, new TimeSpan(0, 0, 1), groups);

app.Start(false);
