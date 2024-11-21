using System.Data;
using Newtonsoft.Json;

namespace PHIASPACE.RTDMS.Utility;

public static class ViewUtility
{
    public static string GetColorByPercentage(string value)
    {
        try
        {
            var count = Convert.ToDouble(value);
            if (count > 0 && count < 100)
                return "yellow";
            else if (count == 100)
                return "green";
            else if (count > 100)
                return "red";
            else
                return "gray";
        }
        catch (Exception)
        {
            return "gray";
        }

    }

    public static List<string[]> GetValues(string id)
    {
        string[] item_names = "AZONE,ASTATE,AVRESULT,AINTM,LHIVF,LSMONTH,AHVRESULT,M703Y,M703M,AHINTCD,AHINTC".Split(',');
        var ex_values = "Continue".Split(',');
        var result = new List<string[]>();
        var values = new List<dynamic>(){new {label = "Household"}};//Utils.GetValueSets().Where(e => e.table_name == id && !item_names.Contains(e.item_name) && !ex_values.Contains(e.value) && e.valueset_type == "normal").ToList();
        var distinct_values = values.Select(e => e.label).Distinct();
        foreach (var value in distinct_values)
        {
            var value_set = values.FirstOrDefault(e => e.label == value);
            var st = new string[] { value_set.id.ToString(), value };
            result.Add(st);
        }

        return result;
    }

    public static string ToJson(this DataSet ds)
    {
        var tables = new Dictionary<string, object>();

        foreach (DataTable table in ds.Tables)
        {
            var rows = table.AsEnumerable().Select(r => r.ItemArray.ToDictionary(table.Columns));
            tables[table.TableName] = rows;
        }

        return JsonConvert.SerializeObject(tables);
    }

    private static Dictionary<string, object> ToDictionary(this object[] itemArray, DataColumnCollection columns)
    {
        var dict = new Dictionary<string, object>();
        for (int i = 0; i < columns.Count; i++)
        {
            dict[columns[i].ColumnName] = itemArray[i];
        }
        return dict;
    }
}