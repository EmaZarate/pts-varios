using Hoshin.Quality.Application.Exceptions.Finding;
using Hoshin.Quality.Application.Repositories;

namespace Hoshin.Quality.Application.UseCases.EditExpirationDateFinding
{
    public class EditExpirationDateFindingUseCase : IEditExpirationDateFindingUseCase
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IFindingStateHistoryRepository _findingStateHistoryRepository;

        public EditExpirationDateFindingUseCase(IFindingRepository findingRepository, IFindingStateHistoryRepository findingStateHistoryRepository)
        {
            _findingRepository = findingRepository;
            _findingStateHistoryRepository = findingStateHistoryRepository;
        }

        public bool Execute(Domain.Finding.Finding finding, string createdByUserId)
        {
            if (!finding.IsStateExpired) throw new FindingStateMustBeExpiredException(finding);
            if (!finding.ValidateExpirationDate()) throw new InvalidExpirationDateException(finding);

            finding.FindingStateID = _findingStateHistoryRepository.GetPreviousState(finding.Id, finding.FindingStateID);
            _findingStateHistoryRepository.Add(finding.Id, finding.FindingStateID, createdByUserId);
            return _findingRepository.UpdateExpirationDate(finding);
        }
    }
}
