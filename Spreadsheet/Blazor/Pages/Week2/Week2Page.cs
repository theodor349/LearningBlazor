using FrontEndAPIHelper.Endpoints;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Pages.Week2
{
    public class Week2Page : ComponentBase
    {
        [Inject] IEntryAccess _ea { get; set; }

        public DateTime StartDate { get; set; } = new DateTime(2020, 12, 28, 5, 0, 0);

        protected WeekObject data;
        protected int startingHour = 5;

        protected async override Task OnInitializedAsync()
        {
            await UpdateData();
        }

        protected async Task UpdateData()
        {
            var entries = await _ea.GetBetween(StartDate, StartDate.AddDays(7));
            data = new WeekObject();
            data.GenerateWeek(entries, StartDate);
            await InvokeAsync(() => StateHasChanged());
        }

    }
}
