using English.Class.Domain.Core;
using FluentValidation;

namespace English.Class.FastEndpoint;

public class PageRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = Constants.MaxPageSize;
}

public class PageRequestValidator : Validator<PageRequest>
{
    public PageRequestValidator()
    {
        RuleFor(e => e.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(e => e.PageSize)
            .LessThanOrEqualTo(Constants.MaxPageSize);
    }
}