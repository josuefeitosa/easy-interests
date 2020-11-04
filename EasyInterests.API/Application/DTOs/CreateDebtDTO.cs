using System;
using EasyInterests.API.Enums;

namespace EasyInterests.API.Application.DTOs
{
  public class CreateDebtDTO
  {
    public CreateDebtDTO(
      int customerId,
      int negotiatorId,
      string description,
      double originalValue,
      DateTime dueDate,
      int parcelsQty,
      InterestTypesEnum interestType,
      InterestIntervalsEnum interestInterval,
      double interestPercentage,
      double negotiatorComissionPercentage
      )
    {
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
    public int CustomerId { get; set; }
    public int NegotiatorId { get; set; }
    public string Description { get; set; }
    public double OriginalValue { get; set; }
    public DateTime DueDate { get; set; }
    public int ParcelsQty { get; set; }
    public InterestTypesEnum InterestType { get; set; }
    public InterestIntervalsEnum InterestInterval { get; set; }
    public double InterestPercentage { get; set; }
    public double NegotiatorComissionPercentage { get; set; }
  }
}
