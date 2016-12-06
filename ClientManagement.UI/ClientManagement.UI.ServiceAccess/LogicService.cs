using ClientManagement.UI.ServiceAccess.cmLogicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.UI.ServiceAccess
{

    public class LogicService
    {
        private ServiceClient cmLogicService = new ServiceClient();

        #region Client Related
        public List<ClientDTO> GetClientsForUser(string userId)
        {
            List<ClientDTO> clients = cmLogicService.getClientsByUserId(userId).ToList();
            return clients;
        }

        public bool AddClient(string name, string email, string phone, string userId, string street, string city, string state, string zip)
        {
            ClientDTO newClient = new ClientDTO();

            newClient.Name = name;
            newClient.Email = email;
            newClient.PhoneNumber = phone;
            newClient.UserId = userId;


            AddressDTO address = new AddressDTO();

            address.Street = street;
            address.City = city;
            address.State = state;
            address.Zip = zip;

            newClient.Address = address;

            return cmLogicService.insertClients(newClient);
        }
        #endregion

        #region Service Type Related
        public bool AddServiceType(string name, decimal rate, string userId)
        {
            ServiceTypeDTO newServiceType = new ServiceTypeDTO();

            newServiceType.Name = name;
            newServiceType.Rate = rate;
            newServiceType.UserId = userId;

            return cmLogicService.insertServiceType(newServiceType);
        }

        public List<ServiceTypeDTO> GetServiceTypes()
        {
            List<ServiceTypeDTO> serviceTypes = cmLogicService.getServiceTypes().ToList();

            return serviceTypes;
        }
        #endregion

        #region Job Related 
        public bool ScheduleJob(string name, decimal rate, string userId)
        {
            jobDTO newJob = new jobDTO();

            newJob.ClientId
            newJob.EstimatedDuration
            newJob.Complete
            newJob.Notes


            return cmLogicService.insertServiceType(newServiceType);
        }
        #endregion
    }
}
