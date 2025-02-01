using Azure.Identity;

using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {

            RuleFor(i => i.Username).Must(beAlphabetic).WithMessage("Please enter a valid name.").NotEmpty().WithMessage("Name field is required.");
            RuleFor(i => i.Email).EmailAddress().WithMessage("Please enter a valid email").NotEmpty().WithMessage("Email field is required.");
            RuleFor(i => i.Adress).NotEmpty().WithMessage("Address field is required.");
            RuleFor(i => i.Street).NotEmpty().WithMessage("Street field is required.");
            RuleFor(i => i.City).NotEmpty().WithMessage("City field is required.");
            RuleFor(i => i.Neighbourhood).NotEmpty().WithMessage("Neighbourhood field is required.");
            RuleFor(i => i.PostalCode).Must(beNumeric).WithMessage("Please enter a valid Postal Code").NotEmpty().WithMessage("Postal Code field is required.");
            RuleFor(i => i.ConfirmPassword).Equal(i => i.Password).WithMessage("Please make sure that passwords are same.");




        }



        private bool beAlphabetic(string username)
        {


            if (username != null)
            {
                return username.All(char.IsLetter);

            }
            return true;

        }

        private bool beNumeric(string postalCode)
        {
            if (postalCode != null)
            {

                return postalCode.All(char.IsDigit);

            }

            return true;
        }
    }
}