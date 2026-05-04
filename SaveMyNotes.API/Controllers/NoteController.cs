using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveMyNotes.Application.Features.Notes.Commands.CreateNote;
using SaveMyNotes.Application.Features.Notes.Commands.DeleteAllNotes;
using SaveMyNotes.Application.Features.Notes.Commands.DeleteNote;
using SaveMyNotes.Application.Features.Notes.Commands.UpdateNote;
using SaveMyNotes.Application.Features.Notes.Queries.GetNoteDetail;
using SaveMyNotes.Application.Features.Notes.Queries.GetNotesList;

namespace SaveMyNotes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class NotesController : ControllerBase
{
    private readonly ISender _mediator;
    public NotesController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetNotesListQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) => Ok(await _mediator.Send(new GetNoteDetailQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteCommand command)
    {
        if (id != command.Id) return BadRequest("ID eşleşmiyor.");
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteNoteCommand(id));
        return NoContent();
    }

    [HttpDelete("clear-all")]
    public async Task<IActionResult> ClearAll()
    {
        await _mediator.Send(new DeleteAllNotesCommand());

        return NoContent();
    }
}