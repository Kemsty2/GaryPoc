using ElsaEdiBackend.Domain.WorkflowDefinitions;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ElsaEdiBackend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/workflows")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class WorkflowDefinitionsController : ControllerBase
    {
        private readonly ILogger<WorkflowDefinitionsController> _logger;
        private readonly IMediator _mediator;

        public WorkflowDefinitionsController(ILogger<WorkflowDefinitionsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of all workflows.
        /// </summary>
        /// <response code="200">Workflows list returned successfully.</response>
        /// <response code="400">Workflows has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while creating the Country.</response>
        /// <remarks>
        /// Requests can be narrowed down with a variety of query string values:
        /// ## Query String Parameters
        /// - **PageNumber**: An integer value that designates the page of records that should be returned.
        /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
        /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
        /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
        ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
        ///     - {Operator} is one of the Operators below
        ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
        ///
        ///    | Operator | Meaning                       | Operator  | Meaning                                      |
        ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
        ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
        ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
        ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
        ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
        ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
        ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
        ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
        ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<WorkflowDefinitionDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet(Name = "GetWorkflows")]
        public async Task<IActionResult> GetWorkflows([FromQuery] WorkflowDefinitionParametersDto workflowParametersDto)
        {
            var query = new GetWorkflowDefinitionList.Query(workflowParametersDto);
            var queryResponse = await _mediator.Send(query);

            var paginationMetadata = new
            {
                totalCount = queryResponse.TotalCount,
                pageSize = queryResponse.PageSize,
                currentPageSize = queryResponse.CurrentPageSize,
                currentStartIndex = queryResponse.CurrentStartIndex,
                currentEndIndex = queryResponse.CurrentEndIndex,
                pageNumber = queryResponse.PageNumber,
                totalPages = queryResponse.TotalPages,
                hasPrevious = queryResponse.HasPrevious,
                hasNext = queryResponse.HasNext
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(queryResponse);
        }

        /// <summary>
        /// Get a single workflow by Id
        /// </summary>
        /// <response code="200">workflow record returned successfully.</response>
        /// <response code="400">Payload has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while adding to accessList.</response>
        [ProducesResponseType(typeof(WorkflowDefinitionDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByWorkflowDefinitionId(string id)
        {
            var command = new GetByWorkflowDefinitionById.Query(id);
            var queryResponse = await _mediator.Send(command);

            return Ok(queryResponse);
        }

        /// <summary>
        /// Manually Trigger a workflow
        /// </summary>
        /// <param name="id">workflow id</param>
        /// <param name="payload">Workflow input</param>        
        /// <response code="204">Workflow triggered</response>
        /// <response code="400">Payload has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while adding to accessList.</response>
        [ProducesResponseType(typeof(WorkflowDefinitionDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpPost("{id:guid}/trigger")]
        public async Task<IActionResult> TriggerWorkflow(string id, [FromBody]TriggerWorkflowDto payload)
        {
            var command = new TriggerWorkflowById.Command(id, payload);
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Add User to Worklfow AccessList
        /// </summary>
        /// <response code="204">Users added.</response>
        /// <response code="400">Payload has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while adding to accessList.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpPatch("{id:guid}/permissions")]
        public async Task<IActionResult> AddUsersToWorkflowAccessList(string id, [FromBody] AccessListForPatchDto payload)
        {
            var command = new UpdateWorkflowPermission.Command(id, payload);
            await _mediator.Send(command);

            return NoContent();
        }


        /// <summary>
        /// Remove User to Worklfow AccessList
        /// </summary>
        /// <response code="204">Users removed.</response>
        /// <response code="400">Payload has missing/invalid values.</response>
        /// <response code="500">There was an error on the server while adding to accessList.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        [HttpDelete("{id:guid}/permissions")]
        public async Task<IActionResult> RemoveUsersToWorkflowAccessList(string id, [FromBody] AccessListForPatchDto payload)
        {
            var command = new UpdateWorkflowPermission.Command(id, payload, false);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}