using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentAssertions;

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
    }
}
