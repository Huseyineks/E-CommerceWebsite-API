using Azure.Identity;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public UserValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(i => i.Username)
                .MustAsync(async (username, cancellation) =>
                {
                    if (string.IsNullOrEmpty(username)) return false;
                    var existingUser = await _userManager.FindByNameAsync(username);
                    return existingUser == null;
                })
                .WithMessage("Bu kullanıcı adı zaten kullanılıyor.")
                .Must(beAlphabetic)
                .WithMessage("Lütfen geçerli bir isim girin.")
                .NotEmpty()
                .WithMessage("İsim alanı zorunludur.");

            RuleFor(i => i.Email)
                .MustAsync(async (email, cancellation) =>
                {
                    if (string.IsNullOrEmpty(email)) return false;
                    var existingUser = await _userManager.FindByEmailAsync(email);
                    return existingUser == null;
                })
                .WithMessage("Bu email adresi zaten kullanılıyor.")
                .EmailAddress()
                .WithMessage("Lütfen geçerli bir email adresi girin.")
                .NotEmpty()
                .WithMessage("Email alanı zorunludur.");

            RuleFor(i => i.Adress).NotEmpty().WithMessage("Adres alanı zorunludur.");
            RuleFor(i => i.Street).NotEmpty().WithMessage("Sokak alanı zorunludur.");
            RuleFor(i => i.City).NotEmpty().WithMessage("Şehir alanı zorunludur.");
            RuleFor(i => i.Neighbourhood).NotEmpty().WithMessage("Mahalle alanı zorunludur.");
            RuleFor(i => i.PostalCode)
                .Must(beNumeric)
                .WithMessage("Lütfen geçerli bir posta kodu girin.")
                .NotEmpty()
                .WithMessage("Posta kodu alanı zorunludur.");
            RuleFor(i => i.ConfirmPassword)
                .Equal(i => i.Password)
                .WithMessage("Lütfen şifrelerin aynı olduğundan emin olun.");
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