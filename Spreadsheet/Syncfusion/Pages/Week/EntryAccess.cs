using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Schedule;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Syncfusion.Pages.Week
{
    public class EntryAccess : IEntryAccess
    {
        private class Model
        {
            public static List<EntryData> _entries = new List<EntryData>()
            {
                new EntryData(){ Id = 1, ActivityId = 1, EntryDate = new DateTime(2021, 1, 1, 10, 0, 0) },
                new EntryData(){ Id = 2, ActivityId = 2, EntryDate = new DateTime(2021, 1, 1, 12, 45, 0) },
                new EntryData(){ Id = 3, ActivityId = 3, EntryDate = new DateTime(2021, 1, 1, 16, 15, 0) },
                new EntryData(){ Id = 4, ActivityId = 4, EntryDate = new DateTime(2021, 1, 1, 20, 30, 0) },
                new EntryData(){ Id = 5, ActivityId = 5, EntryDate = new DateTime(2021, 1, 1, 20, 30, 0) },
            };

            public static List<ActivityData> _activities = new List<ActivityData>()
            {
                new ActivityData(){ Id = 1, CategoryId = 1, Name = "Breakfast" },
                new ActivityData(){ Id = 2, CategoryId = 1, Name = "Lunch" },
                new ActivityData(){ Id = 3, CategoryId = 2, Name = "Youtube" },
                new ActivityData(){ Id = 4, CategoryId = 1, Name = "Dinner" },
                new ActivityData(){ Id = 5, CategoryId = 3, Name = "Social: Julia" },
            };

            public static List<CategoryData> _categories = new List<CategoryData>()
            {
                new CategoryData(){ Id = 1, TypeId = 1, Name = "Eating" },
                new CategoryData(){ Id = 2, TypeId = 2, Name = "Youtube" },
                new CategoryData(){ Id = 3, TypeId = 3, Name = "Social" },
            };

            public static List<CategoryTypeData> _categoryTypes = new List<CategoryTypeData>()
            {
                new CategoryTypeData(){ Id = 1, Priority = 75, Color = Color.Yellow, Name = "Eating" },
                new CategoryTypeData(){ Id = 2, Priority = 100, Color = Color.Red, Name = "Entertainment" },
                new CategoryTypeData(){ Id = 3, Priority = 50, Color = Color.LightBlue, Name = "Social" },
            };
        }


        public async Task<List<Entry>> GetEntries(DateTime start, DateTime end)
        {
            Thread.Sleep(100);
            return Model._entries.ConvertAll(x => ConvertToEntry(x));
        }

        public async Task<List<Category>> GetCategories()
        {
            Thread.Sleep(100);
            return Model._categories.ConvertAll(x => GetCategory(x.Id));
        }

        private Entry ConvertToEntry(EntryData e)
        {
            var activity = Model._activities.Where(x => x.Id == e.ActivityId).FirstOrDefault();
            var cat = GetCategory(activity.CategoryId);
            return new Entry(e, activity, cat);
        }

        private Category GetCategory(int catId)
        {
            if (catId == 0)
                return null;

            CategoryTypeData type = null;
            var cat = Model._categories.Where(x => x.Id == catId).FirstOrDefault();
            if (cat.TypeId != 0)
                type = Model._categoryTypes.Where(x => x.Id == cat.TypeId).FirstOrDefault();
            return new Category(cat, type);
        }
    }

    // Local
    public class Entry
    {
        public Entry(EntryData e, ActivityData activity, Category cat)
        {
            Id = e.Id;
            ActivityId = activity.Id;
            Activity = activity.Name;
            EntryDate = e.EntryDate;
            Category = cat;
        }

        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string Activity { get; set; }
        public DateTime EntryDate { get; set; }
        public Category Category { get; set; }
    }

    public class Category
    {
        public Category()
        {
            Id = 0;
            Name = "";
            TypeId = 0;
            Color = Color.White;
            HexColor = ToHex(Color);
            Priority = int.MaxValue;
        }

        public Category(CategoryData cat, CategoryTypeData type)
        {
            Id = cat.Id;
            Name = cat.Name;
            TypeId = cat.TypeId;
            if(type is null)
            {
                Color = Color.White;
                HexColor = ToHex(Color);
                Priority = int.MaxValue;
            }
            else
            {
                Color = type.Color;
                HexColor = ToHex(Color);
                Priority = type.Priority;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int Priority { get; set; }
        public Color Color { get; set; }
        public string HexColor { get; set; }

        private string ToHex(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }

    // DB
    public class EntryData
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public DateTime EntryDate { get; set; }
    }

    public class ActivityData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }

    public class CategoryData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
    }

    public class CategoryTypeData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public Color Color { get; set; }
    }
    
}
