using System;
using Microsoft.IdentityModel.Tokens;

namespace OWASP_Desktop_App_Security_Top_10
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example 1: Insecure TokenValidationParameters
            var parameters1 = new TokenValidationParameters
            {
                // ruleid: jwt-tokenvalidationparameters-no-expiry-validation
                ValidateLifetime = false,
                RequireSignedTokens = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                // ruleid: jwt-tokenvalidationparameters-no-expiry-validation
                RequireExpirationTime = false
            };

            // Example 2: Secure TokenValidationParameters
            var parameters2 = new TokenValidationParameters
            {
                // ok: jwt-tokenvalidationparameters-no-expiry-validation
                ValidateLifetime = true,
                RequireSignedTokens = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                // ok: jwt-tokenvalidationparameters-no-expiry-validation
                RequireExpirationTime = true
            };

            // Example 3: Individual assignments
            TokenValidationParameters parameters3 = new TokenValidationParameters();
            // ruleid: jwt-tokenvalidationparameters-no-expiry-validation
            parameters3.RequireExpirationTime = false;
            parameters3.ValidateAudience = false;
            parameters3.ValidateIssuer = false;
            // ruleid: jwt-tokenvalidationparameters-no-expiry-validation
            parameters3.ValidateLifetime = false;
            // ok: jwt-tokenvalidationparameters-no-expiry-validation
            parameters3.ValidateLifetime = true;
            // ok: jwt-tokenvalidationparameters-no-expiry-validation
            parameters3.RequireExpirationTime = true;
        }
    }
}