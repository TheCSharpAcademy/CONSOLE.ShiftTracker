using System.Diagnostics.CodeAnalysis;
using ConsoleTableExt;

namespace ShiftTracker.Ui
{
    internal class TableVisualisationEngine
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T : class
        {
            if (tableName == null)
                tableName = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn(tableName)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}