// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: wukong.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Merlincomm {
  public static partial class gRPCWukong
  {
    static readonly string __ServiceName = "merlincomm.gRPCWukong";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.ReadTempRequest> __Marshaller_merlincomm_ReadTempRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.ReadTempRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.ReadTempReply> __Marshaller_merlincomm_ReadTempReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.ReadTempReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.ReadAllRequest> __Marshaller_merlincomm_ReadAllRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.ReadAllRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.ReadAllReply> __Marshaller_merlincomm_ReadAllReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.ReadAllReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.ReadConfigRequest> __Marshaller_merlincomm_ReadConfigRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.ReadConfigRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.ReadConfigReply> __Marshaller_merlincomm_ReadConfigReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.ReadConfigReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.WriteConfigRequest> __Marshaller_merlincomm_WriteConfigRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.WriteConfigRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.WriteConfigReply> __Marshaller_merlincomm_WriteConfigReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.WriteConfigReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.ReadRealTimeStatesRequest> __Marshaller_merlincomm_ReadRealTimeStatesRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.ReadRealTimeStatesRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.ReadRealTimeStatesReply> __Marshaller_merlincomm_ReadRealTimeStatesReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.ReadRealTimeStatesReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.HeartBeatRequestWk> __Marshaller_merlincomm_HeartBeatRequestWk = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.HeartBeatRequestWk.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Merlincomm.HeartBeatReplyWk> __Marshaller_merlincomm_HeartBeatReplyWk = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Merlincomm.HeartBeatReplyWk.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Merlincomm.ReadTempRequest, global::Merlincomm.ReadTempReply> __Method_ReadTemp = new grpc::Method<global::Merlincomm.ReadTempRequest, global::Merlincomm.ReadTempReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ReadTemp",
        __Marshaller_merlincomm_ReadTempRequest,
        __Marshaller_merlincomm_ReadTempReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Merlincomm.ReadAllRequest, global::Merlincomm.ReadAllReply> __Method_ReadAll = new grpc::Method<global::Merlincomm.ReadAllRequest, global::Merlincomm.ReadAllReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ReadAll",
        __Marshaller_merlincomm_ReadAllRequest,
        __Marshaller_merlincomm_ReadAllReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Merlincomm.ReadConfigRequest, global::Merlincomm.ReadConfigReply> __Method_ReadConfig = new grpc::Method<global::Merlincomm.ReadConfigRequest, global::Merlincomm.ReadConfigReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ReadConfig",
        __Marshaller_merlincomm_ReadConfigRequest,
        __Marshaller_merlincomm_ReadConfigReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Merlincomm.WriteConfigRequest, global::Merlincomm.WriteConfigReply> __Method_WriteConfig = new grpc::Method<global::Merlincomm.WriteConfigRequest, global::Merlincomm.WriteConfigReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "WriteConfig",
        __Marshaller_merlincomm_WriteConfigRequest,
        __Marshaller_merlincomm_WriteConfigReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Merlincomm.ReadRealTimeStatesRequest, global::Merlincomm.ReadRealTimeStatesReply> __Method_ReadRealTimeStates = new grpc::Method<global::Merlincomm.ReadRealTimeStatesRequest, global::Merlincomm.ReadRealTimeStatesReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ReadRealTimeStates",
        __Marshaller_merlincomm_ReadRealTimeStatesRequest,
        __Marshaller_merlincomm_ReadRealTimeStatesReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Merlincomm.HeartBeatRequestWk, global::Merlincomm.HeartBeatReplyWk> __Method_HeartBeat = new grpc::Method<global::Merlincomm.HeartBeatRequestWk, global::Merlincomm.HeartBeatReplyWk>(
        grpc::MethodType.Unary,
        __ServiceName,
        "HeartBeat",
        __Marshaller_merlincomm_HeartBeatRequestWk,
        __Marshaller_merlincomm_HeartBeatReplyWk);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Merlincomm.WukongReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of gRPCWukong</summary>
    [grpc::BindServiceMethod(typeof(gRPCWukong), "BindService")]
    public abstract partial class gRPCWukongBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Merlincomm.ReadTempReply> ReadTemp(global::Merlincomm.ReadTempRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Merlincomm.ReadAllReply> ReadAll(global::Merlincomm.ReadAllRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Merlincomm.ReadConfigReply> ReadConfig(global::Merlincomm.ReadConfigRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Merlincomm.WriteConfigReply> WriteConfig(global::Merlincomm.WriteConfigRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Merlincomm.ReadRealTimeStatesReply> ReadRealTimeStates(global::Merlincomm.ReadRealTimeStatesRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Merlincomm.HeartBeatReplyWk> HeartBeat(global::Merlincomm.HeartBeatRequestWk request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for gRPCWukong</summary>
    public partial class gRPCWukongClient : grpc::ClientBase<gRPCWukongClient>
    {
      /// <summary>Creates a new client for gRPCWukong</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public gRPCWukongClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for gRPCWukong that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public gRPCWukongClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected gRPCWukongClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected gRPCWukongClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.ReadTempReply ReadTemp(global::Merlincomm.ReadTempRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ReadTemp(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.ReadTempReply ReadTemp(global::Merlincomm.ReadTempRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ReadTemp, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.ReadTempReply> ReadTempAsync(global::Merlincomm.ReadTempRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ReadTempAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.ReadTempReply> ReadTempAsync(global::Merlincomm.ReadTempRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ReadTemp, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.ReadAllReply ReadAll(global::Merlincomm.ReadAllRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ReadAll(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.ReadAllReply ReadAll(global::Merlincomm.ReadAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ReadAll, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.ReadAllReply> ReadAllAsync(global::Merlincomm.ReadAllRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ReadAllAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.ReadAllReply> ReadAllAsync(global::Merlincomm.ReadAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ReadAll, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.ReadConfigReply ReadConfig(global::Merlincomm.ReadConfigRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ReadConfig(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.ReadConfigReply ReadConfig(global::Merlincomm.ReadConfigRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ReadConfig, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.ReadConfigReply> ReadConfigAsync(global::Merlincomm.ReadConfigRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ReadConfigAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.ReadConfigReply> ReadConfigAsync(global::Merlincomm.ReadConfigRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ReadConfig, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.WriteConfigReply WriteConfig(global::Merlincomm.WriteConfigRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return WriteConfig(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.WriteConfigReply WriteConfig(global::Merlincomm.WriteConfigRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_WriteConfig, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.WriteConfigReply> WriteConfigAsync(global::Merlincomm.WriteConfigRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return WriteConfigAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.WriteConfigReply> WriteConfigAsync(global::Merlincomm.WriteConfigRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_WriteConfig, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.ReadRealTimeStatesReply ReadRealTimeStates(global::Merlincomm.ReadRealTimeStatesRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ReadRealTimeStates(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.ReadRealTimeStatesReply ReadRealTimeStates(global::Merlincomm.ReadRealTimeStatesRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ReadRealTimeStates, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.ReadRealTimeStatesReply> ReadRealTimeStatesAsync(global::Merlincomm.ReadRealTimeStatesRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ReadRealTimeStatesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.ReadRealTimeStatesReply> ReadRealTimeStatesAsync(global::Merlincomm.ReadRealTimeStatesRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ReadRealTimeStates, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.HeartBeatReplyWk HeartBeat(global::Merlincomm.HeartBeatRequestWk request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return HeartBeat(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Merlincomm.HeartBeatReplyWk HeartBeat(global::Merlincomm.HeartBeatRequestWk request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_HeartBeat, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.HeartBeatReplyWk> HeartBeatAsync(global::Merlincomm.HeartBeatRequestWk request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return HeartBeatAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Merlincomm.HeartBeatReplyWk> HeartBeatAsync(global::Merlincomm.HeartBeatRequestWk request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_HeartBeat, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override gRPCWukongClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new gRPCWukongClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(gRPCWukongBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_ReadTemp, serviceImpl.ReadTemp)
          .AddMethod(__Method_ReadAll, serviceImpl.ReadAll)
          .AddMethod(__Method_ReadConfig, serviceImpl.ReadConfig)
          .AddMethod(__Method_WriteConfig, serviceImpl.WriteConfig)
          .AddMethod(__Method_ReadRealTimeStates, serviceImpl.ReadRealTimeStates)
          .AddMethod(__Method_HeartBeat, serviceImpl.HeartBeat).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, gRPCWukongBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_ReadTemp, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Merlincomm.ReadTempRequest, global::Merlincomm.ReadTempReply>(serviceImpl.ReadTemp));
      serviceBinder.AddMethod(__Method_ReadAll, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Merlincomm.ReadAllRequest, global::Merlincomm.ReadAllReply>(serviceImpl.ReadAll));
      serviceBinder.AddMethod(__Method_ReadConfig, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Merlincomm.ReadConfigRequest, global::Merlincomm.ReadConfigReply>(serviceImpl.ReadConfig));
      serviceBinder.AddMethod(__Method_WriteConfig, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Merlincomm.WriteConfigRequest, global::Merlincomm.WriteConfigReply>(serviceImpl.WriteConfig));
      serviceBinder.AddMethod(__Method_ReadRealTimeStates, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Merlincomm.ReadRealTimeStatesRequest, global::Merlincomm.ReadRealTimeStatesReply>(serviceImpl.ReadRealTimeStates));
      serviceBinder.AddMethod(__Method_HeartBeat, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Merlincomm.HeartBeatRequestWk, global::Merlincomm.HeartBeatReplyWk>(serviceImpl.HeartBeat));
    }

  }
}
#endregion
