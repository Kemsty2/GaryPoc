using ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Services;
using ElsaEdiBackend.Services;
using ElsaEdiBackend.Wrappers;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features
{
    public static class GetWorkflowDefinitionList
    {
        public sealed class Query : IRequest<PagedList<WorkflowDefinitionDto>>
        {
            public readonly WorkflowDefinitionParametersDto QueryParameters;

            public Query(WorkflowDefinitionParametersDto queryParameters)
            {
                QueryParameters = queryParameters;
            }
        }

        public sealed class Handler : IRequestHandler<Query, PagedList<WorkflowDefinitionDto>>
        {
            private readonly IWorkflowDefinitionRepository _countryRepository;
            private readonly SieveProcessor _sieveProcessor;
            private readonly ICurrentUserService _currentUserService;

            public Handler(SieveProcessor sieveProcessor, IWorkflowDefinitionRepository countryRepository, ICurrentUserService currentUserService)
            {
                _sieveProcessor = sieveProcessor;
                _countryRepository = countryRepository;
                _currentUserService = currentUserService;
            }

            public async Task<PagedList<WorkflowDefinitionDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var collection = _countryRepository.Query().AsNoTracking();

                var sieveModel = new SieveModel
                {
                    Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                    Filters = request.QueryParameters.Filters
                };

                var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
                if(!string.IsNullOrEmpty(_currentUserService.UserId))
                    appliedCollection = appliedCollection.Where(x => x.Permissions.Any(p =>p.UserId == _currentUserService.UserId));

                var dtoCollection = appliedCollection
                    .ProjectToType<WorkflowDefinitionDto>();

                return await PagedList<WorkflowDefinitionDto>.CreateAsync(dtoCollection,
                    request.QueryParameters.PageNumber,
                    request.QueryParameters.PageSize,
                    cancellationToken);
            }
        }
    }
}