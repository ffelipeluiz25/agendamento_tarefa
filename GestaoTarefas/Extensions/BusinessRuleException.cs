﻿namespace GestaoTarefas.Extensions;
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message)
    {
    }
}