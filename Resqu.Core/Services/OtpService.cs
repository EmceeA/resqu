using Microsoft.EntityFrameworkCore;
using Resqu.Core.Dto;
using Resqu.Core.Entities;
using Resqu.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Services
{
    public class OtpService : IOtp
    {
        private readonly ResquContext _context;
        public OtpService(ResquContext context)
        {
            _context = context;
        }
        public async Task<SendOtpResponseDto> SendOtp(SendOtpRequestDto otpRequest)
        {
            try
            {
                var getNumber = await _context.Customers.Where(c => c.PhoneNumber == otpRequest.MobileNumber).AnyAsync();
                if (getNumber)
                {
                    return new SendOtpResponseDto
                    {
                        Message = "The Number is Already Enrolled",
                        Status = false
                    };
                }

                var getOtpRecord = await _context.Otps.Where(c => c.Phone == otpRequest.MobileNumber).FirstOrDefaultAsync();

                if (getOtpRecord == null)
                {
                    var otp = GenerateRandom(6);
                    var otps = new Otp
                    {
                        DateModified = DateTime.Now.ToString(),
                        OtpNumber = otp,
                        Phone = otpRequest.MobileNumber
                    };
                    await _context.Otps.AddAsync(otps);
                    await _context.SaveChangesAsync();

                    //Send Otp

                    var otpResponse = Send(otps.OtpNumber, otpRequest.MobileNumber);


                    if (otpResponse)
                    {
                        return new SendOtpResponseDto
                        {
                            Message = "Success",
                            Status = true
                        };
                    }

                }
                if (getOtpRecord != null)
                {
                    //Generate OTP and Send
                    getOtpRecord.OtpNumber = GenerateRandom(6);
                    await _context.SaveChangesAsync();

                    //Send Otp

                    var otpResponse  = Send(getOtpRecord.OtpNumber, otpRequest.MobileNumber);


                    if (otpResponse)
                    {
                        return new SendOtpResponseDto
                        {
                            Message = "Success",
                            Status = true
                        };
                    }

                }

                return new SendOtpResponseDto
                {
                    Message = "An Error Occurred",
                    Status = false
                };

            }
            catch (Exception ex)
            {
                throw;
            }

            
        }


        public string GenerateRandom(int length)
        {
            string chars = "1234567890";
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            byte[] data = new byte[length];
            byte[] buffer = null;
            int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);
            crypto.GetBytes(data);
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                byte value = data[i];
                while (value > maxRandom)
                {
                    if (buffer == null)
                    {
                        buffer = new byte[1];
                    }
                    crypto.GetBytes(buffer);
                    value = buffer[0];
                }
                result[i] = chars[value % chars.Length];
            }
            var res = new string(result);
            return res;
        }

        public bool Send(string phoneNumber, string otp)
        {
            return true;
        }
    }
}
