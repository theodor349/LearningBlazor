using FrontEndAPIHelper.Endpoints;
using FrontEndAPIHelper.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Pages.WeekView
{
    public class WeekPage : ComponentBase
    {
        protected DateTime startDate = new DateTime(2020, 10, 10);
        protected int startingHour = 5;
        protected Color currentColor = Color.White;

        protected List<EntryModel> entries;
        [Inject] IEntryAccess _ea { get; set; }

        #region Init
        protected override async Task OnInitializedAsync()
        {
            if (entries == null)
                await Init();
        }

        private async Task Init()
        {
            var start = DateTime.Now;
            startDate = GetMonday(start);
        }
        #endregion

        #region Component Functions
        protected void OnValueChange(int x, int y, string v)
        {
            var date = GetDate(x, y);
            UpdateValue(date, v);
        }

        protected string GetValue(int x, int y)
        {
            var entries = GetEntries(x, y);
            if (entries.Count == 0)
                return "";
            else
                return string.Join(" + ", entries.ConvertAll(x => x.Activity.Name));
        }

        protected Color GetColor(int x, int y)
        {
            var entries = GetEntries(x, y);
            if (entries.Count > 0)
            {
                currentColor = GetPrimaryColor(entries);
            }
            return currentColor;
        }
        #endregion

        #region Helper
        private async Task UpdateValue(DateTime date, string v)
        {
            await _ea.UpdateEntry(date, v);
            await Init();
            StateHasChanged();
        }

        private Color GetPrimaryColor(List<EntryModel> entries)
        {
            var min = entries.Min(x => x.Activity?.Category?.Type?.PriorityLevel);
            EntryModel entry = null;
            if (min is not null)
                entry = entries.Where(x => x.Activity?.Category?.Type?.PriorityLevel == min).FirstOrDefault();

            return entry is null ? Color.White : entry.Activity.Category.Type.Color;
        }

        private List<EntryModel> GetEntries(int x, int y)
        {
            var d = GetDate(x, y);
            return entries.Where(x => x.EntryTime.Date.Equals(d)).ToList();
        }

        private DateTime GetDate(int x, int y)
        {
            var ts = new TimeSpan(x * 24 + startingHour, y * 15, 0);
            var res = startDate + ts;
            return res;
        }

        private DateTime GetMonday(DateTime date)
        {
            date = date.AddDays(-(date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek - 1));
            return new DateTime(date.Year, date.Month, date.Day);
        }
        #endregion
    }
}
