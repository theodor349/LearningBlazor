using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Schedule;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;

namespace Syncfusion.Pages.Week
{
    public class WeekPage : ComponentBase
    {
        // Blazor Variables
        protected DateTime CurrentDate = new DateTime(2020, 12, 28);
        protected List<EntryAppointment> Entries { get; set; } = new List<EntryAppointment>()
        {
            new EntryAppointment(){ StartTime = new DateTime(2020, 12, 28, 1, 0, 0), EndTime = new DateTime(2020, 12, 28, 22, 15, 0), Subject = "Test", CategoryId = 1 }
        };
        protected List<Category> Categories { get; set; }

        [Inject] IEntryAccess _ea { get; set; }

        protected async override Task OnInitializedAsync()
        {
            //await GenerateEntries();
        }

        protected async Task GenerateEntries()
        {
            Entries = await GenerateAppointments(CurrentDate);
            Categories = await GetUniqueCatagories();
        }

        protected async Task<List<EntryAppointment>> GenerateAppointments(DateTime date)
        {
            var start = GetMonday(date);
            var end = start.AddDays(7);
            var entries = await _ea.GetEntries(start, end);

            var appointments = new List<EntryAppointment>();
            EntryAppointment appointment = null;
            foreach (var e in entries)
            {
                if (appointment?.StartTime == e.EntryDate)
                {
                    appointment.AddEntry(e);
                }
                else
                {
                    if (appointment is not null)
                    {
                        appointment.EndTime = e.EntryDate;
                        appointments.Add(appointment);
                    }

                    appointment = new EntryAppointment(e);
                }
            }
            appointments.Add(appointment);
            return appointments;
        }

        protected async Task<List<Category>> GetUniqueCatagories()
        {
            var cats = await _ea.GetCategories();
            return cats;
        }

        protected DateTime GetMonday(DateTime date)
        {
            var day = (int)date.DayOfWeek;
            if (day == 0)
                day += 6;
            else
                day--;

            return new DateTime(date.Year, date.Month, date.Day - day);
        }
    }

    public class EntryAppointment
    {
        public EntryAppointment()
        {
        }

        public EntryAppointment(Entry entry)
        {
            Subject = entry.Activity;
            StartTime = entry.EntryDate;
            EndTime = StartTime.AddMinutes(15);

            if (entry.Category is not null)
            {
                CategoryId = entry.Category.Id;
                Priority = entry.Category.Priority;
            }

            Entries.Add(entry);
        }

        public void AddEntry(Entry entry)
        {
            Subject += " + " + entry.Activity;
            if (entry.Category is not null && entry.Category.Priority < Priority)
            {
                CategoryId = entry.Category.Id;
                Priority = entry.Category.Priority;
            }
            Entries.Add(entry);
        }

        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CategoryId { get; set; }
        public int Priority { get; set; } = int.MaxValue;
        public List<Entry> Entries { get; set; } = new List<Entry>();
    }

    //public class CategoryColor
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string Color { get; set; }
    //}

}
