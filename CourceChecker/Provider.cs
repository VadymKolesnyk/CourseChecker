using CourceChecker.Models;
using System.Data;
using System.Data.OleDb;

namespace CourceChecker;
internal class Provider
{
    private readonly string _splitPath;
    private readonly string _oldPath;

    public Provider(string splitPath, string oldPath)
    {
        _splitPath = splitPath;
        _oldPath = oldPath;
    }

    public Split GetLastSplit()
    {
        using var conn = new OleDbConnection($"Provider=VFPOLEDB.1;Data Source={_splitPath}");
        conn.Open();

        var query = $"SELECT TOP 1 * FROM {_splitPath} ORDER BY npp DESC";

        var adapter = new OleDbDataAdapter(query, conn);

        var dataTable = new DataTable();

        adapter.Fill(dataTable);

        return dataTable.Rows[0].ToObject<Split>();

    }

    public Split GetSplitById(int id)
    {
        using var conn = new OleDbConnection($"Provider=VFPOLEDB.1;Data Source={_splitPath}");
        conn.Open();

        var query = $"SELECT * FROM {_splitPath} WHERE NPP = {id}";

        var adapter = new OleDbDataAdapter(query, conn);

        var dataTable = new DataTable();

        adapter.Fill(dataTable);

        return dataTable.Rows[0].ToObject<Split>();
    }

    public List<Split> GetAllSplits()
    {
        using var conn = new OleDbConnection($"Provider=VFPOLEDB.1;Data Source={_splitPath}");
        conn.Open();

        var query = $"SELECT * FROM {_splitPath} ORDER BY npp ASC";

        using var adapter = new OleDbDataAdapter(query, conn);

        using var dataTable = new DataTable();

        adapter.Fill(dataTable);

        return dataTable.Rows.Cast<DataRow>().Select(x => x.ToObject<Split>()).ToList();

    }

    public void UpdateSplit(int id, bool isDsq, string error)
    {
        using var conn = new OleDbConnection($"Provider=VFPOLEDB.1;Data Source={_splitPath}");
        conn.Open();

        var comm = $"UPDATE {_splitPath} SET lrez = {(isDsq ? 1 : 0)}, erkp = '{error}' WHERE NPP = {id}";

        using var cmd = new OleDbCommand(comm, conn);

        cmd.ExecuteNonQuery();
    }

    public void UpdateOldRecord(int nomer, bool isDsq)
    {
        using var conn = new OleDbConnection($"Provider=VFPOLEDB.1;Data Source={_oldPath}");
        conn.Open();

        var comm = $"UPDATE {_oldPath} SET u_dal = '{(isDsq ? "MP" : "")}' WHERE nomer = {nomer}";

        using var cmd = new OleDbCommand(comm, conn);

        cmd.ExecuteNonQuery();
    }

}
