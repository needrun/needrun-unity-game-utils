using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NeedrunGameUtils
{
    // Nonce는 컴퓨터 공학에서 암호화에 사용하기 위해 만드는 임시 값임. (OTP와 유사하다 생각하면 좋음!)
    public class NonceUtils
    {
        // Apple 연동에 사용할 Nonce값을 만드는 함수
        // 참조: https://github.com/lupidan/apple-signin-unity/blob/master/docs/Firebase_NOTES.md#step-3-generate-the-sha256-of-the-raw-nonce
        public static string GenerateSHA256NonceFromRawNonce(string rawNonce)
        {
            var sha = new SHA256Managed();
            var utf8RawNonce = Encoding.UTF8.GetBytes(rawNonce);
            var hash = sha.ComputeHash(utf8RawNonce);

            var result = string.Empty;
            for (var i = 0; i < hash.Length; i++)
            {
                result += hash[i].ToString("x2");
            }

            return result;
        }

        // Firebase 연동에 사용할 Nonce값을 만드는 함수
        // 이 값은 Apple nonce를 만드는 데도 사용된다
        // 참조: https://github.com/lupidan/apple-signin-unity/blob/master/docs/Firebase_NOTES.md#step-2-generating-a-random-raw-nonce
        public static string GenerateRandomString(int length)
        {
            if (length <= 0)
            {
                throw new Exception("Expected nonce to have positive length");
            }

            const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._";
            var cryptographicallySecureRandomNumberGenerator = new RNGCryptoServiceProvider();
            var result = string.Empty;
            var remainingLength = length;

            var randomNumberHolder = new byte[1];
            while (remainingLength > 0)
            {
                var randomNumbers = new List<int>(16);
                for (var randomNumberCount = 0; randomNumberCount < 16; randomNumberCount++)
                {
                    cryptographicallySecureRandomNumberGenerator.GetBytes(randomNumberHolder);
                    randomNumbers.Add(randomNumberHolder[0]);
                }

                for (var randomNumberIndex = 0; randomNumberIndex < randomNumbers.Count; randomNumberIndex++)
                {
                    if (remainingLength == 0)
                    {
                        break;
                    }

                    var randomNumber = randomNumbers[randomNumberIndex];
                    if (randomNumber < charset.Length)
                    {
                        result += charset[randomNumber];
                        remainingLength--;
                    }
                }
            }

            return result;
        }
    }
}