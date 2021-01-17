using FrontEndAPIHelper.Endpoints;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Pages.Week2
{
    public class Cell2Class : ComponentBase
    {
        [Inject] IEntryAccess _ea { get; set; }
        [Parameter] public int x { get; set; }
        [Parameter] public int y { get; set; }
        [Parameter] public EntryData Entry { get; set; }
        [Parameter] public EventCallback CellHasUpdated { get; set; }

        //[Parameter] public string Value { get; set; }
        //[Parameter] public Color Color { get; set; }

        public Color Color => Entry.Color;
        public string Value
        {
            get => Entry.Value;
            set
            {
                new Task(async () =>
                {
                    await UpdateValue(value);
                }).Start();
            }
        }


        private async Task UpdateValue(string v)
        {
            Console.WriteLine("Updating to: " + v);
            await _ea.UpdateEntry(Entry.Date, v);
            await InvokeAsync(async () => await CellHasUpdated.InvokeAsync());
        }
    }
}
