using EfCoreInterceptor.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Diagnostics;

namespace EfCoreInterceptor.Data;

public sealed class ObservabilityCommandInterceptor : DbCommandInterceptor
{
    private readonly ICurrentActor _actor;
    private readonly ILogger<ObservabilityCommandInterceptor> _logger;
    private static readonly int _thresholdMs = 1000;

   
    public ObservabilityCommandInterceptor(ICurrentActor actor, 
        ILogger<ObservabilityCommandInterceptor> logger)
    {
        _actor = actor;
        _logger = logger ;
     
    }

   
    #region Reader

    public override DbDataReader ReaderExecuted(
        DbCommand command,
        CommandExecutedEventData eventData,
        DbDataReader result)
    {
        LogIfSlow(command, eventData);
        return base.ReaderExecuted(command, eventData, result);
    }

    public override async ValueTask<DbDataReader> ReaderExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        DbDataReader result,
        CancellationToken cancellationToken = default)
    {
        LogIfSlow(command, eventData);
        return await base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    #endregion

    #region Scalar

    public override object? ScalarExecuted(
        DbCommand command,
        CommandExecutedEventData eventData,
        object? result)
    {
        LogIfSlow(command, eventData);
        return base.ScalarExecuted(command, eventData, result);
    }

    public override async ValueTask<object?> ScalarExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        object? result,
        CancellationToken cancellationToken = default)
    {
        LogIfSlow(command, eventData);
        return await base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
    }

    #endregion

    #region NonQuery

    public override int NonQueryExecuted(
        DbCommand command,
        CommandExecutedEventData eventData,
        int result)
    {
        LogIfSlow(command, eventData);
        return base.NonQueryExecuted(command, eventData, result);
    }

    public override async ValueTask<int> NonQueryExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        LogIfSlow(command, eventData);
        return await base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
    }

    #endregion

    private void LogIfSlow(DbCommand command, CommandExecutedEventData eventData)
    {
        var durationMs = eventData.Duration.TotalMilliseconds;

        if (durationMs < _thresholdMs)
            return;

        _logger.LogWarning(
            "Slow SQL detected ({Duration} ms)\nCommand: {CommandText}\nParameters: {Parameters}",
            durationMs,
            command.CommandText,
            GetParameters(command));
    }

    private static string GetParameters(DbCommand command)
    {
        if (command.Parameters.Count == 0)
            return "None";

        return string.Join(", ",
            command.Parameters
                   .Cast<DbParameter>()
                   .Select(p => $"{p.ParameterName}={p.Value}"));
    }


}