using AspNetCoreNuxt.Extensions.FluentValidation;
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Test.Models
{
    //[ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class FileWithModel
    {
        public Nest Nest { get; set; }

        public IFormFile File { get; set; }
    }


    public class Nest
    {
        public string Value { get; private set; }

        public NestNest NestNest { get; private set; }
    }

    public class NestNest
    {
        public string Value { get; private set; }
    }

    public class NestValidator : AbstractValidator<Nest>
    {
        public NestValidator()
        {
            RuleFor(x => x.Value)
                .Configure(x => x.DisplayName = new StaticStringSource("ばりゅー"))
                .NotNull();
            RuleFor(x => x.NestNest.Value)
                .Configure(x => x.DisplayName = new StaticStringSource("ネスト"))
                .NotNull();
        }
    }

    public class CollectionNestValidator : AbstractValidator<Collection<Nest>>
    {
        public CollectionNestValidator()
        {
            RuleForEach(x => x)
                .RemoveForEachPropertyName()
                .SetValidator(new NestValidator());
        }
    }
}
