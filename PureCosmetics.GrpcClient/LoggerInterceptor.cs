using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpc.Core.Interceptors.Interceptor;

namespace PureCosmetics.GrpcClient
{
    /// <summary>
    /// LoggerInterceptor class for gRPC client calls to log request and response details.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpContextAccessor"></param>
    public class LoggerInterceptor(
    ILogger<LoggerInterceptor> logger,
    IHttpContextAccessor? httpContextAccessor
) : Interceptor
    {
        #region Fields and Constants

        /// <summary>
        /// Microservice caller metadata keys
        /// </summary>
        private const string MicroserviceCallerUserName = "caller-user";

        /// <summary>
        /// Microservice caller machine name metadata key
        /// </summary>
        private const string MicroserviceCallerMachineName = "caller-machine";

        /// <summary>
        /// Microservice caller OS version metadata key
        /// </summary>
        private const string MicroserviceCallerOsVersion = "caller-os";

        /// <summary>
        /// Microservice caller session code metadata key
        /// </summary>
        private const string MicroserviceCallerSessionCode = "caller-vnnss";

        /// <summary>
        /// Host of the gRPC service being called
        /// </summary>
        public string? Host { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Unary call interception to add logging and caller metadata
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            AddCallerMetadata(ref context);
            var call = continuation(request, context);

            return new AsyncUnaryCall<TResponse>(
                HandleResponse(context.Method.ServiceName, context.Method.Name, request,
                    call.ResponseAsync),
                call.ResponseHeadersAsync,
                call.GetStatus, call.GetTrailers, call.Dispose);
        }

        /// <summary>
        /// Streaming call interception to add logging and caller metadata
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            AddCallerMetadata(ref context);
            return continuation(context);
        }

        /// <summary>
        /// Server streaming call interception to add logging and caller metadata
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            AddCallerMetadata(ref context);
            return continuation(request, context);
        }

        /// <summary>
        /// Duplex streaming call interception to add logging and caller metadata
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            AddCallerMetadata(ref context);
            return continuation(context);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Handle the response from the gRPC call, logging execution time and errors.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="action"></param>
        /// <param name="request"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private async Task<TResponse> HandleResponse<TResponse>(string serviceName, string action, object request,
            Task<TResponse> t)
        {
            var startTime = Stopwatch.GetTimestamp();
            var actionTracking = $"/{serviceName}/{action}";
            try
            {

                var response = await t;
                var executeTime = Stopwatch.GetElapsedTime(startTime);
                logger.LogInformation("Service Request host {Host} url {ActionTracking} executeTime {ExecuteTime}",
                    Host,
                    actionTracking,
                    executeTime);
                return response;
            }
            catch (Exception ex)
            {
                var executeTime = Stopwatch.GetElapsedTime(startTime);
                // Log error to the console.
                // Note: Configuring .NET Core logging is the recommended way to log errors
                // https://docs.microsoft.com/aspnet/core/grpc/diagnostics#grpc-client-logging
                // var initialColor = Console.ForegroundColor;
                // Console.ForegroundColor = ConsoleColor.Red;
                LogError(ex,
                    $"GRPC call error - ServiceName: {serviceName} - Action: {action} - Message: {ex.Message}", serviceName,
                    action);
                // Console.ForegroundColor = initialColor;
                throw;
            }
        }

        

        /// <summary>
        /// Call to add caller metadata to the gRPC call headers
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="context"></param>
        private void AddCallerMetadata<TRequest, TResponse>(ref ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            var headers = context.Options.Headers;

            // Call doesn't have a headers collection to add to.
            // Need to create a new context with headers for the call.
            if (headers == null)
            {
                headers = new Metadata();
                var options = context.Options.WithHeaders(headers);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            }

            var callerUserEntry = headers.Get(MicroserviceCallerUserName);
            var callerMachineEntry = headers.Get(MicroserviceCallerMachineName);
            var callerOsEntry = headers.Get(MicroserviceCallerOsVersion);
            var callerSessionCodeEntry = headers.Get(MicroserviceCallerSessionCode);
            var callerSessionCode = (callerSessionCodeEntry?.Value) ?? string.Empty;
            if (callerUserEntry != null)
            {
                headers.Remove(callerUserEntry);
            }

            if (callerMachineEntry != null)
            {
                headers.Remove(callerMachineEntry);
            }

            if (callerOsEntry != null)
            {
                headers.Remove(callerOsEntry);
            }

            if (callerSessionCodeEntry != null)
            {
                headers.Remove(callerSessionCodeEntry);
            }

            // Add caller metadata to call headers
            headers.Add(MicroserviceCallerUserName, Environment.UserName);
            headers.Add(MicroserviceCallerMachineName, Environment.MachineName);
            headers.Add(MicroserviceCallerOsVersion, Environment.OSVersion.ToString());
            headers.Add(MicroserviceCallerSessionCode, callerSessionCode);
            if (httpContextAccessor != null)
            {
                string? authorizationHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
                if (authorizationHeader?.Length > 0)
                {
                    headers.Add("Authorization", authorizationHeader);
                }
            }
        }

        /// <summary>
        /// Log error details using the logger
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="serviceName"></param>
        /// <param name="action"></param>
        private void LogError(Exception exception, string message, string serviceName, string action)
        {
            using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "CallToHost", Host ?? string.Empty },
                   { "CallToServiceName", serviceName },
                   { "CallToAction", action }
               }))
            {
                logger.LogError(exception, "{Message}", message);
            }
        }
        #endregion
    }
}
