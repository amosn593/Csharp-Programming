//using EfCoreInterceptor.Interfaces;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using System.Collections.Concurrent;
//using System.Data.Common;
//using System.Diagnostics;

//namespace EfCoreInterceptor.Data;

//public sealed class ObservabilityCommandInterceptor : DbCommandInterceptor
//{
//    private readonly ICurrentActor _actor;
//    private readonly ILogger<ObservabilityCommandInterceptor> _logger;

//    private static readonly TimeSpan SlowQueryThreshold = TimeSpan.FromMilliseconds(250);

//    // Thread-safe store for tracking command execution time
//    private static readonly ConcurrentDictionary<Guid, Stopwatch> _timers = new();

//    public ObservabilityCommandInterceptor(
//        ICurrentActor actor,
//        ILogger<ObservabilityCommandInterceptor> logger)
//    {
//        _actor = actor;
//        _logger = logger;
//    }

//    #region Reader

//    public override InterceptionResult<DbDataReader> ReaderExecuting(
//        DbCommand command,
//        CommandEventData eventData,
//        InterceptionResult<DbDataReader> result)
//    {
//        Tag(command);
//        StartTimer(eventData.CommandId);
//        return base.ReaderExecuting(command, eventData, result);
//    }

//    public override DbDataReader ReaderExecuted(
//    DbCommand command,
//    CommandExecutedEventData eventData,
//    DbDataReader result)
//    {
//        OnExecuted(eventData.CommandId, command, null);
//        return base.ReaderExecuted(command, eventData, result);
//    }

//    #endregion

//    #region NonQuery

//    public override InterceptionResult<int> NonQueryExecuting(
//        DbCommand command,
//        CommandEventData eventData,
//        InterceptionResult<int> result)
//    {
//        Tag(command);
//        StartTimer(eventData.CommandId);
//        return base.NonQueryExecuting(command, eventData, result);
//    }

//    public override void NonQueryExecuted(
//        DbCommand command,
//        CommandExecutedEventData eventData,
//        int result)
//    {
//        StopAndLog(eventData.CommandId, command);
//        base.NonQueryExecuted(command, eventData, result);
//    }

//    #endregion

//    #region Scalar

//    public override InterceptionResult<object> ScalarExecuting(
//        DbCommand command,
//        CommandEventData eventData,
//        InterceptionResult<object> result)
//    {
//        Tag(command);
//        StartTimer(eventData.CommandId);
//        return base.ScalarExecuting(command, eventData, result);
//    }

//    public override void ScalarExecuted(
//        DbCommand command,
//        CommandExecutedEventData eventData,
//        object result)
//    {
//        StopAndLog(eventData.CommandId, command);
//        base.ScalarExecuted(command, eventData, result);
//    }

//    #endregion

//    #region Failure

//    public override void CommandFailed(
//        DbCommand command,
//        CommandErrorEventData eventData)
//    {
//        _timers.TryRemove(eventData.CommandId, out var sw);

//        sw?.Stop();

//        _logger.LogError(
//            eventData.Exception,
//            "SQL FAILED ({ElapsedMs} ms) tenant:{TenantId} corr:{CorrelationId}\n{CommandText}",
//            sw?.Elapsed.TotalMilliseconds,
//            _actor.TenantId,
//            _actor.CorrelationId,
//            command.CommandText);

//        base.CommandFailed(command, eventData);
//    }

//    #endregion

//    #region Helpers

//    private void StartTimer(Guid commandId)
//    {
//        _timers.TryAdd(commandId, Stopwatch.StartNew());
//    }

//    private void StopAndLog(Guid commandId, DbCommand command)
//    {
//        if (!_timers.TryRemove(commandId, out var sw))
//            return;

//        sw.Stop();

//        if (sw.Elapsed < SlowQueryThreshold)
//            return;

//        _logger.LogWarning(
//            "Slow SQL ({ElapsedMs} ms) tenant:{TenantId} corr:{CorrelationId}\n{CommandText}\nParams: {@Params}",
//            sw.Elapsed.TotalMilliseconds,
//            _actor.TenantId,
//            _actor.CorrelationId,
//            command.CommandText,
//            GetParameters(command));
//    }

//    private void Tag(DbCommand command)
//    {
//        // Prevent duplicate tagging (retries, re-execution)
//        if (command.CommandText.StartsWith("/* tenant:"))
//            return;

//        var tenant = _actor.TenantId?.ToString() ?? "none";
//        var corr = _actor.CorrelationId ?? "none";

//        command.CommandText =
//            $"/* tenant:{tenant} corr:{corr} */\n{command.CommandText}";
//    }

//    private static object GetParameters(DbCommand command)
//    {
//        if (command.Parameters.Count == 0)
//            return Array.Empty<object>();

//        var list = new List<object>(command.Parameters.Count);

//        foreach (DbParameter p in command.Parameters)
//        {
//            list.Add(new
//            {
//                p.ParameterName,
//                Value = p.Value is DBNull ? null : p.Value
//            });
//        }

//        return list;
//    }

//    #endregion
//}