using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentAssertions;
using OtpSharp;

namespace KeeOtp.Tests
{
    [TestFixture]
    public class OtpAuthDataTests
    {
        private const string base32Key = "AEBAG%3d%3d%3d";
        private byte[] binaryKey = new byte[] { 1, 2, 3 };

        [Test]
        public void ParseTotp_Success()
        {
            //arrange
            var parameters = $"key={base32Key}&size=12&step=33&type=Totp&counter=3";
            //act
            var data = OtpAuthData.FromString(parameters);
            //assert
            data.Should().NotBeNull();
            data.Size.Should().Be(12);
            data.Step.Should().Be(33);
            data.Type.Should().Be(OtpType.Totp);
            data.OtpHashMode.Should().Be(OtpHashMode.Sha1, "Default");
            data.Key.UsePlainKey(_ => _.Should().Equal(binaryKey));
            data.Counter.Should().Be(0, "This is a totp type so the counter shouldn't be parsed");
        }

        [Test]
        public void ParseHotp_Success()
        {
            //arrange
            var parameters = $"key={base32Key}&size=12&step=33&type=Hotp&counter=3";
            //act
            var data = OtpAuthData.FromString(parameters);
            //assert
            data.Should().NotBeNull();
            data.Size.Should().Be(12);
            data.Step.Should().Be(0, "This is a hotp type so the step shouldn't be parsed");
            data.Type.Should().Be(OtpType.Hotp);
            data.Key.UsePlainKey(_ => _.Should().Equal(binaryKey));
            data.Counter.Should().Be(3);
        }

        [Test]
        public void ParseTotpWithAlgorithm_Success()
        {
            //arrange
            var parameters = $"key={base32Key}&size=12&step=33&type=Totp&counter=3&otpHashMode=Sha256";
            //act
            var data = OtpAuthData.FromString(parameters);
            //assert
            data.Should().NotBeNull();
            data.Size.Should().Be(12);
            data.Step.Should().Be(33);
            data.Type.Should().Be(OtpType.Totp);
            data.OtpHashMode.Should().Be(OtpHashMode.Sha256);
            data.Key.UsePlainKey(_ => _.Should().Equal(binaryKey));
            data.Counter.Should().Be(0, "This is a totp type so the counter shouldn't be parsed");
        }

        [Test]
        public void ParseTotpStringWithAlgorithm_Success()
        {
            var data = new OtpAuthData();
            data.Size = 12;
            data.Step = 30;
            data.Type = OtpType.Totp;
            data.OtpHashMode = OtpHashMode.Sha256;
            data.Key = new ProtectedKey(binaryKey);
            //act
            var parameters = data.EncodedString;
            //assert
            parameters.Should().Be($"key={base32Key}&size=12&otpHashMode=Sha256");
        }

        [Test]
        public void ParseTotpStringWithoutAlgorithm_Success()
        {
            var data = new OtpAuthData();
            data.Size = 12;
            data.Step = 30;
            data.Type = OtpType.Totp;
            data.Key = new ProtectedKey(binaryKey);
            //act
            var parameters = data.EncodedString;
            //assert
            parameters.Should().Be($"key={base32Key}&size=12");
        }
    }
}
