using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CourceChecker.Models;
[DebuggerDisplay("{Nomer} {PointsString}")]
record Split
{
    [Display(Name = "npp")]
    public int Id { get; set; }
    [Display(Name = "nomer")]
    public int Nomer { get; set; }
    [Display(Name = "fam")]
    public string SportsmanName { get; set; }
    [Display(Name = "chip")]
    public string Chip { get; set; }
    [Display(Name = "lrez")]
    public bool IsDsq { get; set; }
    [Display(Name = "erkp")]
    public string Error { get; set; }
    [Display(Name = "grup")]
    public string Group { get; set; }
    [Display(Name = "rsplit")]
    private string CpOrderStirng { get; set; }

    public IEnumerable<string> Points => CpOrderStirng.Trim(';').Split(';').Where((x, i) => i % 2 == 0);
    public string PointsString => string.Join(' ', Points);

}
