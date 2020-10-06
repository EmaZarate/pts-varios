using Hoshin.Quality.Domain.FindingsState;
using System;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditExpirationDateFinding
{
    public class EditExpirationDateFindingDTO
    {
        public int Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CreatedByUserId { get; set; }
        public int FindingStateID { get; set; }
        public FindingsState FindingState { get; set; }
        public string FindingStateName { get; set; }
    }
}
