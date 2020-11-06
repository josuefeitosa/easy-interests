using System;
using System.Collections.Generic;
using System.Linq;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.ViewModels;

namespace EasyInterests.API.Infrastructure.Repositories
{
  public class DebtRepository : IDebtRepository
  {
    private readonly EasyInterestsDBContext _dbContext;

    public DebtRepository(EasyInterestsDBContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async void Create(Debt debt)
    {
      try
      {
        var createdDebt = await _dbContext.Debts.AddAsync(debt);

        await _dbContext.SaveChangesAsync();
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao fazer esta operação.\n-> {e.Message}");
      }
    }

    public List<DebtListViewModel> GetAll()
    {
      try
      {
        var debts = _dbContext.Debts.Select(x => new DebtListViewModel {
          Id = x.Id,
          CustomerName = x.Customer.Name,
          Description = x.Description,
          DueDate = x.DueDate,
          InterestInterval = x.InterestInterval.ToString(),
          InterestType = x.InterestType.ToString(),
          RecalculatedValue = x.RecalculatedValue,
          ParcelsQty = x.ParcelsQty,
          InterestPercentage = x.InterestPercentage,
          Parcels = x.Parcels.ToList(),
          NegotiatorComissionPercentage = x.NegotiatorComissionPercentage,
          NegotiatorName = x.Negotiator.Name,
          NegotiatorPhone = x.Negotiator.PhoneNumber,
          OriginalValue = x.OriginalValue
        }).ToList();

        return debts;
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao buscar os dados solicitados.\n-> {e.Message}");
      }
    }

    public Debt GetOne()
    {
      throw new System.NotImplementedException();
    }

    public void Update(int Id, Debt debt)
    {
      throw new System.NotImplementedException();
    }
  }
}
