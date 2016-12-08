using ClientManagement.UI.ServiceAccess.cmLogicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientManagement.UI.MVC.Models
{
    public class DisplayClientsViewModel
    {
        public List<ClientDTO> ClientsToDisplay { get; set; }
    }
}