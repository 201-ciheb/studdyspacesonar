using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace PHIASPACE.RTDMS.Helper
{
    public static class HtmlHelper
    {
        public static string GetHtmlTable(string[] header, List<string[]> rows)
        {
            var table = "<table><thead><tr>";
            //add the headers to the table
            foreach (var col in header)
            {
                table += string.Format("<th>{0}</th>", col);
            }
            table += "</tr></thead>";
            //start the body
            table += "<tbody>";
            foreach (var row in rows)
            {
                table += "<tr>";

                foreach (var cell in row)
                {
                    table += string.Format("<td>{0}</td>", cell);
                }

                table += "</tr>";

            }

            table += "</tbody></table>";
            return table;
        }

        public static string GetStatusFromString(int value)
        {
            if (value == 1)
            {
                return "Complete";
            }
            else if (value == 0)
            {
                return "Incomplete";
            }
            else
            {
                return "Action Required";
            }
        }

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}