using EyeSave.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeSave.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<Agent> Agents { get; set; }
        private Agent _selectedAgent;
        private string _selectedSort;
        private string _selectedFilter;

        public Agent SelectedAgent
        {
            get => _selectedAgent;
            set => Set(ref _selectedAgent, value, nameof(SelectedAgent));
        }

        public List<string> SortList { get; } = new List<string>
        {
            "Без сортировки",
            "Наименование (возраст)",
            "Наименование (убыв)",
            "Размер скидки (возр)",
            "Размер скидки (убыв)",
            "Приоритет (возр)",
            "Приоритет (уьыв)"
        };

        public string SelectedSort
        {
            get => _selectedSort; 
            set => Set(ref _selectedSort, value, nameof(SelectedSort));
        }

        public string SelectedFilter
        {
            get => _selectedFilter;
            set => Set(ref _selectedFilter, value, nameof(SelectedFilter));
        }

        public List<string> FilterList { get; } = new List<string>
        {
            "Все типы"

        };
        public MainWindowViewModel()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                Agents = context.Agents.AsNoTracking()
                    .Include(a => a.AgentType)
                    .Include(a => a.ProductSales)
                    .ThenInclude(ps => ps.Product)
                    .OrderBy(a => a.Id)
                    .ToList();

                FilterList.AddRange(context.AgentTypes.Select(at => at.Title));
            }
        }

    }
}
