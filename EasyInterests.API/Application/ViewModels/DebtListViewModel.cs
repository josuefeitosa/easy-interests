using System;
using System.Collections.Generic;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Enums;

namespace EasyInterests.API.Application.ViewModels
{
  public class DebtListViewModel
  {
    public DebtListViewModel() { }

    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string NegotiatorName { get; set; }
    public string NegotiatorPhone { get; set; }
    public string Description { get; set; }
    public double OriginalValue { get; set; }
    public double RecalculatedValue { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CalculationDate { get; set; }
    public int ParcelsQty { get; set; }
    public InterestTypesEnum InterestType { get; set; }
    public InterestIntervalsEnum InterestInterval { get; set; }
    public double InterestPercentage { get; set; }
    public double NegotiatorComissionPercentage { get; set; }
    public List<DebtParcel> Parcels { get; set; }
    public bool Paid { get; set; }
  }
}
