using System.Collections.Generic;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.ViewModels;

namespace EasyInterests.API.Infrastructure.Repositories
{
  public interface IDebtRepository
  {
    void Create(Debt debt);
    void Update(int Id, UpdateDebtDTO debt);
    List<Debt> GetByCustomer(int userId);
    List<Debt> GetByNegotiator(int userId);
    List<DebtListViewModel> GetAll();
    Debt GetOne(int Id);
  }
}
