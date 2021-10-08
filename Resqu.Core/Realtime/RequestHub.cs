//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using Resqu.Core.Dto;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Resqu.Core.Realtime
//{
//    public class RequestHub : Hub, IRequestHub
//    {
//        private readonly Resqu.Core.Entities.ResquContext _db;
//        public RequestHub(Resqu.Core.Entities.ResquContext db)
//        {
//            _db = db;
//        }

//        //Here i want to make sure that the request from a customer display on a vendor dashboard on realtime basis
//        public async Task<CustomerRequestResponseDto> CustomerRequestDetails(string methodName,string vendorId)
//        {

//            var getTodaysDateMinutes = DateTime.Now.Minute;
//            CustomerRequestResponseDto service = new CustomerRequestResponseDto
//            {

//            };
//            var getCustomerRequestDetails = await _db.ResquServices.Where(e => e.VendorPhone == vendorId && e.IsVendorAccepted == false).FirstOrDefaultAsync();
//            service.BookingId = getCustomerRequestDetails.BookingId;
//            service.Description = getCustomerRequestDetails.Description;
//            service.ServiceName = getCustomerRequestDetails.ServiceName;
//            service.SubCategoryName = getCustomerRequestDetails.SubCategoryName;
//            service.CustomerName = getCustomerRequestDetails.CustomerName;
//            service.CustomerPhone = getCustomerRequestDetails.CustomerPhone;
//            await Clients.User(vendorId).SendAsync("customerrequestdetails", service);
//            return service;
//        }

//        //public async Task<CustomerRequestResponseDto> send(CustomerRequestResponseDto service)
//        //{

//        //    var getTodaysDateMinutes = DateTime.Now.Minute;
//        //    var getCustomerRequestDetails = await _db.ResquServices.Where(e => e.VendorPhone == service.VendorPhone && e.IsVendorAccepted == false).FirstOrDefaultAsync();
//        //    service.BookingId = getCustomerRequestDetails.BookingId;
//        //    service.Description = getCustomerRequestDetails.Description;
//        //    service.ServiceName = getCustomerRequestDetails.ServiceName;
//        //    service.SubCategoryName = getCustomerRequestDetails.SubCategoryName;
//        //    service.CustomerName = getCustomerRequestDetails.CustomerName;
//        //    service.CustomerPhone = getCustomerRequestDetails.CustomerPhone;
//        //    return service;
//        //}

//    }

//    public interface IRequestHub
//    {
//        //Task<ServiceResponseDto> AlertVendor(string vendorName);
//        Task<CustomerRequestResponseDto> CustomerRequestDetails(string methodName, string vendorId);

//        //Task<CustomerRequestResponseDto> send(CustomerRequestResponseDto service);
//    }
//}
