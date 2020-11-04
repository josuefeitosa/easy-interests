using System.Collections.Generic;
using EasyInterests.API.Application.Models;

namespace EasyInterests.API.Infrastructure.Repositories
{
  public interface IDebtParcelRepository
  {
    void Create(DebtParcel debtParcel);
    void Update(int Id, DebtParcel debtParcel);
    List<DebtParcel> GetParcelsByDebt(int debtId);
  }
}
