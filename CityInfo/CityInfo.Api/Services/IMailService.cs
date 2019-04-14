using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}
