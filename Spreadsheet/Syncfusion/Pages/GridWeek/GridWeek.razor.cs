using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.DatabaseModel;

namespace Syncfusion.Pages.GridWeek
{
    public delegate void OnRefresh();

    public class GridWeekPage : ComponentBase
    {
        [Parameter] public DateTime CurrentDate {
            get => _currDate;
            set 
            {
                if (_currDate == value) return;
                _currDate = value;
                UpdateValues();
            } 
        }
        private DateTime _currDate;
        protected event OnRefresh OnRefresh;
        protected SfGrid<Row> Grid;

        public List<Row> Rows;
        public List<EntryPoint> Entries;
        public Cell[,] Cells = new Cell[7, 96];
        public List<CategoryData> Categories;

        protected override async Task OnInitializedAsync()
        {
            await UpdateValues();
        }

        public async Task UpdateValues()
        {
            var monday = GetMonday(CurrentDate);
            Entries = await Model.GetEntryPoints(monday, monday.AddDays(7));
            ConstrucGrid();
            PopulateGrid(monday);
            await SetCatrogires();
            OnRefresh.Invoke();
        }

        protected DateTime GetMonday(DateTime date)
        {
            var day = (int)date.DayOfWeek;
            if (day == 0)
                day += 6;
            else
                day--;

            return (new DateTime(date.Year, date.Month, date.Day)).AddDays(-day);
        }

        private async Task SetCatrogires()
        {
            Categories = await Model.GetCategories();
        }

        private void ConstrucGrid()
        {
            Rows = new List<Row>();
            for (int y = 0; y < 96; y++)
            {
                Rows.Add(new Row(y));
            }
        }

        private void PopulateGrid(DateTime startDate)
        {
            var lastEntry = Entries.GetEnumerator();
            lastEntry.MoveNext();
            var currentColor = Color.White;
            var currentDate = startDate;

            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 96; y++)
                {
                    var name = "";
                    var cell = new Cell();
                    if(lastEntry.Current?.Date == currentDate)
                    {
                        var entryPoint = lastEntry.Current;
                        name = entryPoint.Name;
                        currentColor = entryPoint.Color;
                        lastEntry.MoveNext();
                        cell.Entry = entryPoint;
                    }
                    cell.Color = currentColor;
                    Cells[x, y] = cell;
                    SetIndex(x, y, name);
                    currentDate = currentDate.AddMinutes(15);
                }
            }
        }

        private void SetIndex(int x, int y, string name)
        {
            Rows[y].SetIndex(x, name);
        }

        public DateTime IndexToDate(int x, int y)
        {
            return CurrentDate.AddDays(x).AddMinutes(y * 15);
        }
    }

    public class Row
    {
        public int RenderColoum { get; set; } = 0;
        public int Y { get; set; }
        public DateTime Time { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }

        public Row()
        {

        }

        public Row(int y)
        {
            Time = new DateTime().AddMinutes(y * 15);
            Y = y;
        }

        public void SetIndex(int x, string name)
        {
            switch (x)
            {
                case 0:
                    Monday = name;
                    break;
                case 1:
                    Tuesday = name;
                    break;
                case 2:
                    Wednesday = name;
                    break;
                case 3:
                    Thursday = name;
                    break;
                case 4:
                    Friday = name;
                    break;
                case 5:
                    Saturday = name;
                    break;
                case 6:
                    Sunday = name;
                    break;
                default:
                    break;
            }
        }
    }

    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; } = Color.White;
        public EntryPoint Entry { get; set; }
    }

    public class EntryDialogData
    {
        public EntryDialogData()
        {
            Entry = new EntryPoint()
            {
                Entries = new List<Entry>()
                {
                    new Entry(){ Activity = "cummy" }
                },
                Date = new DateTime(),
            };
        }

        public EntryPoint Entry { get; set; }
    }
}
