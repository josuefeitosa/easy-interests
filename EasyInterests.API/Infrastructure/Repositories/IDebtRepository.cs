using System.Collections.Generic;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.ViewModels;

namespace EasyInterests.API.Infrastructure.Repositories
{
  public interface IDebtRepository
  {
    void Create(Debt debt);
    void Update(int Id, Debt debt);
    List<DebtListViewModel> GetAll();
    Debt GetOne();
  }
}
