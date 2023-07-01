using ETicaretAPI.Application.Abstractions.Services;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class QRCodeService : IQRCodeService
    {


        public byte[] GenerateQRCode(string code)
        {
            QRCodeGenerator generator = new QRCodeGenerator();

            QRCodeData data = generator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qRCode = new(data);
            byte[] graphics = qRCode.GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });

            return graphics;

        }
    }
}
