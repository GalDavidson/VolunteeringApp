using System;
using System.Collections.Generic;
using System.Text;
using VolunteeringApp.Services;

namespace VolunteeringApp.Models
{
    public partial class Post
    {
        public string ImgSource
        {
            get
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                //Create a source with cache busting!
                Random r = new Random();
                string source = $"{proxy.GetBasePhotoUri()}/{this.AssociationId}.jpg?{r.Next()}";
                return source;
            }
        }
    }
}
