using System;
using System.Collections.Generic;
using EasyInterests.API.Application.DTOs;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.ViewModels;
using EasyInterests.API.Enums;
using EasyInterests.API.Helpers;

namespace EasyInterests.API.Application.Services
{
  public class DebtService : IDebtService
  {
    public void Create(CreateDebtDTO debt)
    {
      var calculatedDebt = this.CalculateDebt(debt);
    }

    public Debt CalculateDebt(CreateDebtDTO debt)
    {
      #region Calculating value with interests
      var calcDate = DateTime.Now;
      double calcValue = 0.00;

      switch (debt.InterestInterval)
      {
        case InterestIntervalsEnum.Day: // Interests by day
          {
            var diffDays = (calcDate - debt.DueDate).Days;

            if (debt.InterestType == InterestTypesEnum.Simple)
              calcValue = InterestCalcs.SimpleInterest(debt.OriginalValue, debt.InterestPercentage, diffDays);

            else
              calcValue = InterestCalcs.CompoundInterest(debt.OriginalValue, debt.InterestPercentage, diffDays);
          }
          break;

        case InterestIntervalsEnum.Month: // Interests by month
          {
            if ((calcDate - debt.DueDate).TotalDays < 30)
              throw new Exception("Difference between due and today is lower than a month.");

            var diffMonths = ((calcDate.Year - debt.DueDate.Year) * 12) + calcDate.Month - debt.DueDate.Month;

            if (debt.InterestType == InterestTypesEnum.Simple)
              calcValue = InterestCalcs.SimpleInterest(debt.OriginalValue, debt.InterestPercentage, diffMonths);

            else
              calcValue = InterestCalcs.CompoundInterest(debt.OriginalValue, debt.InterestPercentage, diffMonths);
          }
          break;

        case InterestIntervalsEnum.Year: // Interests by year
          {
            if ((calcDate - debt.DueDate).TotalDays < 365)
              throw new Exception("Difference between due and today is lower than a year.");

            var diffYears = Math.Round((calcDate - debt.DueDate).TotalDays / 365);

            if (debt.InterestType == InterestTypesEnum.Simple)
              calcValue = InterestCalcs.SimpleInterest(debt.OriginalValue, debt.InterestPercentage, diffYears);

            else
              calcValue = InterestCalcs.CompoundInterest(debt.OriginalValue, debt.InterestPercentage, diffYears);
          }
          break;

        default:
          throw new Exception("Invalid interest interval type");
      }
      #endregion

      Debt calculatedDebt = new Debt{
        Description = debt.Description,
        CustomerId = debt.CustomerId,
        NegotiatorId = debt.NegotiatorId,
        DueDate = debt.DueDate,
        InterestInterval = debt.InterestInterval,
        InterestPercentage = debt.InterestPercentage,
        InterestType = debt.InterestType,
        NegotiatorComissionPercentage = debt.NegotiatorComissionPercentage,
        OriginalValue = debt.OriginalValue,
        ParcelsQty = debt.ParcelsQty,
      };

      calculatedDebt.RecalculatedValue = calcValue;

      return calculatedDebt;
    }
    public List<DebtListViewModel> GetList()
    {
      throw new System.NotImplementedException();
    }

    public void UpdatePaidDebt(int Id)
    {
      throw new System.NotImplementedException();
    }

    public void UpdatePaidParcel(int parcelNumber)
    {
      throw new System.NotImplementedException();
    }
  }
}
