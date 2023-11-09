﻿using System.Net.Mail;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.Features.UsersRegistration;

internal class ValidateEmailQueryHandler : IQueryHandler<ValidateEmailQuery>
{
    public void Handle(ValidateEmailQuery query)
    {
        try
        {
            query.Result = new MailAddress(query.Email) != null;
        }
        catch (FormatException)
        {
            query.Result = false;
        }
    }
}
