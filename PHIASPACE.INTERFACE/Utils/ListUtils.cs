using System.Data;

namespace PHIASPACE.INTERFACE.Utils;
public static class ListUtil
{
    public static List<List<T>> SplitListNormal<T>(this List<T> me, int size = 50)
    {
        var list = new List<List<T>>();
        for (int i = 0; i < me.Count; i += size)
            list.Add(me.GetRange(i, Math.Min(size, me.Count - i)));
        return list;
    }
    //using linq
    public static List<List<T>> SplitList<T>(this List<T> source, int chunkSize = 50)
    {
        return source
            .Select((item, index) => new { Item = item, Index = index })
            .GroupBy(x => x.Index / chunkSize)
            .Select(group => group.Select(x => x.Item).ToList())
            .ToList();
    }
    public static DataTable ConvertListToDataTable<T>(List<T> items)
    {
        DataTable table = new DataTable();
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            table.Columns.Add(property.Name, property.PropertyType);
        }
        foreach (var item in items)
        {
            var row = table.NewRow();
            foreach (var property in properties)
            {
                row[property.Name] = property.GetValue(item);
            }
            table.Rows.Add(row);
        }
        return table;
    }
}
