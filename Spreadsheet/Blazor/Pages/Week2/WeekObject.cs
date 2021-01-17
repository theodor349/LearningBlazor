using FrontEndAPIHelper.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Pages.Week2
{
    public class WeekObject
    {
        public EntryData[,] Week { get; set; }

        public void GenerateWeek(List<EntryModel> entries, DateTime startDate)
        {
            CreateEntryData(entries, startDate);
            SetColor();
        }

        private void SetColor()
        {
            var defaultColor = Color.White;
            var currentColor = defaultColor;
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 96; y++)
                {
                    currentColor = Week[x, y].SetPrimaryColor(currentColor, defaultColor);
                }
            }
        }

        private void CreateEntryData(List<EntryModel> entries, DateTime startDate)
        {
            Week = new EntryData[7, 96];
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 96; y++)
                {
                    Week[x, y] = new EntryData(startDate.AddDays(x).AddMinutes(y * 15));
                }
            }

            foreach (var entry in entries)
            {
                var coord = GetXYPair(startDate, entry.EntryTime.Date);
                Week[coord.Item1, coord.Item2].Add(entry);
            }
        }

        private Tuple<int, int> GetXYPair(DateTime start, DateTime current)
        {
            var offset = current - start;
            return new Tuple<int, int>(offset.Days, offset.Hours * 4 + (offset.Minutes / 15));
        }
    }

    public class EntryData
    {
        public Color Color { get; set; }
        public string Value => string.Join(" + ", _entries.ConvertAll(x => x.Activity.Name));
        public DateTime Date { get; set; }

        private List<EntryModel> _entries = new List<EntryModel>();

        public EntryData(DateTime date)
        {
            Date = date;
        }

        public EntryData Add(EntryModel entry)
        {
            _entries.Add(entry);
            return this;
        }

        public Color SetPrimaryColor(Color currentColor, Color defaultColor)
        {
            if (_entries.Count > 0)
                Color = GetPrimaryColor(defaultColor);
            else
                Color = currentColor;

            return Color;
        }

        private Color GetPrimaryColor(Color defaultColor)
        {
            var min = _entries.Min(x => x.Activity?.Category?.Type?.PriorityLevel);
            if (min is null)
                return defaultColor;
            else
                return _entries
                    .Where(x => x.Activity?.Category?.Type?.PriorityLevel == min)
                    .FirstOrDefault().Activity.Category.Type.Color;
        }
    }
}
