﻿using System;
using FluentValidation;
using Users_Demo.DAL.Models;

namespace Users_Demo.Validation
{
    public class UserModelValidation : AbstractValidator<Users>
    {
        public UserModelValidation()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$")
                .Length(2, 50);

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$")
                .Length(2, 50);

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .NotEmpty()
                .Must(BeAValidDate);

            RuleFor(x => x.IsDeleted).NotEmpty();

            RuleFor(x => x.IsActive).NotEmpty();
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
