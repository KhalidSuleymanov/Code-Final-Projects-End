using CodeFinalProject.ViewModels;
using FluentValidation;

namespace CodeFinalProject.Validators;

public class BookOnlineVMValidator: AbstractValidator<BookOnlineVM>
{
    public BookOnlineVMValidator()
    {
        RuleFor(p => p.FullName).NotNull().NotEmpty().MaximumLength(60);
        RuleFor(p => p.PhoneNumber).NotNull().NotEmpty().Matches(@"^(\+|\d)[0-9]{8,16}$").MaximumLength(13);
        RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(p => p.Additionals).MaximumLength(300);
        RuleFor(p => p.ReservStartDate).NotNull().NotEmpty().GreaterThan(DateTime.Now);
        RuleFor(p => p.ReservEndDate).NotNull().NotEmpty().GreaterThan(p=>p.ReservStartDate);
        RuleFor(p => p.RoomId).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
    }
}