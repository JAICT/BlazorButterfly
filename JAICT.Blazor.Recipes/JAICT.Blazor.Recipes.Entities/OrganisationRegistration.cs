using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JAICT.Blazor.Recipes.Entities
{
    public class OrganisationRegistration
    {        
        public Guid OrganisationId { get; set; }

        public string OrganisationName { get; set; }

        public string OrganisationAccessKey { get; set; }
    }

    public class OrganisationRegistrationResult
    {
        public string OrganisationAccessKey { get; set; }
    }
}
