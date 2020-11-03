using System;
using System.Collections.Generic;
using EasyInterests.API.Enums;

namespace EasyInterests.API.Application.Models
{
  public class Debt
  {
    public Debt(int id, int customerId, int negotiatorId, string description, double originalValue, DateTime dueDate, int parcelsQty, InterestTypesEnum interestType, InterestIntervalsEnum interestInterval, double interestPercentage, double negotiatorComissionPercentage)
    {
      this.Id = id;
      this.CustomerId = customerId;
      this.NegotiatorId = negotiatorId;
      this.Description = description;
      this.OriginalValue = originalValue;
      this.DueDate = dueDate;
      this.ParcelsQty = parcelsQty;
      this.InterestType = interestType;
      this.InterestInterval = interestInterval;
      this.InterestPercentage = interestPercentage;
      this.NegotiatorComissionPercentage = negotiatorComissionPercentage;

    }
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public virtual User Customer { get; set; }
    public int NegotiatorId { get; set; }
    public virtual User Negotiator { get; set; }
    public string Description { get; set; }
    public double OriginalValue { get; set; }
    public DateTime DueDate { get; set; }
    public int ParcelsQty { get; set; }
    public virtual ICollection<DebtParcel> Parcels { get; set; }
    public InterestTypesEnum InterestType { get; set; }
    public InterestIntervalsEnum InterestInterval { get; set; }
    public double InterestPercentage { get; set; }
    public double NegotiatorComissionPercentage { get; set; }
  }
}
