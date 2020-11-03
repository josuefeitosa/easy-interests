using System;

namespace EasyInterests.API.Application.Models
{
  public class DebtParcel
  {
    public DebtParcel(int id, int debtId, int parcel, double value, DateTime dueDate)
    {
      this.Id = id;
      this.DebtId = debtId;
      this.Parcel = parcel;
      this.Value = value;
      this.DueDate = dueDate;

    }
    public int Id { get; set; }
    public int DebtId { get; set; }
    public virtual Debt Debt { get; set; }
    public int Parcel { get; set; }
    public double Value { get; set; }
    public DateTime DueDate { get; set; }
  }
}
