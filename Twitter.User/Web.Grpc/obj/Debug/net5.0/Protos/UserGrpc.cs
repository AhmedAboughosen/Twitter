// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/user.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Twitter.Web.User.Grpc.Protos.User {
  public static partial class User
  {
    static readonly string __ServiceName = "twitter.user.User";

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

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

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

    static readonly grpc::Marshaller<global::Twitter.Web.User.Grpc.Protos.User.CreateAccountRequest> __Marshaller_twitter_user_CreateAccountRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Twitter.Web.User.Grpc.Protos.User.CreateAccountRequest.Parser));
    static readonly grpc::Marshaller<global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> __Marshaller_twitter_user_MessageResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse.Parser));
    static readonly grpc::Marshaller<global::Twitter.Web.User.Grpc.Protos.User.ConfirmAccountRequest> __Marshaller_twitter_user_ConfirmAccountRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Twitter.Web.User.Grpc.Protos.User.ConfirmAccountRequest.Parser));
    static readonly grpc::Marshaller<global::Twitter.Web.User.Grpc.Protos.User.UpdateProfileRequest> __Marshaller_twitter_user_UpdateProfileRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Twitter.Web.User.Grpc.Protos.User.UpdateProfileRequest.Parser));
    static readonly grpc::Marshaller<global::Twitter.Web.User.Grpc.Protos.User.UserLoginRequest> __Marshaller_twitter_user_UserLoginRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Twitter.Web.User.Grpc.Protos.User.UserLoginRequest.Parser));
    static readonly grpc::Marshaller<global::Twitter.Web.User.Grpc.Protos.User.UserLoginResponse> __Marshaller_twitter_user_UserLoginResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Twitter.Web.User.Grpc.Protos.User.UserLoginResponse.Parser));
    static readonly grpc::Marshaller<global::Twitter.Web.User.Grpc.Protos.User.AddFollowerRequest> __Marshaller_twitter_user_AddFollowerRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Twitter.Web.User.Grpc.Protos.User.AddFollowerRequest.Parser));
    static readonly grpc::Marshaller<global::Twitter.Web.User.Grpc.Protos.User.UnFollowerRequest> __Marshaller_twitter_user_UnFollowerRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Twitter.Web.User.Grpc.Protos.User.UnFollowerRequest.Parser));

    static readonly grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.CreateAccountRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> __Method_CreateAccount = new grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.CreateAccountRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateAccount",
        __Marshaller_twitter_user_CreateAccountRequest,
        __Marshaller_twitter_user_MessageResponse);

    static readonly grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.ConfirmAccountRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> __Method_ConfirmAccount = new grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.ConfirmAccountRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ConfirmAccount",
        __Marshaller_twitter_user_ConfirmAccountRequest,
        __Marshaller_twitter_user_MessageResponse);

    static readonly grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.UpdateProfileRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> __Method_UpdateProfile = new grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.UpdateProfileRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateProfile",
        __Marshaller_twitter_user_UpdateProfileRequest,
        __Marshaller_twitter_user_MessageResponse);

    static readonly grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.UserLoginRequest, global::Twitter.Web.User.Grpc.Protos.User.UserLoginResponse> __Method_UserLogin = new grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.UserLoginRequest, global::Twitter.Web.User.Grpc.Protos.User.UserLoginResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UserLogin",
        __Marshaller_twitter_user_UserLoginRequest,
        __Marshaller_twitter_user_UserLoginResponse);

    static readonly grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.AddFollowerRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> __Method_AddFollower = new grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.AddFollowerRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddFollower",
        __Marshaller_twitter_user_AddFollowerRequest,
        __Marshaller_twitter_user_MessageResponse);

    static readonly grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.UnFollowerRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> __Method_UnFollower = new grpc::Method<global::Twitter.Web.User.Grpc.Protos.User.UnFollowerRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UnFollower",
        __Marshaller_twitter_user_UnFollowerRequest,
        __Marshaller_twitter_user_MessageResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Twitter.Web.User.Grpc.Protos.User.UserReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of User</summary>
    [grpc::BindServiceMethod(typeof(User), "BindService")]
    public abstract partial class UserBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> CreateAccount(global::Twitter.Web.User.Grpc.Protos.User.CreateAccountRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> ConfirmAccount(global::Twitter.Web.User.Grpc.Protos.User.ConfirmAccountRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> UpdateProfile(global::Twitter.Web.User.Grpc.Protos.User.UpdateProfileRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Twitter.Web.User.Grpc.Protos.User.UserLoginResponse> UserLogin(global::Twitter.Web.User.Grpc.Protos.User.UserLoginRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> AddFollower(global::Twitter.Web.User.Grpc.Protos.User.AddFollowerRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Twitter.Web.User.Grpc.Protos.User.MessageResponse> UnFollower(global::Twitter.Web.User.Grpc.Protos.User.UnFollowerRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(UserBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreateAccount, serviceImpl.CreateAccount)
          .AddMethod(__Method_ConfirmAccount, serviceImpl.ConfirmAccount)
          .AddMethod(__Method_UpdateProfile, serviceImpl.UpdateProfile)
          .AddMethod(__Method_UserLogin, serviceImpl.UserLogin)
          .AddMethod(__Method_AddFollower, serviceImpl.AddFollower)
          .AddMethod(__Method_UnFollower, serviceImpl.UnFollower).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, UserBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreateAccount, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Twitter.Web.User.Grpc.Protos.User.CreateAccountRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(serviceImpl.CreateAccount));
      serviceBinder.AddMethod(__Method_ConfirmAccount, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Twitter.Web.User.Grpc.Protos.User.ConfirmAccountRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(serviceImpl.ConfirmAccount));
      serviceBinder.AddMethod(__Method_UpdateProfile, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Twitter.Web.User.Grpc.Protos.User.UpdateProfileRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(serviceImpl.UpdateProfile));
      serviceBinder.AddMethod(__Method_UserLogin, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Twitter.Web.User.Grpc.Protos.User.UserLoginRequest, global::Twitter.Web.User.Grpc.Protos.User.UserLoginResponse>(serviceImpl.UserLogin));
      serviceBinder.AddMethod(__Method_AddFollower, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Twitter.Web.User.Grpc.Protos.User.AddFollowerRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(serviceImpl.AddFollower));
      serviceBinder.AddMethod(__Method_UnFollower, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Twitter.Web.User.Grpc.Protos.User.UnFollowerRequest, global::Twitter.Web.User.Grpc.Protos.User.MessageResponse>(serviceImpl.UnFollower));
    }

  }
}
#endregion