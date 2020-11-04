using System;

namespace EasyInterests.API.Application.DTOs
{
  public class CreateDebtParcelDTO
  {
    public CreateDebtParcelDTO(int debtId, int parcel, double value, DateTime dueDate)
    {
      this.DebtId = debtId;
      this.Parcel = parcel;
      this.Value = value;
      this.DueDate = dueDate;
    }

    public int DebtId { get; set; }
    public int Parcel { get; set; }
    public double Value { get; set; }
    public DateTime DueDate { get; set; }
  }
}
