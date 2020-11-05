using System.Collections.Generic;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.ViewModels;

namespace EasyInterests.API.Application.Services
{
    public interface IDebtService
    {
      void Create(string negotiatorEmail, CreateDebtDTO debt);
      Debt CalculateDebt(User negotiator, CreateDebtDTO debt);
      void UpdatePaidParcel(int parcelNumber);
      void UpdatePaidDebt(int Id);
      List<DebtListViewModel> GetList();
    }
}
