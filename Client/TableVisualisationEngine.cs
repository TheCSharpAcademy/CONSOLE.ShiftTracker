using ConsoleTableExt;

using API.Models;

namespace Client
{
    internal class TableVisualisationEngine
    {
        private readonly List<List<object>> TableData = new();

        internal void Print()
        {
            ConsoleTableBuilder
                .From(TableData)
                .WithTitle("ShiftLogger-App", ConsoleColor.Yellow, ConsoleColor.Black)
                .WithColumn("ID", "Start", "End", "Pay", "Minutes", "Location")
                .ExportAndWriteLine();
        }

        internal void Add(List<Shift> list)
        {
            foreach (Shift entity in list)
            {
                TableData.Add(
                    new List<object>
                    {
                        entity.ShiftID,
                        entity.Start,
                        entity.End,
                        entity.Pay,
                        entity.Minutes,
                        entity.Location
                    }
                );
            }
        }

        internal void Clear()
        {
            TableData.Clear();
        }
    }
}
