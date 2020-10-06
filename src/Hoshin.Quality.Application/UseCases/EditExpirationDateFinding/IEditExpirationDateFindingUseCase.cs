namespace Hoshin.Quality.Application.UseCases.EditExpirationDateFinding
{
    public interface IEditExpirationDateFindingUseCase
    {
        bool Execute(Domain.Finding.Finding finding, string createdByUserId);
    }
}
