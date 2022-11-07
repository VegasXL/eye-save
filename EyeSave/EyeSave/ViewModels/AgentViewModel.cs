using EyeSave.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyeSave.ViewModels
{
    public partial class AgentViewModel : ViewModelBase
    {
        private Agent _agent;

        public Agent Agent
        {
            get => _agent;
            set => Set(ref _agent, value, nameof(Agent));
        }

        private bool IsNew {get; init; }
        public AgentViewModel(int? agentId)
        {
            if (agentId == null)
            {
                IsNew = true;
                Agent = new Agent();
                return;
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Agent = context.Agents
                    .Include(a => a.AgentType)
                    .Include(a => a.ProductSales)
                    .ThenInclude(s => s.Product)
                    .Single(a => a.Id == agentId);
            }
        }
    }
}
