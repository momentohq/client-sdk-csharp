﻿using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using MomentoSdk.Exceptions;

namespace MomentoSdk
{
    public class MomentoSigner
    {
        private readonly JwtHeader jwtHeader;

        public MomentoSigner(string jwkJsonString)
        {
            try
            {
                var securityKey = new JsonWebKey(jwkJsonString);
                var credentials = new SigningCredentials(securityKey, securityKey.Alg);
                this.jwtHeader = new JwtHeader(credentials);
            }
            catch (Exception e)
            {
                throw new InvalidArgumentException($"Invalid JWK: {jwkJsonString}", e);
            }
        }

        /// <summary>
        /// Create a pre-signed HTTPS URL.
        /// </summary>
        /// <param name="hostname">Hostname of the SimpleCacheService. Use the value returned from CreateSigningKey's response.</param>
        /// <param name="signingRequest">The parameters used for generating a pre-signed URL</param>
        /// <returns></returns>
        public string CreatePresignedUrl(string hostname, SigningRequest signingRequest)
        {
            var jwtToken = SignAccessToken(signingRequest);
            var cacheName = signingRequest.CacheName;
            var cacheKey = signingRequest.CacheKey;

            return signingRequest.CacheOperation switch
            {
                CacheOperation.GET => $"https://{hostname}/cache/get/{cacheName}/{cacheKey}?token={jwtToken}",
                CacheOperation.SET => $"https://{hostname}/cache/set/{cacheName}/{cacheKey}?ttl={signingRequest.TtlSeconds * (ulong)1000}&token={jwtToken}",
                _ => throw new NotImplementedException($"Unhandled {signingRequest.CacheOperation}")
            };
        }

        /// <summary>
        /// Create the signature for auth to be used in JWT.
        /// </summary>
        /// <param name="hostname">Hostname of the SimpleCacheService. Use the value returned from CreateSigningKey's response.</param>
        /// <param name="signingRequest">The parameters used for generating a pre-signed URL</param>
        /// <returns></returns>
        public string SignAccessToken(SigningRequest signingRequest)
        {
            var payload = CommonJwtBody(signingRequest.CacheName, signingRequest.CacheKey, signingRequest.ExpiryEpochSeconds);
            switch (signingRequest.CacheOperation)
            {
                case CacheOperation.GET:
                    {
                        payload.Add("method", new string[] { "get" });
                        break;
                    }
                case CacheOperation.SET:
                    {
                        payload.Add("method", new string[] { "set" });
                        payload.Add("ttl", signingRequest.TtlSeconds);
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException($"Unhandled {signingRequest.CacheOperation}");
                    }
            }

            return CreateJwtToken(payload);
        }

        private string CreateJwtToken(JwtPayload jwtPayload)
        {
            try
            {
                var jwtToken = new JwtSecurityToken(this.jwtHeader, jwtPayload);
                return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }
            catch (Exception e)
            {
                throw new InvalidArgumentException($"Invalid JWK alg: {jwtHeader.Alg}", e);
            }
        }


        private JwtPayload CommonJwtBody(string cacheName, string cacheKey, uint expiryEpochSeconds)
        {
            return new JwtPayload()
            {
                { "exp", expiryEpochSeconds },
                { "cache", cacheName },
                { "key", cacheKey }
            };
        }
    }
}