using System.Collections.Generic;
using EasyInterests.API.Application.Models;

namespace EasyInterests.API.Application.DTOs
{
  public class UpdateDebtDTO
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public virtual ICollection<UpdateDebtParcelDTO> Parcels { get; set; }
    public double NegotiatorComissionPercentage { get; set; }
    public bool Paid { get; set; }
  }
}
