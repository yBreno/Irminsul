using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Irminsul.Application.DTos.Characters;

namespace Irminsul.Application.DTos.Validators
{
    public class CreateCharacterDtoValidator : AbstractValidator<CreateCharacterDto>
    {
        public CreateCharacterDtoValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Nome não pode ser vazio").MaximumLength(100).WithMessage("Nome não pode ter mais de 100 caracteres");
            RuleFor(x => x.title).NotEmpty().WithMessage("Título não pode ser vazio").MaximumLength(100).WithMessage("Título não pode ter mais de 100 caracteres");
            RuleFor(x => x.imageUrl).NotEmpty().WithMessage("A imagem é obrigatoria").Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("A imagem deve ser uma URL válida");
            RuleFor(x => x.description).NotEmpty().WithMessage("Descrição não pode ser vazia").MaximumLength(500).WithMessage("A Descrição nao pode ter mais de 500 caracteres");
            RuleFor(x => x.lore).NotEmpty().WithMessage("Lore não pode ser vazia");
            RuleFor(x => x.rarity).IsInEnum().WithMessage("Raridade precisa ser valida");
            RuleFor(x => x.vision).IsInEnum().WithMessage("Visão precisa ser valida");
            RuleFor(x => x.weaponType).IsInEnum().WithMessage("Tipo de arma precisa ser valida");   
            RuleFor(x => x.nation).IsInEnum().WithMessage("Nação precisa ser valida");  
        }
    }
}
