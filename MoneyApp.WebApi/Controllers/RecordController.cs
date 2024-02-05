using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyApp.UseCases.Handlers.Records.Commands.AddRecord;
using MoneyApp.UseCases.Handlers.Records.Commands.DeleteRecord;
using MoneyApp.UseCases.Handlers.Records.Commands.UpdateRecord;
using MoneyApp.UseCases.Handlers.Records.Queries.GetRecord;
using MoneyApp.WebApi.Models.Record;

namespace MoneyApp.WebApi.Controllers
{
    public class RecordController : Controller
    {
        private readonly IMediator _mediator;

        public RecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [Route("api/addRecord")]
        public async Task<IActionResult> AddRecord([FromBody] AddRecordModel model)
        {
            var command = new AddRecordCommand()
            {
                Change = model.Change,
                Text = model.Text,
                Created = model.Date,
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("api/deleteRecord")]
        public async Task<IActionResult> DeleteRecord([FromBody] DeleteRecordModel model)
        {
            var command = new DeleteRecordCommand()
            {
                RecordId = model.RecordId,
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("api/updateRecord")]
        public async Task<IActionResult> UpdateRecord([FromBody] UpdateRecordModel model)
        {
            var command = new UpdateRecordCommand()
            {
                RecordId = model.RecordId,
                Change = model.Change,
                Created = model.Date,
                Text = model.Text,
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("api/getRecord")]
        public async Task<IActionResult> GetRecord(GetRecordModel model)
        {
            var command = new GetRecordQuery()
            {
                RecordId = model.RecordId,
            };

            var record = await _mediator.Send(command);

            return Ok(new { Record = record, });
        }
    }
}
