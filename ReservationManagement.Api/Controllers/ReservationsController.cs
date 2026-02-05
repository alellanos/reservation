using Microsoft.AspNetCore.Mvc;
using ReservationManagement.Api.Requests;
using ReservationManagement.Application.Dtos;
using ReservationManagement.Application.UseCases.CancelReservation;
using ReservationManagement.Application.UseCases.CompleteReservation;
using ReservationManagement.Application.UseCases.ConfirmReservation;
using ReservationManagement.Application.UseCases.CreateReservation;
using ReservationManagement.Application.UseCases.GetReservationDetail;
using ReservationManagement.Application.UseCases.GetReservations;
using ReservationManagement.Application.UseCases.MarkReservationAsNoShow;
using ReservationManagement.Domain.Enums;

namespace ReservationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly ICreateReservationUseCase _createUseCase;
        private readonly IGetReservationsUseCase _getReservationsUseCase;
        private readonly IGetReservationDetailUseCase _getDetailUseCase;
        private readonly IConfirmReservationUseCase _confirmUseCase;
        private readonly ICancelReservationUseCase _cancelUseCase;
        private readonly IMarkReservationAsNoShowUseCase _noShowUseCase;
        private readonly ICompleteReservationUseCase _completeReservationUseCase;

        public ReservationsController(
            ICreateReservationUseCase createUseCase,
            IGetReservationsUseCase getReservationsUseCase,
            IGetReservationDetailUseCase getDetailUseCase,
            IConfirmReservationUseCase confirmUseCase,
            ICancelReservationUseCase cancelUseCase,
            IMarkReservationAsNoShowUseCase noShowUseCase,
            ICompleteReservationUseCase completeReservationUseCase)
        {
            _createUseCase = createUseCase;
            _getReservationsUseCase = getReservationsUseCase;
            _getDetailUseCase = getDetailUseCase;
            _confirmUseCase = confirmUseCase;
            _cancelUseCase = cancelUseCase;
            _noShowUseCase = noShowUseCase;
            _completeReservationUseCase = completeReservationUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest request)
        {
            var command = new CreateReservationCommand
            {
                UserId = request.UserId,
                Name = request.Name,
                Email = request.Email,
                DateTime = request.DateTime,
                PeopleQuantity = request.PeopleQuantity,
                Type = request.Type,
                VipCode = request.VipCode,
                PreferredTable = request.PreferredTable,
                BirthdayAge = request.BirthdayAge,
                RequiresCake = request.RequiresCake
            };

            await _createUseCase.ExecuteAsync(command);

            return Created(string.Empty, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] DateTime? date,
            [FromQuery] ReservationType? type,
            [FromQuery] ReservationStatus? status,
            [FromQuery] string? name,
            [FromQuery] string? email)
        {
            var filter = new ReservationFilterDto
            {
                Date = date,
                Type = type,
                Status = status,
                Name = name,
                Email = email
            };

            var result = await _getReservationsUseCase.ExecuteAsync(filter);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var result = await _getDetailUseCase.ExecuteAsync(id);
            return Ok(result);
        }

        [HttpPost("{id:guid}/confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            await _confirmUseCase.ExecuteAsync(id);
            return Ok();
        }

        [HttpPost("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(
            Guid id,
            [FromBody] CancelReservationRequest request)
        {
            await _cancelUseCase.ExecuteAsync(id, request.Reason);
            return Ok();
        }

        [HttpPost("{id:guid}/no-show")]
        public async Task<IActionResult> NoShow(Guid id)
        {
            await _noShowUseCase.ExecuteAsync(id);
            return Ok();
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            await _completeReservationUseCase.ExecuteAsync(id);
            return Ok();
        }
    }

}
