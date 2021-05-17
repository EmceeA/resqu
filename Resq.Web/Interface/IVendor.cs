using Resq.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resq.Web.Interface
{
    public interface IVendor
    {
        public string UploadImage(CreateVendorViewModel createVendor);
        public string CreateVendor(CreateVendorViewModel createVendor);
        public string GenerateCode();
    }
}
