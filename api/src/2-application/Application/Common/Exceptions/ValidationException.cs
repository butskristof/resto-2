﻿using FluentValidation.Results;
using Resto.Common.Enumerations;

namespace Resto.Application.Common.Exceptions;

public sealed class ValidationException : Exception
{
    private const string DefaultMessage = "One or more validation failures have occurred.";

    public ValidationException() : base(DefaultMessage)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(string key, ErrorCode errorCode) : base(DefaultMessage)
    {
        Errors = new Dictionary<string, string[]>
        {
            { key, new[] { errorCode.ToString() } }
        };
    }

    public IDictionary<string, string[]> Errors { get; }
}