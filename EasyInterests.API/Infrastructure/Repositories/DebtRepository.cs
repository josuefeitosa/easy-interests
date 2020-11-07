using System;
using System.Collections.Generic;
using System.Linq;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.ViewModels;
using Microsoft.EntityFrameworkCore;

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
          OriginalValue = x.OriginalValue,
          Paid = x.Paid
        }).ToList();

        return debts;
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao buscar os dados solicitados.\n-> {e.Message}");
      }
    }

    public List<Debt> GetByCustomer(int userId)
    {
      try
      {
        return _dbContext.Debts.Where(x => x.CustomerId == userId).ToList();
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao buscar os dados solicitados.\n-> {e.Message}");
      }
    }

    public List<Debt> GetByNegotiator(int userId)
    {
      try
      {
        return _dbContext.Debts.Where(x => x.NegotiatorId == userId).ToList();
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao buscar os dados solicitados.\n-> {e.Message}");
      }
    }

    public Debt GetOne(int Id)
    {
      try
      {
        return _dbContext.Debts.SingleOrDefault(x => x.Id == Id);
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao buscar os dados solicitados.\n-> {e.Message}");
      }
    }

    public async void Update(int Id, UpdateDebtDTO updatedDebt)
    {
      try
      {
        var debt = _dbContext.Debts
          .Include(x => x.Parcels)
            .SingleOrDefault(x => x.Id == Id);

        if (debt is null)
        {
          throw new Exception("Not Found");
        }

        if (updatedDebt.Description != null)
          debt.Description = updatedDebt.Description;

        if (updatedDebt.NegotiatorComissionPercentage != 0)
          debt.NegotiatorComissionPercentage = updatedDebt.NegotiatorComissionPercentage;

        if (updatedDebt.Paid != debt.Paid)
          debt.Paid = updatedDebt.Paid;

        if (updatedDebt.Parcels != null)
        {
          foreach (var updatedParcel in updatedDebt.Parcels)
          {
              var existingParcel = debt.Parcels.SingleOrDefault(x => x.Parcel == updatedParcel.Parcel);

              if (existingParcel is null)
              {
                throw new Exception("Not Found");
              }

              if (existingParcel.Paid != updatedParcel.Paid)
                existingParcel.Paid = updatedParcel.Paid;
          }
        }

        await _dbContext.SaveChangesAsync();
      }
      catch (Exception e)
      {
        throw new Exception($"Houve um problema ao fazer esta operação.\n-> {e.Message}");
      }
    }
  }
}
