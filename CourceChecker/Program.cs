using CourceChecker;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

const string basePath = @"D:\orientir\event\odesa_zumonyi_kubok_1\D_1";

var groups = new Dictionary<string, string>
{
    ["Ч21Е"] = "<70 44 53 54 38 65 33 37 59 43 42 [12 48 56 40 45 69 39 49 41 63 47 31 35] 100>"
};

var app = new App(basePath, new TimeSpan(0, 0, 1), groups);

app.Start(false);
