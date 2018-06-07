// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using AspNetCoreCustomUserManager.Models;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreCustomUserManager
{
  public enum SignUpResultError
  {
    CredentialTypeNotFound
  }

  public class SignUpResult
  {
    public User User { get; set; }
    public bool Success { get; set; }
    public SignUpResultError? Error { get; set; }

    public SignUpResult(User user = null, bool success = false, SignUpResultError? error = null)
    {
      this.User = user;
      this.Success = success;
      this.Error = error;
    }
  }

  public enum ValidateResultError
  {
    CredentialTypeNotFound,
    CredentialNotFound,
    SecretNotValid
  }

  public class ValidateResult
  {
    public User User { get; set; }
    public bool Success { get; set; }
    public ValidateResultError? Error { get; set; }

    public ValidateResult(User user = null, bool success = false, ValidateResultError? error = null)
    {
      this.User = user;
      this.Success = success;
      this.Error = error;
    }
  }

  public enum ChangeSecretResultError
  {
    CredentialTypeNotFound,
    CredentialNotFound
  }

  public class ChangeSecretResult
  {
    public bool Success { get; set; }
    public ChangeSecretResultError? Error { get; set; }

    public ChangeSecretResult(bool success = false, ChangeSecretResultError? error = null)
    {
      this.Success = success;
      this.Error = error;
    }
  }

  public interface IUserManager
  {
    SignUpResult SignUp(string name, string credentialTypeCode, string identifier);
    SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret);
    void AddToRole(User user, string roleCode);
    void AddToRole(User user, Role role);
    void RemoveFromRole(User user, string roleCode);
    void RemoveFromRole(User user, Role role);
    ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret);
    ValidateResult Validate(string credentialTypeCode, string identifier);
    ValidateResult Validate(string credentialTypeCode, string identifier, string secret);
    void SignIn(HttpContext httpContext, User user, bool isPersistent = false);
    void SignOut(HttpContext httpContext);
    int GetCurrentUserId(HttpContext httpContext);
    User GetCurrentUser(HttpContext httpContext);
  }
}