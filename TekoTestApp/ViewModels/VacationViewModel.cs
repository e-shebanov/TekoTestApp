using System.Collections.Generic;
using TekoTestApp.Models;

namespace TekoTestApp.ViewModels
{
    public class VacationViewModel
    {
        public List<Vacation> Vacations { get; set; }
        public Vacation NewVacation { get; set; }
    }
}