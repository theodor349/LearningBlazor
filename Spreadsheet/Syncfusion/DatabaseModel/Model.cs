using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.DatabaseModel
{
    public class Model
    {
        private static List<EntryTime> _entryTimes = new List<EntryTime>()
        {
            new EntryTime() { Date = new DateTime(2021, 1, 6, 10, 15, 0), Minutes = 150 },
            new EntryTime() { Date = new DateTime(2021, 1, 6, 12, 45, 0), Minutes = 210 },
            new EntryTime() { Date = new DateTime(2021, 1, 6, 16, 15, 0), Minutes = 255 },
            new EntryTime() { Date = new DateTime(2021, 1, 6, 20, 30, 0), Minutes = 0 },

            new EntryTime() { Date = new DateTime(2020, 12, 29, 10, 15, 0), Minutes = 150 },
            new EntryTime() { Date = new DateTime(2020, 12, 29, 12, 45, 0), Minutes = 210 },
            new EntryTime() { Date = new DateTime(2020, 12, 29, 16, 15, 0), Minutes = 255 },
            new EntryTime() { Date = new DateTime(2020, 12, 29, 20, 30, 0), Minutes = 0 },

            new EntryTime() { Date = new DateTime(2021, 1, 2, 10, 0, 0), Minutes = 150 },
            new EntryTime() { Date = new DateTime(2021, 1, 2, 12, 0, 0), Minutes = 210 },
            new EntryTime() { Date = new DateTime(2021, 1, 2, 16, 0, 0), Minutes = 255 },
            new EntryTime() { Date = new DateTime(2021, 1, 2, 20, 0, 0), Minutes = 0 },
        };

        private static List<EntryData> _entries = new List<EntryData>()
        {
            new EntryData(){ Id = 1, ActivityId = 1, EntryDate = new DateTime(2021, 1, 6, 10, 15, 0) },
            new EntryData(){ Id = 1, ActivityId = 3, EntryDate = new DateTime(2021, 1, 6, 10, 15, 0) },
            new EntryData(){ Id = 2, ActivityId = 2, EntryDate = new DateTime(2021, 1, 6, 12, 45, 0) },
            new EntryData(){ Id = 3, ActivityId = 3, EntryDate = new DateTime(2021, 1, 6, 16, 15, 0) },
            new EntryData(){ Id = 3, ActivityId = 7, EntryDate = new DateTime(2021, 1, 6, 16, 15, 0) },
            new EntryData(){ Id = 4, ActivityId = 4, EntryDate = new DateTime(2021, 1, 6, 20, 30, 0) },
            new EntryData(){ Id = 5, ActivityId = 5, EntryDate = new DateTime(2021, 1, 6, 20, 30, 0) },

            new EntryData(){ Id = 1, ActivityId = 1, EntryDate = new DateTime(2020, 12, 29, 10, 15, 0) },
            new EntryData(){ Id = 2, ActivityId = 2, EntryDate = new DateTime(2020, 12, 29, 12, 45, 0) },
            new EntryData(){ Id = 3, ActivityId = 3, EntryDate = new DateTime(2020, 12, 29, 16, 15, 0) },
            new EntryData(){ Id = 4, ActivityId = 4, EntryDate = new DateTime(2020, 12, 29, 20, 30, 0) },
            new EntryData(){ Id = 5, ActivityId = 5, EntryDate = new DateTime(2020, 12, 29, 20, 30, 0) },

            new EntryData(){ Id = 1, ActivityId = 1, EntryDate = new DateTime(2021, 1, 2, 10, 0, 0) },
            new EntryData(){ Id = 2, ActivityId = 2, EntryDate = new DateTime(2021, 1, 2, 12, 0, 0) },
            new EntryData(){ Id = 3, ActivityId = 3, EntryDate = new DateTime(2021, 1, 2, 16, 0, 0) },
            new EntryData(){ Id = 4, ActivityId = 4, EntryDate = new DateTime(2021, 1, 2, 20, 0, 0) },
            new EntryData(){ Id = 5, ActivityId = 5, EntryDate = new DateTime(2021, 1, 2, 20, 0, 0) },
        };

        private static List<ActivityData> _activities = new List<ActivityData>()
        {
            new ActivityData(){ Id = 1, CategoryId = 1, Name = "Breakfast", NormName = "breakfast" },
            new ActivityData(){ Id = 2, CategoryId = 1, Name = "Lunch", NormName = "lunch" },
            new ActivityData(){ Id = 3, CategoryId = 2, Name = "Youtube", NormName = "youtube" },
            new ActivityData(){ Id = 4, CategoryId = 0, Name = "Dinner", NormName = "dinner" },
            new ActivityData(){ Id = 5, CategoryId = 3, Name = "Social: Julia", NormName = "social: julia" },
            new ActivityData(){ Id = 6, CategoryId = 4, Name = "TV", NormName = "tv" },
            new ActivityData(){ Id = 7, CategoryId = 4, Name = "Film: Power", NormName = "film: power" },
        };

        private static List<CategoryData> _categories = new List<CategoryData>()
        {
            new CategoryData(){ Id = 0, Priority = 0, Color = Color.White, Name = "Empty" },
            new CategoryData(){ Id = 1, Priority = 75, Color = Color.Yellow, Name = "Eating" },
            new CategoryData(){ Id = 2, Priority = 100, Color = Color.Red, Name = "Youtube" },
            new CategoryData(){ Id = 3, Priority = 50, Color = Color.Blue, Name = "Social" },
            new CategoryData(){ Id = 4, Priority = 99, Color = Color.Orange, Name = "Watching" },
        };

        public static async Task UpdateCategory(int activityId, int catId)
        {
            await Task.Delay(100); // Simulating calling an API
            var activity = _activities.Where(x => x.Id == activityId).FirstOrDefault();
            if(activity is not null)
            {
                if(_categories.Where(x => x.Id == catId).Count() > 0)
                {
                    activity.CategoryId = catId;
                }
            }
        }

        public static async Task<List<CategoryData>> GetCategories()
        {
            await Task.Delay(100); // Simulating calling an API
            return _categories;
        }

        public static async Task AddEntryString(string s, DateTime d)
        {
            await Task.Delay(100); // Simulating calling an API
            RemoveEntryTime(d);

            var activities = s.Split("+").ToList();
            activities.ConvertAll(x => x.Trim());
            if(activities.Count > 0)
            {
                _entryTimes.Add(new EntryTime() { Date = d });
                activities.ForEach(x => AddEntry(x, d));
            }
        }

        private static void RemoveEntryTime(DateTime d)
        {
            var times = _entryTimes.Where(x => x.Date == d).ToList();
            times.ForEach(x => _entryTimes.Remove(x));
            var entries = _entries.Where(x => x.EntryDate == d).ToList();
            entries.ForEach(x => _entries.Remove(x));
        }

        private static void AddEntry(string s, DateTime d)
        {
            var activity = GetActivity(s);
            var entry = new EntryData()
            {
                Id = _entries.Max(x => x.Id) + 1,
                ActivityId = activity.Id,
                EntryDate = d,
            };
            _entries.Add(entry);
        }

        private static ActivityData GetActivity(string s)
        {
            var a = _activities.Where(x => x.NormName.Equals(s.ToLower())).FirstOrDefault();
            if (a is null)
            {
                a = new ActivityData()
                {
                    Id = _activities.Max(x => x.Id) + 1,
                    Name = s,
                    NormName = s.ToLower(),
                    CategoryId = 0
                };
                _activities.Add(a);
            }
            return a;
        }

        public static async Task<List<EntryPoint>> GetEntryPoints(DateTime start, DateTime end)
        {
            await Task.Delay(100); // Simulating calling an API

            var res = new List<EntryPoint>();
            foreach (var e in _entryTimes.Where(x => x.Date >= start && x.Date < end))
            {
                res.Add(GetEntryPoint(e.Date));
            }
            res.Sort();
            return res;
        }

        private static EntryPoint GetEntryPoint(DateTime date)
        {
            return new EntryPoint()
            {
                Date = date,
                Entries = GetEntriesAt(date)
            };
        }

        private static List<Entry> GetEntriesAt(DateTime date)
        {
            var res = new List<Entry>();
            var entries = _entries.Where(x => x.EntryDate == date);
            foreach (var e in entries)
            {
                res.Add(GetEntry(e));
            }
            return res;
        }

        private static Entry GetEntry(EntryData e)
        {
            var activity = _activities.Where(x => x.Id == e.ActivityId).First();
            var cat = _categories.Where(x => x.Id == activity.CategoryId).First();
            return new Entry()
            {
                Id = e.Id,
                ActivityId = activity.Id,
                Activity = activity.Name,
                Category = cat,
            };
        }
    }

    public class EntryPoint : IComparable<EntryPoint>
    {
        public DateTime Date { get; set; }
        public List<Entry> Entries { get; set; }
        public string Name
        {
            get
            {
                string name = "";
                if (Entries?.Count > 0)
                {
                    name = Entries.First().Activity;
                    for (int i = 1; i < Entries.Count; i++)
                    {
                        name += " + " + Entries[i].Activity;
                    }
                }
                return name;
            }
        }
        public Color Color
        {
            get
            {
                int min = Entries.Min(x => x.Category.Priority);
                return Entries.Where(x => x.Category.Priority == min).First().Category.Color;
            }
        }

        public int CompareTo(EntryPoint other)
        {
            return DateTime.Compare(Date, other.Date);
        }
    }

    public class Entry
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string Activity { get; set; }
        public CategoryData Category { get; set; }
    }

    // DB
    public class EntryTime : IComparable<EntryTime>
    {
        public DateTime Date { get; set; }
        public int Minutes { get; set; }

        public int CompareTo(EntryTime other)
        {
            return DateTime.Compare(Date, other.Date);
        }
    }

    public class EntryData : IComparable<EntryData>
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public DateTime EntryDate { get; set; }

        public int CompareTo(EntryData other)
        {
            return DateTime.Compare(EntryDate, other.EntryDate);
        }
    }

    public class ActivityData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormName { get; set; }
        public int CategoryId { get; set; }
    }

    public class CategoryData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public Color Color { get; set; }
    }
}
